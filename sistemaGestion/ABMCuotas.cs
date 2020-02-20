using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace sistemaGestion
{
    public partial class ABMCuotas : Form
    {
        Conexion dbCon = Conexion.Instance();
        public ABMCuotas()
        {
            InitializeComponent();
        }

        private void ABMCuotas_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from tarjetas", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0) + "-" + reader.GetString(1));

                }
                reader.Close();
            }
        }

        private void cargarplanes()
        {
            listBox1.Items.Clear();
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from planestarjetas where idtarjeta=" + dbCon.extraerCodigo(comboBox1.Text), dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader.GetString(0)+"-"+ reader.GetString(2)+" cuotas");

                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarplanes();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            bunifuFlatButton4.Enabled = false;
            textBox1.Enabled = true;
            comboBox2.Enabled = true;
            bunifuCards5.Enabled = false;
           
            bunifuFlatButton1.Enabled = false;
            
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            textBox1.Text = "";
            bunifuCards1.Text = "";
            textBox1.Focus();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuFlatButton4.Enabled = false;
            textBox1.Enabled = false;
            comboBox2.Enabled = false;
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            bunifuFlatButton3.Enabled = false;
            bunifuCards1.Text = "";
            textBox1.Text = "";
           
            bunifuCards5.Enabled = true;
           
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton4.Enabled = true;
            textBox1.Enabled = true;
            comboBox2.Enabled = true;
           
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
           
            bunifuCards5.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuCards1.Text = dbCon.extraerCodigo(listBox1.SelectedItem.ToString());
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from planestarjetas where idplan=" + bunifuCards1.Text, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Text = reader.GetString(2);
                    textBox1.Text = reader.GetString(3);
                }
                reader.Close();
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta acción es irreversible. Desea continuar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var cmd = new MySqlCommand("delete from planestarjetas where idplan=" + bunifuCards1.Text, dbCon.Connection);
                cmd.ExecuteNonQuery();
                cargarplanes();
                bunifuFlatButton2_Click(this, new EventArgs());
            }
        }

        private void clearError()
        {
            foreach (TextBox tb in bunifuCards1.Controls.OfType<TextBox>())
            {
                tb.BackColor = Color.White;
            }


        }

        private void SetError(Control control, string mensaje)
        {
            control.BackColor = Color.FromArgb(217, 83, 79);
            control.Focus();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            clearError();
            if (comboBox1.Text.Trim() == "") { MessageBox.Show("Se debe seleccionar una tarjeta primero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); bunifuFlatButton2_Click(this, new EventArgs()); return; }
            if (textBox1.Text.Trim() == "") { SetError(label1, "Debe indicar el porcentaje de interés"); return; }
            if (comboBox2.Text.Trim() == "") { SetError(label3, "Debe indicar las cuotas de este plan"); return; }

            string query = "";
            var cmd = new MySqlCommand();
            if (bunifuCards1.Text != "")
            {
                //modificacion
                query = "update planestarjetas set cuotas='" + comboBox2.Text + "',interes='" + textBox1.Text.Replace(',', '.') + "' where idplan=" + bunifuCards1.Text;
            }
            else
            {
                query = "insert into planestarjetas (idplan,idtarjeta,cuotas,interes) values (NULL," + dbCon.extraerCodigo(comboBox1.Text) + ",'" + comboBox2.Text + "','" + textBox1.Text.Replace(',', '.') + "');";
            }
            cmd = new MySqlCommand(query, dbCon.Connection);
            cmd.ExecuteNonQuery();
            cargarplanes();
            bunifuFlatButton2_Click(this, new EventArgs());
        }
    }
}
