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
    public partial class Cobrador : Form
    {
        float montoacobrar; string comprobante; string idCliente; string idUsuario;
        Conexion dbCon = Conexion.Instance();
        public Cobrador(float monto,string comp,string cliente,string usuario)
        {
            InitializeComponent();
            montoacobrar = monto;
            comprobante = comp;
            idCliente = cliente;
            idUsuario = usuario;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cobrador_Load(object sender, EventArgs e)
        {
            textBox1.Text = montoacobrar.ToString();
            textBox1.SelectAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float recibo = float.Parse(textBox1.Text);
                textBox2.Text = (recibo - montoacobrar).ToString("0.##");
            }
            catch
            {
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (float.Parse(textBox1.Text) <= 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    insertarCuenta();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
                if (float.Parse(textBox1.Text) <=0)
                {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
                else
                {
                insertarCuenta();
                this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            
        }

        private void insertarCuenta()
        {
            string fecha = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            var cmd = new MySqlCommand("INSERT INTO `ctacte`(`idctacte`, `clicodigo`, `fecha`, `numcomprobante`, `descripcion`, `debe`, `haber`) VALUES (NULL," + idCliente + ",'" + fecha + "'," + comprobante+ ",'Cobranza',0,'" + (float.Parse(textBox1.Text)- float.Parse(textBox2.Text)).ToString().Replace(',', '.') + "')", dbCon.Connection);
            cmd.ExecuteNonQuery();
            if ((float.Parse(textBox1.Text) - float.Parse(textBox2.Text)) >= montoacobrar)
            {
                cmd= new MySqlCommand("update ventas set facestado=1 where faccodigo="+comprobante, dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
