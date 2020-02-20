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
using System.Security.Cryptography;

namespace sistemaGestion
{
    public partial class ABMUsuarios : Form
    {
        Conexion dbCon = Conexion.Instance();
        public ABMUsuarios()
        {
            InitializeComponent();
        }

        private void ABMUsuarios_Load(object sender, EventArgs e)
        {
            cargarLista();
        }

        private void cargarLista()
        {
            listBox1.Items.Clear();
            var cmd = new MySqlCommand("select * from usuarios where tipo<>0", dbCon.Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    listBox1.Items.Add(reader.GetString(0)+"-"+reader.GetString(1));
                }
                catch { }
            }
            reader.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            bunifuCards1.Text = "";
            bunifuCards1.Enabled = true;
            bunifuCards5.Enabled = false;

            textBox2.Text = "";
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            textBox1.Focus();
            label4.Visible = false;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
            bunifuFlatButton1.Enabled = false;
            bunifuCards5.Enabled = false;
            bunifuCards1.Enabled = true;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            label4.Visible = true;
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
            if (textBox1.Text.Trim() == "") { SetError(textBox1, "Campo Requerido"); return; }
            if (textBox2.Text.Trim() == "") { SetError(textBox2, "Campo Requerido"); return; }
            if (bunifuCards1.Text == "") //es nuevo
            {
                var cmd = new MySqlCommand("insert into usuarios (idusuario,nombre,pass,tipo) values (NULL,'" + textBox1.Text + "','" + MD5Hash(textBox2.Text) + "','" + (comboBox1.SelectedIndex + 1) + "')", dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
            else //es modificacion
            {
                if (textBox2.Text.Trim() != "")
                {
                    if (MessageBox.Show("Desea modificar la contraseña de este usuario?", "Contraseña", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var cmd = new MySqlCommand("update usuarios set nombre='" + textBox1.Text + "',pass='" + MD5Hash(textBox2.Text) + "',tipo='" + (comboBox1.SelectedIndex + 1) + "' where idusuario=" + bunifuCards1.Text, dbCon.Connection);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    { return; }
                }
                else
                {
                    var cmd = new MySqlCommand("update usuarios set nombre='" + textBox1.Text + "',tipo='" + (comboBox1.SelectedIndex + 1) + "' where idusuario=" + bunifuCards1.Text, dbCon.Connection);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Datos guardados correctamente!", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bunifuFlatButton2_Click(this, new EventArgs());
            cargarLista();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuCards5.Enabled = true;
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            bunifuFlatButton3.Enabled = false;
            bunifuCards1.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            textBox1.Focus();
            label4.Visible = false;
            bunifuCards1.Enabled = false;
        }

        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
