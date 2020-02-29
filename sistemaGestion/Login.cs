using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace sistemaGestion
{
    public partial class Login : Form
    {
        Conexion dbCon = Conexion.Instance();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            bunifuCards2.Location = bunifuCards1.Location;
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from vendedores where activo=1", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bunifuDropdown1.AddItem(reader.GetString(1));
                }
                reader.Close();
            }
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

      
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            
           
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            if (bunifuDropdown1.selectedValue.Trim() == "") { bunifuCards1.color = Color.Firebrick; return; }
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from vendedores where VendNombre='" + bunifuDropdown1.selectedValue + "'", dbCon.Connection);
                var reader = cmd.ExecuteReader();
               if(reader.HasRows)
                {
                    bunifuCards1.color = Color.FromArgb(51,122,183);
                    bunifuCustomLabel1.Visible = false;
                    bunifuTransition1.HideSync(bunifuCards1,false);
                    bunifuTransition1.ShowSync(bunifuCards2,false);
                    
                    bunifuMaterialTextbox2.Text = "";
                    bunifuMaterialTextbox2.Focus();
                }
               else
                {
                    bunifuCards1.color = Color.FromArgb(217, 83, 79);
                    bunifuCustomLabel1.Visible = true;
                    bunifuDropdown1.Focus();
                }
                reader.Close();
            }
            }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            string pass = ""; string nombreusuario = ""; string tipousuario = ""; string idusuario = "";
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from Vendedores where VendNombre='" + bunifuDropdown1.selectedValue + "'", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pass = reader.GetString(3);
                    nombreusuario = reader.GetString(1);
                    tipousuario = reader.GetString(6);
                    idusuario = reader.GetString(0);
                }
                reader.Close();
                if (pass == bunifuMaterialTextbox2.Text)
                {
                    Form1 principal = new Form1(nombreusuario, tipousuario, idusuario);
                    principal.Show();
                    this.Hide();
                }
                else
                {
                    bunifuCards2.color = Color.FromArgb(217, 83, 79);
                    bunifuCustomLabel2.Visible = true;
                    bunifuMaterialTextbox2.Focus();
                    return;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bunifuTransition1.HideSync(bunifuCards2,false);
            bunifuTransition1.ShowSync(bunifuCards1,false);
            bunifuMaterialTextbox2.Text = "";
            bunifuDropdown1.Focus();
        }

        private void bunifuMaterialTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                bunifuFlatButton1_Click_1(this, new EventArgs());
            }
        }

        private void bunifuMaterialTextbox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuFlatButton3_Click(this, new EventArgs());
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {
            bunifuFlatButton1_Click_1(this, e);
        }
    }
}
