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
    public partial class ABMClientes : Form
    {
        Conexion dbCon = Conexion.Instance();
        Form1 principal;
        public ABMClientes(Form1 p)
        {
            InitializeComponent();
            principal = p;
        }

        private void ABMClientes_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns.Add("razonsocial", "Razon Social");
            dataGridView1.Columns.Add("doc", "Doc.");
            dataGridView1.Columns.Add("telefono", "Telefono");
            dataGridView1.Columns.Add("direccion", "Direccion");
            if (dbCon.IsConnect())
            {
                
                 var cmd = new MySqlCommand("select * from resiva order by ivacodigo asc", dbCon.Connection);
                 var reader = cmd.ExecuteReader();
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
                        query = "select * from clientes where clirazonsocial like '" + textBox1.Text + "%'";
                        break;
                    //por nombre que incluya
                    case 1:
                        query = "select * from clientes where clirazonsocial like '%" + textBox1.Text.Replace(' ', '%') + "%'";
                        break;
                    //por cuit
                    case 2:
                        query = "select * from clientes where clinumdoc='" + textBox1.Text + "'";
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
                        fila = dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2)+"-"+reader.GetString(3), reader.GetString(9), reader.GetString(5)+" "+ reader.GetString(6)+" "+ reader.GetString(7));
                        
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
                string query = "select clientes.*,resiva.IvaDescripcion as tipoiva from clientes inner join resiva on resiva.ivacodigo=clientes.ivacodigo where clicodigo=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox2.Text = reader.GetString(1);//razon social
                    comboBox2.Text= reader.GetString(2); //tipo doc
                    textBox3.Text = reader.GetString(3); //numdoc
                    comboBox3.Text = reader.GetString(4) + "-" + reader.GetString(13); //tipo iva
                    textBox4.Text = reader.GetString(5); //dir
                    textBox5.Text = reader.GetString(6); //local
                    textBox6.Text = reader.GetString(7); //prov
                    textBox7.Text = reader.GetString(9); //tel
                    textBox8.Text = reader.GetString(10); //email
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
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            textBox2.Focus();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (bunifuCards1.Text != "")
            {
                bunifuCards1.Enabled = true;
                bunifuCards3.Enabled = true;
                bunifuCards5.Enabled = false;
                dataGridView1.Enabled = false;
                bunifuFlatButton1.Enabled = false;
                bunifuFlatButton2.Enabled = true;
                bunifuFlatButton3.Enabled = true;
            }
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
            bunifuCards1.Text = "";
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
            if (textBox5.Text == "") { SetError(textBox5, "Este campo no puede estar vacio"); return; }
            if (textBox6.Text == "") { SetError(textBox6, "Este campo no puede estar vacio"); return; }
            if (textBox7.Text == "") { SetError(textBox7, "Este campo no puede estar vacio"); return; }
            var cmd = new MySqlCommand();
            int codigo = dbCon.nuevoid("CliCodigo", "Clientes");
            string fechahoy = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            if (bunifuCards1.Text == "") //es nuevo
            {
                cmd = new MySqlCommand("INSERT INTO `clientes`(`CliCodigo`, `CliRazonSocial`, `CliTipoDoc`, `CliNumDoc`, `IvaCodigo`, `CliDireccion`, `CliLocalidad`, `CliProvincia`, `CliCPostal`, `CliTelefono`, `CliEmail`, `CliFechaAlta`, `CliTipoCliente`) VALUES ("+codigo.ToString()+",'" + textBox2.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + dbCon.extraerCodigo(comboBox3.Text) + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','','" + textBox7.Text + "','"+ textBox8.Text+"','"+fechahoy+"','')", dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new MySqlCommand("update clientes set clirazonsocial='" + textBox2.Text + "',clitipodoc='" + comboBox2.Text + "',CliNumDoc='" + textBox3.Text + "',clitelefono='" + textBox7.Text + "',clidireccion='" + textBox4.Text + "',clilocalidad='" + textBox5.Text + "',cliprovincia='" + textBox6.Text + "',IvaCodigo='" + dbCon.extraerCodigo(comboBox3.Text) + "',cliemail='"+textBox8.Text+"' where clicodigo="+bunifuCards1.Text, dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
            bunifuFlatButton2_Click(this, e);
            buscarprod();
        }
    }
}
