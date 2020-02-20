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
    public partial class ListadoFacturas : Form
    {
        Conexion dbCon = Conexion.Instance();
        //impFiscal fiscal = new impFiscal();
        facElectronica elec = new facElectronica(Application.StartupPath);
        Form1 principal;
        public ListadoFacturas(Form1 p)
        {
            InitializeComponent();
            principal = p;
            //fiscal.Progress += Fiscal_Progress;
        }

        private void Fiscal_Progress(int obj)
        {
            principal.toolStripProgressBar1.Visible = true;
            principal.toolStripProgressBar1.Value = obj;
            Application.DoEvents();
            if (obj == 100) { principal.toolStripProgressBar1.Visible = false; }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            cargarventas();
        }

        private void cargarventas()
        {
            dataGridView1.Rows.Clear();
            string query = "select ventas.*,clientes.clirazonsocial,tiposcomprobante.nombre from ventas inner join clientes on clientes.clicodigo=ventas.clicodigo inner join tiposcomprobante on tiposcomprobante.codigo=ventas.factipo where (facfecha>='" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + " 00:00:00' and facfecha<='" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + " 23:59:59') order by faccodigo asc";
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(18), reader.GetString(4), reader.GetString(17), reader.GetString(11), reader.GetString(2), reader.GetString(1),reader.GetString(16));
                }
                reader.Close();
            }
        }

        private void ListadoFacturas_Load(object sender, EventArgs e)
        {
           /* if (fiscal.Initialize())
            {
                //ribbonPanel14.Visible = true;
            }
            else
            {
                MessageBox.Show(fiscal.getLastError(), "Fiscal");
            }
            if (!fiscal.isAvailable()) { bunifuFlatButton2.Enabled = false; bunifuFlatButton6.Enabled = false; }*/
            if (!elec.isAvailable()) { bunifuFlatButton3.Enabled = false; }

            dateTimePicker1.Value = DateTime.Now;
            cargarventas();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            principal.toolStripProgressBar1.Maximum = 100;
            principal.toolStripProgressBar1.Value = 0;
            //verificar que la venta no tenga un numero asociado
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString() != "0")
            {
                MessageBox.Show("Este comprobante ya se encuentra impreso.", "Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                /*if (!fiscal.Print(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()))
                { MessageBox.Show(fiscal.getLastError(), "Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);return; }
                else
                {
                    var cmd = new MySqlCommand("update ventas set facnumero="+fiscal.getLastId()+",pdv="+fiscal.getPdV()+" where faccodigo="+ dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(), dbCon.Connection);
                    cmd.ExecuteNonQuery();
                    cargarventas();
                }*/
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
           /* if (!fiscal.cierreZ())
            { MessageBox.Show(fiscal.getLastError(), "Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }*/
        }
    }
}
