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
    public partial class Ventas : Form
    {
        Form1 padre;string idUsuario;
        Conexion dbCon = Conexion.Instance();
        public Ventas(Form1 p, string id)
        {
            InitializeComponent();
            padre = p;
            idUsuario = id;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection col = new
          AutoCompleteStringCollection();
            if (dbCon.IsConnect())
            {
                string query = "select clirazonsocial from clientes";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    col.Add(reader.GetString(0));
                }
                reader.Close();
            }
            comboBox2.SelectedIndex = 0;
            textBox5.AutoCompleteCustomSource = col;
            AutoCompleteStringCollection col2 = new
         AutoCompleteStringCollection();
            if (dbCon.IsConnect())
            {
                string query = "select prodescripcion from productos where probaja=0";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    col2.Add(reader.GetString(0));
                }
                reader.Close();
            }
            textBox1.AutoCompleteCustomSource = col2;

            comboBox1.Items.Add("0-EFECTIVO");
            if (dbCon.IsConnect())
            {
                string query = "select * from tarjetas order by idtarjeta asc";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0) + "-" + reader.GetString(1));
                }
                reader.Close();
                comboBox1.SelectedIndex = 0;
            }

            nuevaFila();
        }

        private void nuevaFila()
        {


            dataGridView1.Rows.Add("", "", "", "", "", "");
            acomodarControles(dataGridView1.Rows.Count - 1);

        }

        private void acomodarControles(int fila)
        {
            try
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[fila].Cells[0];

                Rectangle posicion = dataGridView1.GetCellDisplayRectangle(0, fila, false);
                int x = posicion.Location.X + (posicion.Size.Height / 2)+237;
                int y = posicion.Location.Y + (posicion.Size.Height / 2)-10;
                textBox1.Location = new Point(x, y);
                textBox1.Size = posicion.Size;

                posicion = dataGridView1.GetCellDisplayRectangle(2, fila, false);
                x = posicion.Location.X + (posicion.Size.Height / 2)+237;
                y = posicion.Location.Y + (posicion.Size.Height / 2)-10;
                textBox2.Location = new Point(x, y);
                textBox2.Size = posicion.Size;

                posicion = dataGridView1.GetCellDisplayRectangle(3, fila, false);
                x = posicion.Location.X + (posicion.Size.Height / 2)+237;
                y = posicion.Location.Y + (posicion.Size.Height / 2) -10;
                textBox3.Location = new Point(x, y);
                textBox3.Size = posicion.Size;

                textBox1.Text = dataGridView1.Rows[fila].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[fila].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[fila].Cells[3].Value.ToString();

                textBox1.Focus();
                textBox1.SelectAll();
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            acomodarControles(e.RowIndex);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int numero; int fila = dataGridView1.CurrentRow.Index; float preciou = 0;
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
         }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float numero;
                textBox2.Text = textBox2.Text.Replace('.', ',');
                if (!float.TryParse(textBox2.Text, out numero)) { textBox2.Text = "1"; }
                if (textBox2.Text == "" || float.Parse(textBox2.Text) < 0) { textBox2.Text = "1"; }


                try
                {

                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = textBox2.Text;
                    if (float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString()) < float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()))
                    {
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[i].Style.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[i].Style.BackColor = Color.White;
                        }
                    }
                    float preciou = 0;
                    //calcular precio por paquete
                    preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());


                    calcularTotal();
                    textBox3.Focus();
                }
                catch
                {
                    textBox2.Focus();
                }
            }
        }

        public double RoundUp(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }

        private void calcularTotal()
        {
            if (textBox6.Text.Trim() == "") { textBox6.Text = "0"; }
            if (textBox7.Text.Trim() == "") { textBox7.Text = "0"; }




            float total = 0; float recargos = 0; float descuentos = 0;
            recargos = float.Parse(textBox6.Text.Replace('.', ','));
            descuentos = float.Parse(textBox7.Text.Replace('.', ','));

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() != "")
                {
                    total += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                }
            }
            //subtotal
            textBox8.Text = total.ToString();

            //primero, recalcular los montos de dcto y recargo
            float porcentaje = 0; float subtotal = 0; float importe = 0;
            try
            {
                //calcular importe de descuento
                porcentaje = float.Parse(textBox10.Text.Replace('.', ','));
                subtotal = float.Parse(textBox8.Text.Replace('.', ','));
                textBox7.Text = ((porcentaje * subtotal) / 100).ToString();


                //calcular importe de recargo
                porcentaje = float.Parse(textBox11.Text.Replace('.', ','));
                subtotal = float.Parse(textBox8.Text.Replace('.', ','));
                textBox6.Text = ((porcentaje * subtotal) / 100).ToString();


                //calcular porcentaje de descuento
                importe = float.Parse(textBox7.Text.Replace('.', ','));
                subtotal = float.Parse(textBox8.Text.Replace('.', ','));
                if (subtotal != 0)
                {
                    textBox10.Text = ((importe * 100) / subtotal).ToString();
                }



                //calcular porcentaje de recargo
                importe = float.Parse(textBox6.Text.Replace('.', ','));
                subtotal = float.Parse(textBox8.Text.Replace('.', ','));

                if (subtotal != 0)
                {
                    textBox11.Text = ((importe * 100) / subtotal).ToString();
                }


            }
            catch { }

            //sumar recargos
            total += recargos;
            //restar descuentos
            total -= descuentos;

            label5.Text = total.ToString();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float numero;
                textBox3.Text = textBox3.Text.Replace('.', ',');
                if (textBox1.Text.Trim() == "") { textBox1.Focus(); return; }
                if (!float.TryParse(textBox3.Text, out numero)) { textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(); }
                if (textBox3.Text == "" || float.Parse(textBox3.Text) < 0) { textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(); }

                try
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value = textBox3.Text;
                    float preciou = 0;
                    preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                    calcularTotal();
                    //si estoy en la ultima fila, tengo que agregar una mas. sino, tengo que ir a la ultima
                    if (dataGridView1.CurrentRow.Index == (dataGridView1.Rows.Count - 1))
                    {
                        nuevaFila();
                    }
                    else
                    {
                        acomodarControles(dataGridView1.Rows.Count - 1);
                    }
                }
                catch
                {
                    textBox3.Focus();
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //buscar cliente por nombre
                if (dbCon.IsConnect())
                {
                    string query = "select * from clientes where clirazonsocial='" + textBox5.Text + "'";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    textBox4.Text = "";

                    while (reader.Read())
                    {
                        textBox4.Text = reader.GetString(0);
                       
                        textBox1.Focus();
                    }
                    reader.Close();
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //buscar cliente por codigo
                if (dbCon.IsConnect())
                {
                    string query = "select * from clientes where clicodigo=" + textBox4.Text;
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        textBox5.Text = "";
                        textBox5.Text = reader.GetString(1);
                        
                        textBox1.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Cliente no encontrado", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox4.Text = "1";
                        textBox5.Text = "CONSUMIDOR FINAL";
                        textBox4.SelectAll();
                    }
                    reader.Close();
                }
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.SelectAll();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.SelectAll();
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        private void borrarTodo()
        {
            textBox5.Text = "CONSUMIDOR FINAL";
            textBox4.Text = "1";
            dataGridView1.Rows.Clear();
            label5.Text = "0,00";
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            textBox10.Text = "0";
            textBox11.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0,00";
            textBox6.Text = "0";
            nuevaFila();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            borrarTodo();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
 foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
            calcularTotal();
            acomodarControles(dataGridView1.Rows.Count - 1);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            long numero; int fila = dataGridView1.CurrentRow.Index; float preciou = 0;
            if (textBox1.Text.Trim() == "") { return; }
            textBox1.Text = textBox1.Text.Replace("*", string.Empty);
            if (!(long.TryParse(textBox1.Text, out numero)))
            {
                string codigo = "";
                string query = "select productos.*,productolista.importe from productos inner join productolista on productolista.idproducto=productos.procodigo where probaja=0 and prodescripcion='" + textBox1.Text + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();
                    codigo = reader.GetString(0);
                    dataGridView1.Rows[fila].Cells[0].Value = reader.GetString(1); // codigo
                    dataGridView1.Rows[fila].Cells[1].Value = reader.GetString(2); //descripcion
                    dataGridView1.Rows[fila].Cells[2].Value = "1"; //cant
                    dataGridView1.Rows[fila].Cells[3].Value = (double.Parse(reader.GetString(15))).ToString("0.##"); //pu
                    dataGridView1.Rows[fila].Cells[4].Value = (double.Parse(reader.GetString(15))).ToString("0.##"); //subtotal
                    dataGridView1.Rows[fila].Cells[5].Value = reader.GetString(0); //id
                    dataGridView1.Rows[fila].Cells[6].Value = reader.GetString(3); //stock
                    dataGridView1.Rows[fila].Cells[7].Value = reader.GetString(6); //iva
                    

                    textBox1.Text = reader.GetString(1);
                    textBox2.Text = "1";
                    textBox3.Text = (double.Parse(reader.GetString(15))).ToString("0.##");
                    reader.Close();
                    preciou = 0;
                    preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                    //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    calcularTotal();
                    //textBox2.Focus();
                }
                else
                {
                    reader.Close();
                    textBox1.Focus();
                    textBox1.SelectAll();
                }
            }
            else
            {
                //buscar por codigo interno o codigo de barras
                if (dbCon.IsConnect())
                {
                    string codigo = "";
                    string query = "select productos.*,productolista.importe from productos inner join productolista on productolista.idproducto=productos.procodigo where probaja=0 and procodigo=" + textBox1.Text;
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        codigo = reader.GetString(0);
                        dataGridView1.Rows[fila].Cells[0].Value = reader.GetString(1); // codigo
                        dataGridView1.Rows[fila].Cells[1].Value = reader.GetString(2); //descripcion
                        dataGridView1.Rows[fila].Cells[2].Value = "1"; //cant
                        dataGridView1.Rows[fila].Cells[3].Value = (double.Parse(reader.GetString(15))).ToString("0.##"); //pu
                        dataGridView1.Rows[fila].Cells[4].Value = (double.Parse(reader.GetString(15))).ToString("0.##"); //subtotal
                        dataGridView1.Rows[fila].Cells[5].Value = reader.GetString(0); //id
                        dataGridView1.Rows[fila].Cells[6].Value = reader.GetString(3); //stock
                        dataGridView1.Rows[fila].Cells[7].Value = reader.GetString(6); //iva


                        textBox1.Text = reader.GetString(1);
                        textBox2.Text = "1";
                        textBox3.Text = (double.Parse(reader.GetString(15))).ToString("0.##");
                        reader.Close();
                        preciou = 0;
                        preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                        //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        calcularTotal();
                        //textBox2.Focus();
                    }
                    else
                    {
                        reader.Close();
                        query = "select productos.*,productolista.importe from productos inner join productolista on productolista.idproducto=productos.procodigo where probaja=0 and procodbar='" + textBox1.Text + "'";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            codigo = reader.GetString(0);
                            dataGridView1.Rows[fila].Cells[0].Value = reader.GetString(1); // codigo
                            dataGridView1.Rows[fila].Cells[1].Value = reader.GetString(2); //descripcion
                            dataGridView1.Rows[fila].Cells[2].Value = "1"; //cant
                            dataGridView1.Rows[fila].Cells[3].Value = (double.Parse(reader.GetString(15))).ToString("0.##"); //pu
                            dataGridView1.Rows[fila].Cells[4].Value = (double.Parse(reader.GetString(15))).ToString("0.##"); //subtotal
                            dataGridView1.Rows[fila].Cells[5].Value = reader.GetString(0); //id
                            dataGridView1.Rows[fila].Cells[6].Value = reader.GetString(3); //stock
                            dataGridView1.Rows[fila].Cells[7].Value = reader.GetString(6); //iva


                            textBox1.Text = reader.GetString(1);
                            textBox2.Text = "1";
                            textBox3.Text = (double.Parse(reader.GetString(15))).ToString("0.##");
                            reader.Close();
                            preciou = 0;
                            preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                            calcularTotal();
                            //textBox2.Focus();
                        }
                        else
                        {
                           
                                reader.Close();
                                textBox1.Focus();
                                textBox1.SelectAll();
                            
                        }
                    }
                    reader.Close();
                    //textBox1.Focus();
                    //textBox1.SelectAll();
                }

            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    //calcular porcentaje de descuento
                    float importe = float.Parse(textBox7.Text.Replace('.', ','));
                    float subtotal = float.Parse(textBox8.Text.Replace('.', ','));
                    textBox10.Text = ((importe * 100) / subtotal).ToString();
                    calcularTotal();
                }
                catch { }
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    //calcular importe de descuento
                    float porcentaje = float.Parse(textBox10.Text.Replace('.', ','));
                    float subtotal = float.Parse(textBox8.Text.Replace('.', ','));
                    textBox7.Text = ((porcentaje * subtotal) / 100).ToString();
                    calcularTotal();
                }
                catch { }
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            else if (Char.IsSeparator(e.KeyChar))
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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            else if (Char.IsSeparator(e.KeyChar))
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                
                label18.Visible = false;
                comboBox3.Visible = false;
                calcularTotal();
            }
            else
            {
                label18.Visible = true;
                comboBox3.Visible = true;
                comboBox3.Items.Clear();
                //cargar cuotas
                string query = "select * from planestarjetas where idtarjeta=" + dbCon.extraerCodigo(comboBox1.Text);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox3.Items.Add(reader.GetString(0) + "- PLAN " + reader.GetString(2) + " CUOTAS");
                }
                reader.Close();
                comboBox3.SelectedIndex = 0;
                calcularinteres();
            }
        }

        private void calcularinteres()
        {
            try
            {
                string aumento = "0";
                string query = "select * from planestarjetas where idplan=" + dbCon.extraerCodigo(comboBox3.Text);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox11.Text = reader.GetString(3);
                }
                reader.Close();

                //calcular importe de recargo
                float porcentaje = float.Parse(textBox11.Text.Replace('.', ','));
                float subtotal = float.Parse(textBox8.Text.Replace('.', ','));
                textBox6.Text = ((porcentaje * subtotal) / 100).ToString();
                calcularTotal();
            }
            catch { }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcularinteres();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            float iva21 = 0;float iva105 = 0; float subtotal = 0;float total = 0;
            float descuento = 0;float recargo = 0;
            var cmd = new MySqlCommand();
          
            total = float.Parse(label5.Text);
            descuento = float.Parse(textBox7.Text);
            recargo = float.Parse(textBox6.Text);
            //calcular IVA
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                cmd = new MySqlCommand("select proiva from productos where procodigo='"+ dataGridView1.Rows[i].Cells[5].Value.ToString() + "'", dbCon.Connection);
                var reader2 = cmd.ExecuteReader();
                while (reader2.Read())
                {
                    if(reader2.GetFloat(0)==21)
                    {
                        iva21 += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 1.21f;
                    }
                    else
                    {
                        iva105 += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 1.105f;
                    }
                }
                reader2.Close();
            }
            subtotal = float.Parse(textBox8.Text) - (iva21 + iva105);
            string tipocomprobante = "1";string tipocliente = "99";string doccliente = "80";string tipoempresa = "1";
            //obtener el iva y el documento del cliente. 
            cmd = new MySqlCommand("select clitipodoc, clitipo from clientes where clicodigo="+textBox4.Text, dbCon.Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tipocliente = reader.GetString(1);
                doccliente= reader.GetString(0);
            }
            reader.Close();
            //obtener el tipo de empresa
            cmd = new MySqlCommand("select tipo from config", dbCon.Connection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tipoempresa = reader.GetString(0);
            }
            reader.Close();
            //ahora fijarse que tipo de comprobante tengo que emitir
            switch(tipocliente)
            {
                case "1"://cliente es responsable inscripto
                    switch(tipoempresa)
                    {
                        case "1":tipocomprobante = "1";break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "2"://cliente es responsable NO inscripto
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "6"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "3"://cliente es NO responsable
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "6"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "4"://cliente es IVA EXENTO
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "6"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "5"://cliente es CONSUMIDOR FINAL
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "6"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "6"://cliente es RESP MONOTRIBUTO
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "6"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "7"://cliente es NO CATEGORIZADO
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "6"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "11"://cliente es IVA EXENTO
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "1"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
                case "14"://cliente es IVA EXENTO
                    switch (tipoempresa)
                    {
                        case "1": tipocomprobante = "6"; break;
                        case "6": tipocomprobante = "11"; break;
                        case "4": tipocomprobante = "11"; break;
                    }
                    break;
            }
            string operacion = "EGRESO";
            //si es nota de credito, cambiar de tipocomprobante
            if (comboBox2.Text != "Venta")
            {
                tipocomprobante = (int.Parse(tipocomprobante) + 2).ToString();
                /*subtotal = subtotal * -1;
                total = subtotal * -1;
                iva105 = subtotal * -1;
                iva21 = subtotal * -1;*/
                operacion = "INGRESO";
            }
            string fecha = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            //GUARDAR VENTA
            if (total!=0)
            {
                   cmd = new MySqlCommand("INSERT INTO `ventas`(`faccodigo`, `facnumero`, `pdv`, `factipo`, `facfecha`, `clicodigo`, `facsubtotal`, `iva105`, `iva21`, `facdescuento`,`facrecargo`, `factotal`, `vendedor`, `facestado`, `cae`, `vencimiento`, `tipopago`) VALUES(NULL,0,0," + tipocomprobante+",'"+fecha+"',"+textBox4.Text+",'"+subtotal.ToString().Replace(',','.')+ "','" + iva105.ToString().Replace(',', '.') + "','" + iva21.ToString().Replace(',', '.') + "','" + descuento.ToString().Replace(',', '.') + "','" + recargo.ToString().Replace(',', '.') + "','" + total.ToString().Replace(',', '.') + "',"+idUsuario+",0,'','','"+comboBox1.Text+"//"+comboBox3.Text+"')", dbCon.Connection);
                   cmd.ExecuteNonQuery();
                long codigo= cmd.LastInsertedId;
                //guardar detalle de venta, descontar stock y generar kardex
                float piva = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() != "")
                    {
                        piva = (float.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString()) / 100) + 1;
                        cmd = new MySqlCommand("INSERT INTO `detalleventas`(`faccodigo`, `procodigo`, `prodescripcion`, `cantidad`, `preciounitario`, `proiva`, `subtotal`) VALUES (" + codigo.ToString() + "," + dataGridView1.Rows[i].Cells[5].Value.ToString() + ",'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(',', '.') + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(',', '.') + "','" + (float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / piva).ToString().Replace(',', '.') + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString().Replace(',', '.') + "')", dbCon.Connection);
                        cmd.ExecuteNonQuery();
                        //kardex
                        string query = "insert into kardex (kardexid,comprobante,numerocomprobante,operacion,fecha,procodigo,prodescripcion,cantidad) values (NULL,'" + tipocomprobante + "'," + codigo.ToString() + ",'" + operacion + "','" + fecha + "'," + dataGridView1.Rows[i].Cells[5].Value.ToString() + ",'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" +  dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(',', '.') + "')";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.ExecuteNonQuery();
                        //descuento stock
                        if (operacion == "EGRESO")
                        {
                            query = "update productos set prostock=prostock-" + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(',', '.') + " where procodigo=" + dataGridView1.Rows[i].Cells[5].Value.ToString();
                        }
                        else
                        {
                            query = "update productos set prostock=prostock+" + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(',', '.') + " where procodigo=" + dataGridView1.Rows[i].Cells[5].Value.ToString();
                        }
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.ExecuteNonQuery();
                    }
                }
                //generar ctacte en cliente
                if(operacion=="EGRESO") //es una venta
                {
                    cmd = new MySqlCommand("INSERT INTO `ctacte`(`idctacte`, `clicodigo`, `fecha`, `numcomprobante`, `descripcion`, `debe`, `haber`) VALUES (NULL,"+textBox4.Text+",'"+fecha+"',"+codigo.ToString()+",'Venta','"+total.ToString().Replace(',','.')+"',0)", dbCon.Connection);
                    cmd.ExecuteNonQuery();
                    //abrir cobrador
                    Cobrador cobranza = new Cobrador(total,codigo.ToString(),textBox4.Text,idUsuario);
                    cobranza.ShowDialog();
                }
                else
                {
                    cmd = new MySqlCommand("INSERT INTO `ctacte`(`idctacte`, `clicodigo`, `fecha`, `numcomprobante`, `descripcion`, `debe`, `haber`) VALUES (NULL," + textBox4.Text + ",'" + fecha + "'," + codigo.ToString() + ",'Nota de Credito',0,'" + Math.Abs(total).ToString().Replace(',', '.') + "')", dbCon.Connection);
                    cmd.ExecuteNonQuery();
                }
                borrarTodo();
               
            }

            
        }
    }
}
