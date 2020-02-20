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
using System.IO;

namespace sistemaGestion
{
    public partial class Configuracion : Form
    {
        Conexion dbCon = Conexion.Instance();
        public Configuracion(int pestana)
        {
            InitializeComponent();
            tabControl1.SelectTab(pestana);
        }

        private void Configuracion_Load(object sender, EventArgs e)
        {
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select config.*,tipoiva.nombre from config inner join tipoiva on tipoiva.codigo=config.tipo", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Text = reader.GetString(3) + "-" + reader.GetString(14);
                    textBox1.Text = reader.GetString(2);
                    textBox2.Text = reader.GetString(4);
                    textBox3.Text = reader.GetString(5);
                    textBox4.Text = reader.GetString(6);
                    textBox5.Text = reader.GetString(7);
                    textBox6.Text = reader.GetString(8);
                    try
                    {
                        var data = (Byte[])reader.GetValue(1);
                        var stream = new MemoryStream(data);
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                    catch { }

                    textBox11.Text = reader.GetString(11);
                    textBox12.Text = reader.GetString(9);
                    if (reader.GetString(12) == "1") { bunifuiOSSwitch1.Value = true; }else { bunifuiOSSwitch1.Value = false; }
                    
                    textBox13.Text = reader.GetString(10);
                    if (reader.GetString(13) == "1") { bunifuiOSSwitch2.Value = true; } else { bunifuiOSSwitch2.Value = false; }

                }
                reader.Close();
                StreamReader arch = new StreamReader(Application.StartupPath + "\\servidorSQL.ini");

                textBox10.Text = arch.ReadLine();
                textBox9.Text = arch.ReadLine();
                textBox8.Text = arch.ReadLine();
                textBox7.Text = arch.ReadLine();
                arch.Close();
                if(File.Exists(Application.StartupPath + "\\certificado.crt")) { textBox14.Text = Application.StartupPath + "\\certificado.crt"; }
                if (File.Exists(Application.StartupPath + "\\clave.key")) { textBox14.Text = Application.StartupPath + "\\clave.key"; }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de Imagen Compatibles|*.jpg;*.png;*.bmp;*.jpeg;*.gif|Archivos BMP (*.bmp)|*.bmp|Archivos JPG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Archivos PNG (*.png)|*.png|Archivos GIF (*.gif)|*.gif";
            openFileDialog1.Title = "Imagen de Fondo";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label8.Text = openFileDialog1.FileName;
                pictureBox1.Load(label8.Text);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            var cmd = new MySqlCommand("update config set razonsocial='"+textBox1.Text+"',tipo='"+dbCon.extraerCodigo(comboBox1.Text)+"',cuit='"+textBox2.Text+ "',direccion='" + textBox3.Text + "',telefono='" + textBox4.Text + "',email='" + textBox5.Text + "',web='" + textBox6.Text + "'", dbCon.Connection);
            cmd.ExecuteNonQuery();
            if (label8.Text != "")
            {
                System.IO.FileStream fs = new FileStream(label8.Text, FileMode.Open);
                System.IO.BufferedStream bf = new BufferedStream(fs);
                byte[] buffer = new byte[bf.Length];
                bf.Read(buffer, 0, buffer.Length);

                byte[] buffer_new = buffer;
                MySqlCommand command = new MySqlCommand("", dbCon.Connection);
                command.CommandText = "update config set imgLogo=@image;";

                command.Parameters.AddWithValue("@image", buffer_new);

                command.ExecuteNonQuery();
            }
            MessageBox.Show("Guardado", "Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            int fiscalhabil = 0;int elechabil = 0;
            if (bunifuiOSSwitch1.Value == true) { fiscalhabil = 1; }
            if (bunifuiOSSwitch2.Value == true) { elechabil = 1; }
            var cmd = new MySqlCommand("update config set pdvfiscal='" + textBox12.Text + "',pdvelectronico='" + textBox13.Text + "',puertofiscal='" + textBox11.Text + "',fiscalhabil='" + fiscalhabil+ "',elechabil='" +elechabil + "'", dbCon.Connection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Guardado", "Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (testDB())
            {
                MessageBox.Show("La conexion funciona correctamente!", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La conexion ha devuelto un error. Verifique los datos ingresados y que el motor de MySQL esté funcionando.", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool testDB()
        {
            var conn_info = "Server=" + textBox10.Text + ";Port=3306;Database=" + textBox9.Text + ";Uid=" + textBox8.Text + ";Pwd=" + textBox7.Text;
            bool isConn = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(conn_info);
                conn.Open();
                isConn = true;
            }
            catch (ArgumentException a_ex)
            {

            }
            catch (MySqlException ex)
            {
                isConn = false;
                switch (ex.Number)
                {

                    case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        break;
                    case 0: // Access denied (Check DB name,username,password)
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConn;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter(Application.StartupPath + "\\servidorSQL.ini", false);
            write.WriteLine(textBox10.Text);
            write.WriteLine(textBox9.Text);
            write.WriteLine(textBox8.Text);
            write.WriteLine(textBox7.Text);
            write.Close();
            MessageBox.Show("Guardado\nEsta configuración tendrá efecto luego de reiniciar el sistema.", "Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
