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
    public partial class Informes : Form
    {
        Form1 principal;
        Conexion dbCon = Conexion.Instance();
        public Informes(Form1 p,int pestana)
        {
            InitializeComponent();
            principal = p;
            tabControl1.SelectTab(pestana);
        }

        private void Informes_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection col = new
         AutoCompleteStringCollection();
            AutoCompleteStringCollection col2 = new
         AutoCompleteStringCollection();
            AutoCompleteStringCollection col3 = new
       AutoCompleteStringCollection();
            if (dbCon.IsConnect())
            {
                string query = "select prodescripcion from productos where probaja=0";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    col.Add(reader.GetString(0));
                }
                reader.Close();

                 query = "select clirazonsocial from clientes";
                 cmd = new MySqlCommand(query, dbCon.Connection);
                 reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    col2.Add(reader.GetString(0));
                }
                reader.Close();

                query = "select provrazonsocial from proveedores";
                cmd = new MySqlCommand(query, dbCon.Connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    col3.Add(reader.GetString(0));
                }
                reader.Close();
            }
            
        
            textBox1.AutoCompleteCustomSource = col2;
            textBox3.AutoCompleteCustomSource = col2;
            textBox2.AutoCompleteCustomSource = col;
            textBox4.AutoCompleteCustomSource = col3;
            textBox5.AutoCompleteCustomSource = col3;

        }

        private bool validarCliente(Control text, Control label)
        {
            errorProvider1.Clear();
            int numero;

            if (!(int.TryParse(textBox1.Text, out numero)))
            {
                string codigo = "";
                string query = "select clicodigo from clientes where clirazonsocial='" + text.Text + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();
                    codigo = reader.GetString(0);
                    reader.Close();
                    label.Text = codigo;
                    return true;
                }
                return true;
            }
            else
            {
                //buscar por codigo interno o codigo de barras
                if (dbCon.IsConnect())
                {
                    string codigo = "";
                    string query = "select clicodigo from clientes where clicodigo=" + text.Text;
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        codigo = reader.GetString(0);
                        reader.Close();
                        label.Text = codigo;
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        errorProvider1.SetError(text, "No se encuentra el cliente!");
                        return false;
                    }
                }

            }
            return true;
        }

        private bool validarProveedor(Control text, Control label)
        {
            errorProvider1.Clear();
            int numero;

            if (!(int.TryParse(textBox1.Text, out numero)))
            {
                string codigo = "";
                string query = "select provcodigo from proveedores where provrazonsocial='" + text.Text + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();
                    codigo = reader.GetString(0);
                    reader.Close();
                    label.Text = codigo;
                    return true;
                }
                return true;
            }
            else
            {
                //buscar por codigo interno o codigo de barras
                if (dbCon.IsConnect())
                {
                    string codigo = "";
                    string query = "select provcodigo from proveedores where provrazonsocial=" + text.Text;
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        codigo = reader.GetString(0);
                        reader.Close();
                        label.Text = codigo;
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        errorProvider1.SetError(text, "No se encuentra el proveedor!");
                        return false;
                    }
                }

            }
            return true;
        }

        private bool validarProducto(Control text,Control label)
        {
            errorProvider1.Clear();
            int numero;

            if (!(int.TryParse(textBox2.Text, out numero)))
            {
                string codigo = "";
                string query = "select procodigo from productos where prodescripcion='" + text.Text + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();
                    codigo = reader.GetString(0);
                    reader.Close();
                    label.Text = codigo;
                    return true;
                }
                return true;
            }
            else
            {
                //buscar por codigo interno o codigo de barras
                if (dbCon.IsConnect())
                {
                    string codigo = "";
                    string query = "select procodigo from productos where procodigo=" + text.Text;
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        codigo = reader.GetString(0);
                        reader.Close();
                        label.Text = codigo;
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        query = "select procodigo from productos where procodbar='" + text.Text + "'";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            codigo = reader.GetString(0);
                            reader.Close();
                            label.Text = codigo;
                            return true;
                        }
                        else
                        {
                            errorProvider1.SetError(text, "No se encuentra el producto!");
                            return false;
                        }
                    }
                    reader.Close();
                    errorProvider1.SetError(text, "No se encuentra el producto!");
                    return false;
                }

            }
            return true;
        }
    }
}
