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
    public partial class Vencimientos : Form
    {
        Conexion dbCon = Conexion.Instance();
        Form1 principal;
        public Vencimientos(Form1 p)
        {
            InitializeComponent();
            principal = p;
           
        }

        private void Vencimientos_Load(object sender, EventArgs e)
        {
            cargarlista();
        }

        private void cargarlista()
        {
            dataGridView1.Rows.Clear();
            try {
                if (dbCon.IsConnect())
                {
                    string query = "select  compras.*,proveedores.provrazonsocial from compras inner join proveedores on proveedores.provcodigo=compras.provcodigo where estado ='0' order by fechavenc asc";
                    var cmd = new MySqlCommand("select count(*) from compras where estado ='0' order by fechavenc asc", dbCon.Connection);
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
                        fila = dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(11), reader.GetString(8), reader.GetString(1).Remove(10), reader.GetString(9).Remove(10));
                        var parameterDate = DateTime.ParseExact(reader.GetString(9).Remove(10), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        var todaysDate = DateTime.Today;

                        if (parameterDate <= todaysDate)
                        {
                            dataGridView1.Rows[fila].DefaultCellStyle.BackColor = Color.FromArgb(248, 215, 218);
                            dataGridView1.Rows[fila].DefaultCellStyle.ForeColor = Color.FromArgb(114, 28, 36);
                            dataGridView1.Rows[fila].DefaultCellStyle.SelectionBackColor = Color.FromArgb(114, 28, 36);
                            dataGridView1.Rows[fila].DefaultCellStyle.SelectionForeColor = Color.FromArgb(248, 215, 218);
                        }
                        else
                        {
                            dataGridView1.Rows[fila].DefaultCellStyle.BackColor = Color.FromArgb(212, 237, 218);
                            dataGridView1.Rows[fila].DefaultCellStyle.ForeColor = Color.FromArgb(21, 87, 36);
                            dataGridView1.Rows[fila].DefaultCellStyle.SelectionForeColor = Color.FromArgb(212, 237, 218);
                            dataGridView1.Rows[fila].DefaultCellStyle.SelectionBackColor = Color.FromArgb(21, 87, 36);
                        }
                        
                    }
                    reader.Close();
                    principal.toolStripProgressBar1.Visible = false;
                }

            }
            catch { principal.toolStripProgressBar1.Visible = false; }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var cmd = new MySqlCommand("upadte compras set estado=1 where comcodigo="+dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), dbCon.Connection);
            cmd.ExecuteNonQuery();
            cargarlista();
        }
    }
}
