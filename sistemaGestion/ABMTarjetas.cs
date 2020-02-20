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
    public partial class ABMTarjetas : Form
    {
        Conexion dbCon = Conexion.Instance();
        public ABMTarjetas()
        {
            InitializeComponent();
        }

        private void ABMTarjetas_Load(object sender, EventArgs e)
        {
            cargarLista();
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

        private void cargarLista()
        {
            listBox1.Items.Clear();
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from tarjetas", dbCon.Connection);
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

                var cmd = new MySqlCommand("select * from tarjetas where idtarjeta=" + bunifuCards1.Text, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader.GetString(1);
                }
                reader.Close();
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton4.Enabled = true;
           
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;
            textBox1.Enabled = true;
            bunifuCards5.Enabled = false;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuFlatButton4.Enabled = false;
            textBox1.Enabled = false;
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            bunifuFlatButton3.Enabled = false;
            bunifuCards1.Text = "";
            textBox1.Text = "";
            textBox1.Focus();
            bunifuCards5.Enabled = false;
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta acción es irreversible. Desea continuar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var cmd = new MySqlCommand("delete from tarjetas where idtarjeta=" + bunifuCards1.Text + ";delete from planestarjetas where idtarjeta=" + bunifuCards1.Text + ";", dbCon.Connection);
                cmd.ExecuteNonQuery();
                cargarLista();
                bunifuFlatButton1_Click(this, new EventArgs());
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            clearError();
            if (textBox1.Text.Trim() == "") { SetError(label1, "La tarjeta debe tener un nombre"); return; }
            string query = "";
            var cmd = new MySqlCommand();
            if (bunifuCards1.Text != "")
            {
                //modificacion
                query = "update tarjetas set nombretarjeta='" + textBox1.Text + "' where idtarjeta=" + bunifuCards1.Text;
            }
            else
            {
                query = "insert into tarjetas (idtarjeta,nombretarjeta) values (NULL,'" + textBox1.Text + "');";
            }
            cmd = new MySqlCommand(query, dbCon.Connection);
            cmd.ExecuteNonQuery();
            cargarLista();
            bunifuFlatButton1_Click(this, new EventArgs());
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            bunifuFlatButton4.Enabled = false;
            textBox1.Enabled = true;
            bunifuCards5.Enabled = false;
           
            bunifuFlatButton1.Enabled = false;
           
           
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = false;
            textBox1.Text = "";
            bunifuCards1.Text = "";
            textBox1.Focus();
        }
    }
}
