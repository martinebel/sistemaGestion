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
    public partial class Kardex : Form
    {
        Conexion dbCon = Conexion.Instance();
        public Kardex()
        {
            InitializeComponent();
        }

        private void Kardex_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string query = "select * from kardex where (fecha>='" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + " 00:00:00' and fecha<='" + dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day + " 23:59:59') order by kardexid desc";
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(5), reader.GetString(6), reader.GetString(4), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(7));
                }
                reader.Close();
            }
        }
    }
}
