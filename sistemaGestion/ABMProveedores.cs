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
    public partial class ABMProveedores : Form
    {
        Conexion dbCon = Conexion.Instance();
        Form1 principal;
        public ABMProveedores(Form1 p)
        {
            InitializeComponent();
            principal = p;
        }

        private void ABMProveedores_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns.Add("razonsocial", "Razon Social");
            dataGridView1.Columns.Add("doc", "Doc.");
            dataGridView1.Columns.Add("telefono", "Telefono");
            dataGridView1.Columns.Add("direccion", "Direccion");
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from tipodocumentos order by codigo asc", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0) + "-" + reader.GetString(1));
                }
                reader.Close();
                cmd = new MySqlCommand("select * from tipoiva order by codigo asc", dbCon.Connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox3.Items.Add(reader.GetString(0) + "-" + reader.GetString(1));
                }
                reader.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscarprod();
            }
        }

        private void buscarprod()
        {
            if (textBox1.Text == "") { return; }
            dataGridView1.Rows.Clear();
            try
            {
                string query = "";
                switch (comboBox1.SelectedIndex)
                {
                    //por nombre que empiece
                    case 0:
                        query = "select * from proveedores where provrazonsocial like '" + textBox1.Text + "%'";
                        break;
                    //por nombre que incluya
                    case 1:
                        query = "select * from proveedores where provrazonsocial like '%" + textBox1.Text.Replace(' ', '%') + "%'";
                        break;
                    //por cuit
                    case 2:
                        query = "select * from proveedores where provcuit='" + textBox1.Text + "'";
                        break;

                }
                if (dbCon.IsConnect())
                {

                    var cmd = new MySqlCommand(query.Replace("*", "COUNT(*)"), dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    principal.toolStripProgressBar1.Maximum = reader.GetInt32(0);
                    principal.toolStripProgressBar1.Visible = true;
                    principal.toolStripProgressBar1.Value = 0;
                    Application.DoEvents();
                    reader.Close();
                    cmd = new MySqlCommand(query, dbCon.Connection);
                    reader = cmd.ExecuteReader();
                    int fila;

                    while (reader.Read())
                    {
                        principal.toolStripProgressBar1.Value++;
                        Application.DoEvents();
                        fila = dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(3), reader.GetString(5), reader.GetString(6));

                    }
                    reader.Close();
                    principal.toolStripProgressBar1.Visible = false;
                }

            }
            catch { principal.toolStripProgressBar1.Visible = false; }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dbCon.IsConnect())
            {
                string query = "select proveedores.*,tipodocumentos.nombre as tipodocumento,tipoiva.nombre as tipoiva from proveedores inner join tipodocumentos on tipodocumentos.codigo=proveedores.provtipodoc inner join tipoiva on tipoiva.codigo=proveedores.provtipo where provcodigo=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox2.Text = reader.GetString(1);//razon social
                    comboBox2.Text = reader.GetString(2) + "-" + reader.GetString(7); //tipo doc
                    textBox3.Text = reader.GetString(3); //numdoc
                    comboBox3.Text = reader.GetString(4) + "-" + reader.GetString(8); //tipo iva
                    textBox4.Text = reader.GetString(6); //dir
                   
                    textBox7.Text = reader.GetString(5); //tel
                    bunifuCards1.Text = reader.GetString(0);
                }
                reader.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bunifuCards1.Enabled = true;
            bunifuCards3.Enabled = true;
            bunifuCards5.Enabled = false;
            dataGridView1.Enabled = false;
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            bunifuCards1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            
            textBox7.Text = "";
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            textBox2.Focus();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            bunifuCards1.Enabled = true;
            bunifuCards3.Enabled = true;
            bunifuCards5.Enabled = false;
            dataGridView1.Enabled = false;
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
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
            bunifuCards1.Enabled = false;
            bunifuCards3.Enabled = false;
            bunifuCards5.Enabled = true;
            dataGridView1.Enabled = true;
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            bunifuFlatButton3.Enabled = false;
            clearError();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            clearError();
            if (textBox2.Text == "") { SetError(textBox2, "Este campo no puede estar vacio"); return; }
            if (textBox3.Text == "") { SetError(textBox3, "Este campo no puede estar vacio"); return; }
            if (textBox4.Text == "") { SetError(textBox4, "Este campo no puede estar vacio"); return; }
           
            if (textBox7.Text == "") { SetError(textBox7, "Este campo no puede estar vacio"); return; }
            var cmd = new MySqlCommand();
            if (bunifuCards1.Text == "") //es nuevo
            {
                cmd = new MySqlCommand("INSERT INTO `proveedores`(`provcodigo`, `provrazonsocial`, `provtipodoc`, `provcuit`, `provtipo`, `provtelefono`, `provdireccion`) VALUES (NULL,'" + textBox2.Text + "','" + dbCon.extraerCodigo(comboBox2.Text) + "','" + textBox3.Text + "','" + dbCon.extraerCodigo(comboBox3.Text) + "','" + textBox7.Text + "','" + textBox4.Text + "')", dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new MySqlCommand("update proveedores set provrazonsocial='" + textBox2.Text + "',provtipodoc='" + dbCon.extraerCodigo(comboBox2.Text) + "',provcuit='" + textBox3.Text + "',provtelefono='" + textBox7.Text + "',provdireccion='" + textBox4.Text + "',provtipo='" + dbCon.extraerCodigo(comboBox3.Text) + "' where provcodigo=" + bunifuCards1.Text, dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
            bunifuFlatButton2_Click(this, e);
            buscarprod();
        }
    }
}
