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
    public partial class Compras : Form
    {
        Form1 padre;string idUsuario;
        Conexion dbCon = Conexion.Instance();
        public Compras(Form1 p,string usuario)
        {
            InitializeComponent();
            padre = p;
            idUsuario = usuario;
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection col = new
         AutoCompleteStringCollection();
            if (dbCon.IsConnect())
            {
                string query = "select provrazonsocial from proveedores";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    col.Add(reader.GetString(0));
                }
                reader.Close();
            }
            comboBox2.SelectedIndex = 0;
            textBox5.AutoCompleteCustomSource = col;
            AutoCompleteStringCollection col2 = new
         AutoCompleteStringCollection();
            if (dbCon.IsConnect())
            {
                string query = "select prodescripcion from productos";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    col2.Add(reader.GetString(0));
                }
                reader.Close();
            }
            textBox1.AutoCompleteCustomSource = col2;
            

            nuevaFila();
        }

        private void nuevaFila()
        {


            dataGridView1.Rows.Add("", "", "", "", "", "");
            acomodarControles(dataGridView1.Rows.Count - 1);

        }

        private void acomodarControles(int fila)
        {
            try
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[fila].Cells[0];

                Rectangle posicion = dataGridView1.GetCellDisplayRectangle(0, fila, false);
                int x = posicion.Location.X + (posicion.Size.Height / 2) + 237;
                int y = posicion.Location.Y + (posicion.Size.Height / 2) - 10;
                textBox1.Location = new Point(x, y);
                textBox1.Size = posicion.Size;

                posicion = dataGridView1.GetCellDisplayRectangle(2, fila, false);
                x = posicion.Location.X + (posicion.Size.Height / 2) + 237;
                y = posicion.Location.Y + (posicion.Size.Height / 2) - 10;
                textBox2.Location = new Point(x, y);
                textBox2.Size = posicion.Size;

                posicion = dataGridView1.GetCellDisplayRectangle(3, fila, false);
                x = posicion.Location.X + (posicion.Size.Height / 2) + 237;
                y = posicion.Location.Y + (posicion.Size.Height / 2) - 10;
                textBox3.Location = new Point(x, y);
                textBox3.Size = posicion.Size;

                textBox1.Text = dataGridView1.Rows[fila].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[fila].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[fila].Cells[3].Value.ToString();

                textBox1.Focus();
                textBox1.SelectAll();
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            acomodarControles(e.RowIndex);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int numero; int fila = dataGridView1.CurrentRow.Index; float preciou = 0;
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float numero;
                textBox2.Text = textBox2.Text.Replace('.', ',');
                if (!float.TryParse(textBox2.Text, out numero)) { textBox2.Text = "1"; }
                if (textBox2.Text == "" || float.Parse(textBox2.Text) < 0) { textBox2.Text = "1"; }
                int cantvendida = 0; int paquetes = 0; int unidadessueltas = 0;

                double preciounitario = 0;
                cantvendida = int.Parse(textBox2.Text);
                preciounitario = float.Parse(textBox3.Text);

                try
                {
                            double preciofinal = (cantvendida * preciounitario);
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = textBox2.Text;
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciofinal.ToString("0.##"); ;
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value = (preciofinal / double.Parse(textBox2.Text.Replace('.', ','))).ToString();
                            textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
                     
                    calcularTotal();
                    textBox3.Focus();
                }
                catch
                {
                    textBox2.Focus();
                }
            }
        }

        public double RoundUp(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }

        private void calcularTotal()
        {
            if (textBox7.Text.Trim() == "") { textBox7.Text = "0"; }
            if (textBox10.Text.Trim() == "") { textBox10.Text = "0"; }



            float total = 0; float iva105 = 0; float iva21= 0;
           

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() != "")
                {
                    try
                    {
                        total += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        float piva = (float.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString()) / 100) + 1;
                        if (dataGridView1.Rows[i].Cells[8].Value.ToString() == "21")
                        {
                            iva21 += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())-(float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / piva);
                        }
                        else
                        {
                            iva105 += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())-(float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / piva);
                        }
                    }
                    catch { }
                }
            }
            //subtotal
            textBox8.Text = (total-(iva105+iva21)).ToString();
            textBox7.Text = iva105.ToString();
            textBox10.Text = iva21.ToString();
            label5.Text = total.ToString();

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float numero;
                textBox3.Text = textBox3.Text.Replace('.', ',');
                if (textBox1.Text.Trim() == "") { textBox1.Focus(); return; }
                if (!float.TryParse(textBox3.Text, out numero)) { textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(); }
                if (textBox3.Text == "" || float.Parse(textBox3.Text) < 0) { textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(); }

                try
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value = textBox3.Text;
                    float preciou = 0;
                    preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                    calcularTotal();
                    //si estoy en la ultima fila, tengo que agregar una mas. sino, tengo que ir a la ultima
                    if (dataGridView1.CurrentRow.Index == (dataGridView1.Rows.Count - 1))
                    {
                        nuevaFila();
                    }
                    else
                    {
                        acomodarControles(dataGridView1.Rows.Count - 1);
                    }
                }
                catch
                {
                    textBox3.Focus();
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //buscar cliente por nombre
                if (dbCon.IsConnect())
                {
                    string query = "select * from proveedores where provrazonsocial='" + textBox5.Text + "'";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    textBox4.Text = "";

                    while (reader.Read())
                    {
                        textBox4.Text = reader.GetString(0);

                        textBox1.Focus();
                    }
                    reader.Close();
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //buscar cliente por codigo
                if (dbCon.IsConnect())
                {
                    string query = "select * from proveedores where provcodigo=" + textBox4.Text;
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        textBox5.Text = "";
                        textBox5.Text = reader.GetString(1);

                        textBox1.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Proveedor no encontrado", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox4.Text = "1";
                        textBox5.Text = "CONSUMIDOR FINAL";
                        textBox4.SelectAll();
                    }
                    reader.Close();
                }
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.SelectAll();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.SelectAll();
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        private void borrarTodo()
        {
            textBox5.Text = "CONSUMIDOR FINAL";
            textBox4.Text = "1";
            dataGridView1.Rows.Clear();
            label5.Text = "0,00";
            comboBox2.SelectedIndex = 0;
            textBox10.Text = "0,00";
            textBox7.Text = "0,00";
            textBox8.Text = "0,00";
            nuevaFila();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            borrarTodo();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
            calcularTotal();
            acomodarControles(dataGridView1.Rows.Count - 1);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            long numero; int fila = dataGridView1.CurrentRow.Index; float preciou = 0;
            if (textBox1.Text.Trim() == "") { return; }
            textBox1.Text = textBox1.Text.Replace("*", string.Empty);
            if (!(long.TryParse(textBox1.Text, out numero)))
            {
                string codigo = "";
                string query = "select * from productos where  prodescripcion='" + textBox1.Text + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();
                    codigo = reader.GetString(0);
                    dataGridView1.Rows[fila].Cells[0].Value = reader.GetString(1);
                    dataGridView1.Rows[fila].Cells[1].Value = reader.GetString(2);
                    dataGridView1.Rows[fila].Cells[2].Value = "1";
                    dataGridView1.Rows[fila].Cells[3].Value = (double.Parse(reader.GetString(3))).ToString("0.##");
                    dataGridView1.Rows[fila].Cells[4].Value = reader.GetString(6);
                    dataGridView1.Rows[fila].Cells[5].Value = reader.GetString(0);
                    dataGridView1.Rows[fila].Cells[6].Value = reader.GetString(7);
                    dataGridView1.Rows[fila].Cells[7].Value = "";
                    dataGridView1.Rows[fila].Cells[8].Value = reader.GetString(4);
                    if (reader.GetString(10) != "1")
                    {
                        dataGridView1.Rows[fila].Cells[7].Value = "Paq: " + reader.GetString(10) + "u";
                    }

                    textBox1.Text = reader.GetString(1);
                    textBox2.Text = "1";
                    textBox3.Text = (double.Parse(reader.GetString(3))).ToString("0.##");
                    reader.Close();
                    preciou = 0;
                    preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                    //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    calcularTotal();
                    //textBox2.Focus();
                }
                else
                {
                    reader.Close();
                    textBox1.Focus();
                    textBox1.SelectAll();
                }
            }
            else
            {
                //buscar por codigo interno o codigo de barras
                if (dbCon.IsConnect())
                {
                    string codigo = "";
                    string query = "select * from productos where procodigo=" + textBox1.Text;
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        codigo = reader.GetString(0);
                        dataGridView1.Rows[fila].Cells[0].Value = reader.GetString(1);
                        dataGridView1.Rows[fila].Cells[1].Value = reader.GetString(2);
                        dataGridView1.Rows[fila].Cells[2].Value = "1";
                        dataGridView1.Rows[fila].Cells[3].Value = (double.Parse(reader.GetString(3))).ToString("0.##");
                        dataGridView1.Rows[fila].Cells[4].Value = reader.GetString(6);
                        dataGridView1.Rows[fila].Cells[5].Value = reader.GetString(0);
                        dataGridView1.Rows[fila].Cells[6].Value = reader.GetString(7);
                        dataGridView1.Rows[fila].Cells[7].Value = "";
                        dataGridView1.Rows[fila].Cells[8].Value = reader.GetString(4);
                        if (reader.GetString(10) != "1")
                        {
                            dataGridView1.Rows[fila].Cells[7].Value = "Paq: " + reader.GetString(10) + "u";
                        }
                        textBox1.Text = reader.GetString(1);
                        textBox2.Text = "1";
                        textBox3.Text = (double.Parse(reader.GetString(3))).ToString("0.##");
                        reader.Close();
                        preciou = 0;
                        preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                        //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        calcularTotal();
                        //textBox2.Focus();
                    }
                    else
                    {
                        reader.Close();
                        query = "select * from productos where  procodbar='" + textBox1.Text + "'";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            codigo = reader.GetString(0);
                            dataGridView1.Rows[fila].Cells[0].Value = reader.GetString(1);
                            dataGridView1.Rows[fila].Cells[1].Value = reader.GetString(2);
                            dataGridView1.Rows[fila].Cells[2].Value = "1";
                            dataGridView1.Rows[fila].Cells[3].Value = (double.Parse(reader.GetString(3))).ToString("0.##");
                            dataGridView1.Rows[fila].Cells[4].Value = reader.GetString(6);
                            dataGridView1.Rows[fila].Cells[5].Value = reader.GetString(0);
                            dataGridView1.Rows[fila].Cells[6].Value = reader.GetString(7);
                            dataGridView1.Rows[fila].Cells[7].Value = "";
                            dataGridView1.Rows[fila].Cells[8].Value = reader.GetString(4);
                            if (!reader.IsDBNull(10))
                            {
                                if (reader.GetString(10) != "1")
                                {
                                    dataGridView1.Rows[fila].Cells[7].Value = "Paq: " + reader.GetString(10) + "u";
                                }
                            }
                            textBox1.Text = reader.GetString(1);
                            textBox2.Text = "1";
                            textBox3.Text = (double.Parse(reader.GetString(3))).ToString("0.##");
                            reader.Close();
                            preciou = 0;
                            preciou = float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString());
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString().Replace('.', ',');
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = preciou * float.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                            calcularTotal();
                            //textBox2.Focus();
                        }
                        else
                        {

                            reader.Close();
                            textBox1.Focus();
                            textBox1.SelectAll();

                        }
                    }
                    reader.Close();
                    //textBox1.Focus();
                    //textBox1.SelectAll();
                }

            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string fecha = dateTimePicker1.Value.Year.ToString() +"-"+ dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString()+" 00:00:00";
            string vencimiento = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString() + " 00:00:00";
            bool cambiarPrecios = false;
            string fechahoy = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            DialogResult resultado;
            if (float.Parse(label5.Text) != 0)
            {
                resultado = MessageBox.Show("Esta acción modifica el precio de costo de sus productos, lo que impacta en sus porcentajes de ganancia o precios finales.\nSi desea mantener el margen de ganancia modificando el precio, presione SI.\nSi desea mantener el precio modificando su margen de ganancia, presione NO\nPara cancelar la carga de datos, presione CANCELAR.\n\nLos precios/porcentajes por paquete no serán modificados.", "Compras", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (resultado == DialogResult.Cancel) { return; }
                if (resultado == DialogResult.Yes) { cambiarPrecios = true; }

                var cmd = new MySqlCommand("INSERT INTO `compras`(`comcodigo`, `comfecha`, `comtipo`, `comnumfactura`, `provcodigo`, `iva105`, `iva21`, `subtotal`, `total`, `fechavenc`, `estado`) VALUES (NULL,'"+fecha+"','"+comboBox2.Text+"','"+textBox6.Text+"',"+textBox4.Text+",'"+textBox7.Text.Replace(',','.')+ "','" + textBox10.Text.Replace(',', '.') + "','" + textBox8.Text.Replace(',', '.') + "','" + label5.Text.Replace(',', '.') + "','"+vencimiento+"',0)", dbCon.Connection);
                cmd.ExecuteNonQuery();
                long codigo = cmd.LastInsertedId;
                float proiva = 0;
                float procosto = 0;
                float procostoneto = 0;
                float procostonetoOrig = 0;
                float proventa = 0;
                float proganancia = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() != "")
                    {
                        procosto = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        cmd = new MySqlCommand("select proiva,proprecioventa,proprecioneto from productos where procodigo=" + dataGridView1.Rows[i].Cells[5].Value.ToString(), dbCon.Connection);
                        var reader = cmd.ExecuteReader();
                        reader.Read();
                        proiva = reader.GetFloat(0);
                        proventa = reader.GetFloat(1);
                        procostonetoOrig = reader.GetFloat(2);
                        reader.Close();
                        
                        //calcular el nuevo precio de costo neto y guardar junto con el costo nuevo
                        float diferencia = ((proiva * procosto) / 100);
                        procostoneto = (procosto + diferencia);
                       
                        cmd = new MySqlCommand("update productos set propreciocosto='"+procosto.ToString().Replace(',','.')+ "',proprecioneto='" + procostoneto.ToString().Replace(',', '.') + "' where procodigo=" + dataGridView1.Rows[i].Cells[5].Value.ToString(), dbCon.Connection);
                        cmd.ExecuteNonQuery();
                        //guardar stock y generar kardex
                        //kardex
                        string query = "insert into kardex (kardexid,comprobante,numerocomprobante,operacion,fecha,procodigo,prodescripcion,cantidad) values (NULL,'" + comboBox2.Text + " Compra'," + codigo.ToString() + ",'INGRESO','" + fechahoy + "'," + dataGridView1.Rows[i].Cells[5].Value.ToString() + ",'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(',', '.') + "')";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.ExecuteNonQuery();
                        //incremento stock
                        query = "update productos set prostock=prostock+" + dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(',', '.') + " where procodigo=" + dataGridView1.Rows[i].Cells[5].Value.ToString();
                        
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand("INSERT INTO `detallecompras`(`comcodigo`, `procodigo`, `prodescripcion`, `cantidad`, `preciounitario`, `subtotal`) VALUES ("+codigo.ToString()+","+ dataGridView1.Rows[i].Cells[5].Value.ToString() + ",'"+ dataGridView1.Rows[i].Cells[1].Value.ToString() + "','"+ dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(',','.') + "','"+ dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(',','.') + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString().Replace(',', '.') + "')", dbCon.Connection);
                        cmd.ExecuteNonQuery();
                        //calcular precio o porcentaje, segun corresponda
                        float nuevadiferencia = 0;
                        if (cambiarPrecios==true)
                        {
                            //calculo el porcentaje para mantener
                            nuevadiferencia = proventa - procostonetoOrig;
                            proganancia = ((nuevadiferencia * 100) / procostonetoOrig);
                            nuevadiferencia = ((proganancia * procostoneto) / 100);
                            proventa = (procostoneto + nuevadiferencia);
                            cmd = new MySqlCommand("update productos set proprecioventa='"+proventa.ToString().Replace(',','.')+"' where procodigo=" + dataGridView1.Rows[i].Cells[5].Value.ToString(), dbCon.Connection);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                             nuevadiferencia = proventa - procostonetoOrig;
                            proganancia = ((nuevadiferencia * 100) / procostonetoOrig);
                            
                        }

                    }
                }
                borrarTodo();
          }
            }
    }
}
