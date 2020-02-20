using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace sistemaGestion
{
    public partial class ABMCategorias : Form
    {
        Conexion dbCon = Conexion.Instance();
        Form1 padre;
        public ABMCategorias(Form1 p)
        {
            InitializeComponent();
            padre = p;
        }

        private void ABMCategorias_Load(object sender, EventArgs e)
        {
            cargarLista();
            
        }

        private void cargarLista()
        {
            listBox1.Items.Clear();
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from categorias order by catcodigo asc", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader.GetString(0) + "-" + reader.GetString(1));
                }
                reader.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuCards1.Text = dbCon.extraerCodigo(listBox1.SelectedItem.ToString());
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from categorias where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox2.Text = reader.GetString(1);
                }
                reader.Close();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";
            
            bunifuCards1.Text = "";
            bunifuCards1.Enabled = true;
            bunifuFlatButton4.Enabled = false;
            bunifuCards3.Enabled = false;
            bunifuCards5.Enabled = false;
       
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            textBox2.Focus();
            clearError();
        }

        private void clearError()
        {
            foreach (TextBox tb in bunifuCards1.Controls.OfType<TextBox>())
            {
                tb.BackColor = Color.White;
            }

            foreach (TextBox tb in bunifuCards3.Controls.OfType<TextBox>())
            {
                tb.BackColor = Color.White;
            }
        }

        private void SetError(Control control, string mensaje)
        {
            control.BackColor = Color.FromArgb(217, 83, 79);
            control.Focus();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";
           
            bunifuCards1.Text = "";
            bunifuCards1.Enabled = false;
            bunifuFlatButton4.Enabled = false;
            bunifuCards3.Enabled = false;
            bunifuCards5.Enabled = true;
            
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            bunifuFlatButton3.Enabled = false;
            
            clearError();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            clearError();
            float numero;
            if (textBox2.Text == "") { SetError(textBox2, "Este campo no puede estar vacio"); return; }

            string query = "";

            if (bunifuCards1.Text == "") //es un prd nuevo
            {
                query = "insert into categorias values(NULL,'" + textBox2.Text + "')";
            }
            else // es una modificacion
            {
                query = "update categorias set nombre='" + textBox2.Text + "' where catcodigo=" + bunifuCards1.Text;
            }
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                reader.Close();
            }
            bunifuFlatButton2_Click(this, e);
            cargarLista();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            clearError();
            if (bunifuCards1.Text == "") { return; }
            if (textBox1.Text == "") { SetError(textBox1, "Este campo no puede estar vacio"); return;}
            if (MessageBox.Show("Está por AUMENTAR todos los precios en un " + textBox1.Text + "%. Esta operación puede demorar dependiendo de la cantidad de productos que tenga esta categoría.\nEsta operación no se puede deshacer y modifica los precios de venta y de venta por paquete.\nDesea continuar?", "Precios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //obtener total de prod a modificar
                var cmd = new MySqlCommand("select count(*) from catprod where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                padre.toolStripProgressBar1.Maximum = reader.GetInt32(0);
                padre.toolStripProgressBar1.Visible = true;
                padre.toolStripProgressBar1.Value = 0;
                Application.DoEvents();
                reader.Close();

                cmd = new MySqlCommand("select productos.proprecioventa,productos.preciopaquete,productos.procodigo from catprod inner join productos on productos.procodigo=catprod.procodigo where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
                 reader = cmd.ExecuteReader();
                double precio = 0; double preciopaquete = 0; double sumar = 0; double sumarpaquete = 0;
                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    padre.toolStripProgressBar1.Value++;
                    Application.DoEvents();
                    precio = double.Parse(dr["proprecioventa"].ToString());
                    sumar = (double.Parse(textBox1.Text.Replace(',', '.')) * precio) / 100;
                    try
                    {
                        preciopaquete = double.Parse(dr["preciopaquete"].ToString());
                        sumarpaquete = (double.Parse(textBox1.Text.Replace(',', '.')) * preciopaquete) / 100;
                    }
                    catch
                    {
                        preciopaquete = precio;
                        sumarpaquete = sumar;
                    }
                    cmd = new MySqlCommand("update productos set proprecioventa='" + (precio + sumar).ToString().Replace(',', '.') + "',preciopaquete='" + (preciopaquete + sumarpaquete).ToString().Replace(',', '.') + "' where procodigo=" + dr["procodigo"].ToString(), dbCon.Connection);
                    cmd.ExecuteNonQuery();


                }

                padre.toolStripProgressBar1.Visible = false;
                bunifuFlatButton2_Click(this, e);
                textBox1.Text = "";
                MessageBox.Show("Cambio de Precios finalizado!", "Precios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            clearError();
            if (bunifuCards1.Text == "") { return; }
            if (textBox1.Text == "") { SetError(textBox1, "Este campo no puede estar vacio"); return; }
            if (MessageBox.Show("Está por DISMINUIR todos los precios en un " + textBox1.Text + "%. Esta operación puede demorar dependiendo de la cantidad de productos que tenga esta categoría.\nEsta operación no se puede deshacer y modifica los precios de venta y de venta por paquete.\nDesea continuar?", "Precios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //obtener total de prod a modificar
                var cmd = new MySqlCommand("select count(*) from catprod where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                padre.toolStripProgressBar1.Maximum = reader.GetInt32(0);
                padre.toolStripProgressBar1.Visible = true;
                padre.toolStripProgressBar1.Value = 0;
                Application.DoEvents();
                reader.Close();
                 cmd = new MySqlCommand("select productos.proprecioventa,productos.preciopaquete,productos.procodigo from catprod inner join productos on productos.procodigo=catprod.procodigo where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
                 reader = cmd.ExecuteReader();
                double precio = 0; double preciopaquete = 0; double sumar = 0; double sumarpaquete = 0;
                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    padre.toolStripProgressBar1.Value++;
                    Application.DoEvents();
                    precio = double.Parse(dr["proprecioventa"].ToString());
                    sumar = (double.Parse(textBox1.Text.Replace(',', '.')) * precio) / 100;
                    try
                    {
                        preciopaquete = double.Parse(dr["preciopaquete"].ToString());
                        sumarpaquete = (double.Parse(textBox1.Text.Replace(',', '.')) * preciopaquete) / 100;
                    }
                    catch
                    {
                        preciopaquete = precio;
                        sumarpaquete = sumar;
                    }
                    cmd = new MySqlCommand("update productos set proprecioventa='" + (precio - sumar).ToString().Replace(',', '.') + "',preciopaquete='" + (preciopaquete - sumarpaquete).ToString().Replace(',', '.') + "' where procodigo=" + dr["procodigo"].ToString(), dbCon.Connection);
                    cmd.ExecuteNonQuery();


                }

                padre.toolStripProgressBar1.Visible = false;
                bunifuFlatButton2_Click(this, e);
                textBox1.Text = "";
                MessageBox.Show("Cambio de Precios finalizado!", "Precios", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }


            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            //fijarse si no hay productos asociados, luego eliminar.
            var cmd = new MySqlCommand("select * from catprod where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows) { MessageBox.Show("Hay productos asociados a esta categoria. Debe re asociar dichos productos para poder eliminar esta categoria.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error); reader.Close(); return; }
            reader.Close();
            cmd = new MySqlCommand("delete from catprod where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand("delete from categorias where catcodigo=" + bunifuCards1.Text, dbCon.Connection);
            cmd.ExecuteNonQuery();
            bunifuFlatButton2_Click(this, e);
            cargarLista();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
            bunifuCards1.Enabled = true;
            bunifuFlatButton4.Enabled = true;
            bunifuCards3.Enabled = true;
            bunifuCards5.Enabled = false;

            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
        }
    }
}
