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
    public partial class ABMProductos : Form
    {
        Conexion dbCon = Conexion.Instance();
         Compras padre;
        Form1 principal;
        bool esnuevo = false;

        public ABMProductos(Form1 p)
        {
            InitializeComponent();
            principal = p;
        }

         public ABMProductos(Compras ff)
         {
             InitializeComponent();
             padre = ff;
             linkLabel1_LinkClicked(this, new LinkLabelLinkClickedEventArgs(new LinkLabel.Link()));
             bunifuiOSSwitch1.Value = true;
             bunifuiOSSwitch1.Enabled = false;
             //button3.Enabled = false;
             this.ControlBox = false;
         }

        private static DialogResult ShowInputDialog(ref string input, string title)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            textBox.CharacterCasing = CharacterCasing.Upper;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&Aceptar";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancelar";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void ABMProductos_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;

            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns.Add("barras", "Codigo Barras");
            dataGridView1.Columns.Add("desc", "Descripcion");
            dataGridView1.Columns.Add("stock", "Stock");
            if (dbCon.IsConnect())
            {

                var cmd = new MySqlCommand("select * from categorias order by idcategoria asc", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0) + "-" + reader.GetString(2));
                }
                reader.Close();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscarprod();
            }
        }

        private void buscarprod()
        {
            if (textBox3.Text == "") { return; }
            dataGridView1.Rows.Clear();
            string condicion = "probaja=0 and ";
            if (bunifuiOSSwitch2.Value == true) { condicion = ""; }
            try
            {
                string query = "";
                switch (comboBox3.SelectedIndex)
                {
                    //por nombre que empiece
                    case 0:
                        query = "select * from productos where " + condicion + " prodescripcion like '" + textBox3.Text + "%'";
                        break;
                    //por nombre que incluya
                    case 1:
                        query = "select * from productos where " + condicion + " prodescripcion like '%" + textBox3.Text.Replace(' ', '%') + "%'";
                        break;
                    //por codigo interno
                    case 2:
                        query = "select * from productos where " + condicion + " procodigo=" + textBox3.Text;
                        break;
                    //por barras
                    case 3:
                        query = "select * from productos where " + condicion + " procodbar = '" + textBox3.Text + "'";
                        break;
                }
                if (dbCon.IsConnect())
                {

                    var cmd = new MySqlCommand(query.Replace("*","COUNT(*)"), dbCon.Connection);
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
                       fila = dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                        if (reader.GetString(7) == "1")
                        {
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                dataGridView1.Rows[fila].Cells[i].Style.ForeColor = Color.Red;
                            }
                        }
                    }
                    reader.Close();
                    principal.toolStripProgressBar1.Visible = false;
                }

            }
            catch { principal.toolStripProgressBar1.Visible = false; }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuCards1.Enabled = false;
            bunifuCards3.Enabled = false;
            bunifuCards2.Enabled = false;
            bunifuCards5.Enabled = true;

            dataGridView1.Enabled = true;

            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            bunifuFlatButton3.Enabled = false;
            textBox3.Focus();
            clearError();

            //if (padre != null) { this.Close(); }
        }

        private void clearError()
        {
            foreach (TextBox tb in bunifuCards1.Controls.OfType<TextBox>())
            {
                tb.BackColor = Color.White;
            }
            foreach (TextBox tb in bunifuCards2.Controls.OfType<TextBox>())
            {
                tb.BackColor = Color.White;
            }
            foreach (TextBox tb in bunifuCards3.Controls.OfType<TextBox>())
            {
                tb.BackColor = Color.White;
            }
        }

        private void linkLabel2_Click(object sender, EventArgs e)
        {
            string input = "CATEGORIA";
            if (ShowInputDialog(ref input, "Nueva Categoria") == DialogResult.OK)
            {
                if (dbCon.IsConnect())
                {
                    var cmd = new MySqlCommand("insert into categorias values(NULL,1,'" + input + "',0)", dbCon.Connection);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand("select * from categorias order by idcategoria desc limit 1", dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0) + "-" + reader.GetString(2));
                        comboBox2.Text = reader.GetString(0) + "-" + reader.GetString(2);
                    }
                    reader.Close();


                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (textBox14.Text == "") { return; }
            esnuevo = false;
            bunifuCards1.Enabled = true;
            bunifuCards3.Enabled = true;
            bunifuCards2.Enabled = true;
            bunifuCards5.Enabled = false;

            dataGridView1.Enabled = false;
            textBox14.Enabled = false;
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            esnuevo = true;
            bunifuCards1.Enabled = true;
            bunifuCards3.Enabled = true;
            bunifuCards2.Enabled = true;
            bunifuCards5.Enabled = false;
            textBox14.Enabled = true;
            dataGridView1.Enabled = false;

            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;

            textBox2.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "21";
            label12.Text = "Ultima Actualización: ";
            bunifuiOSSwitch1.Value = true;
            textBox2.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tratar de calcular NETO
                if (textBox1.Text.Trim() != "")
                {
                    try
                    {

                        float costo = float.Parse(textBox1.Text.Replace('.', ','));
                        float iva = float.Parse(comboBox1.Text.Replace('.', ','));
                        float diferencia = ((iva * costo) / 100);
                        textBox4.Text = (costo + diferencia).ToString();
                    }
                    catch
                    {
                        textBox4.Text = "0";
                    }
                }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tratar de calcular COSTO
                if (textBox4.Text.Trim() != "")
                {
                    try
                    {

                        float costo = float.Parse(textBox4.Text.Replace('.', ','));
                        float iva = (float.Parse(comboBox1.Text.Replace('.', ','))/100)+1;
                        
                        textBox1.Text = (costo / iva).ToString();
                    }
                    catch
                    {
                        textBox1.Text = "0";
                    }
                }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.Text.Trim() == "") { comboBox1.Text = "21"; }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tratar de calcular VENTA
                if (textBox4.Text.Trim() != "")
                {
                    try
                    {

                        float costo = float.Parse(textBox4.Text.Replace('.', ','));
                        float ganancia = float.Parse(textBox9.Text.Replace('.', ','));
                        float diferencia = ((ganancia * costo) / 100);
                        textBox8.Text = (costo + diferencia).ToString();
                    }
                    catch
                    {
                        textBox8.Text = "0";
                    }
                }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //tratar de calcular GANANCIA
                if (textBox4.Text.Trim() != "")
                {
                    try
                    {
                        float venta = float.Parse(textBox8.Text.Replace('.', ','));
                        float costo = float.Parse(textBox4.Text.Replace('.', ','));
                        float diferencia = venta - costo;
                        float ganancia = ((diferencia * 100) / costo);
                        textBox9.Text = ganancia.ToString();
                    }
                    catch
                    {
                        textBox9.Text = "0";
                    }
                }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        /*
        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tratar de calcular VENTA
                if (textBox11.Text.Trim() != "")
                {
                    try
                    {

                        float costo = float.Parse(textBox11.Text.Replace('.', ','));
                        float ganancia = float.Parse(textBox12.Text.Replace('.', ','));
                        float diferencia = ((ganancia * costo) / 100);
                        textBox8.Text = (costo + diferencia).ToString();
                    }
                    catch
                    {
                        textBox8.Text = "0";
                    }
                }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tratar de calcular GANANCIA
                if (textBox8.Text.Trim() != "")
                {
                    try
                    {
                        float venta = float.Parse(textBox8.Text.Replace('.', ','));
                        float costo = float.Parse(textBox11.Text.Replace('.', ','));
                        float diferencia = venta - costo;
                        float ganancia = ((diferencia * 100) / costo);
                        textBox12.Text = ganancia.ToString();
                    }
                    catch
                    {
                        textBox12.Text = "0";
                    }
                }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    label15.Text = (float.Parse(textBox10.Text.Replace('.', ',')) * float.Parse(textBox6.Text.Replace('.', ','))).ToString() + "u";
                }
                catch { }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    label15.Text = (float.Parse(textBox10.Text.Replace('.', ',')) * float.Parse(textBox6.Text.Replace('.', ','))).ToString() + "u";
                }
                catch { }
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tratar de calcular GANANCIA
                if (textBox8.Text.Trim() != "")
                {
                    try
                    {
                        float venta = float.Parse(textBox8.Text.Replace('.', ','));
                        float costo = float.Parse(textBox11.Text.Replace('.', ','));
                        float diferencia = venta - costo;
                        float ganancia = ((diferencia * 100) / costo);
                        textBox12.Text = ganancia.ToString();
                    }
                    catch
                    {
                        textBox12.Text = "0";
                    }
                }
                
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }*/

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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
                e.Handled = true;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
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
                e.Handled = true;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
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
                e.Handled = true;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void SetError(Control control,string mensaje)
        {
            control.BackColor = Color.FromArgb(217, 83, 79);
            control.Focus();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            clearError();
            float numero;
            if (textBox2.Text == "") { SetError(textBox2, "Este campo no puede estar vacio"); return; }
            if (comboBox2.Text == "") { MessageBox.Show("Debe indicar una categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (textBox4.Text == "" || !(float.TryParse(textBox4.Text, out numero)))
            {
                SetError(textBox4, "Este campo no puede estar vacio. Debe ser un número");
                return;
            }

            

            if (textBox6.Text == "" || !(float.TryParse(textBox6.Text, out numero)))
            {
                SetError(textBox6, "Este campo no puede estar vacio. Debe ser un número");
                return;
            }
            if (textBox7.Text == "" || !(float.TryParse(textBox7.Text, out numero)))
            {
                SetError(textBox7, "Este campo no puede estar vacio. Debe ser un número");
                return;
            }


            string fechahoy = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString()+" "+DateTime.Now.Hour.ToString()+":"+DateTime.Now.Minute.ToString()+":"+DateTime.Now.Second.ToString();
            

            if(textBox14.Text.Trim()=="")
            {
                textBox14.Text = dbCon.nuevoid("procodigo", "productos").ToString();
            }

            string query = "select * from productos where procodbar=" + textBox13.Text;
            var cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            string baja = "1";
            if (bunifuiOSSwitch1.Value == true) { baja = "0"; }
            long codigo = 0;

            if (esnuevo) //es un prd nuevo
            {
                //verificar que el codigo no se repita


                cmd = new MySqlCommand(query, dbCon.Connection);
                reader = cmd.ExecuteReader();
                if (reader.HasRows) { SetError(textBox13, "Ya existe este codigo."); reader.Close(); return; }
                reader.Close();
                cmd = new MySqlCommand("select * from productos where procodigo=" + textBox14.Text, dbCon.Connection);
                reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    reader.Close();
                    if(MessageBox.Show("Este código interno ya existe. Desea que el sistema asigne un nuevo código por usted?","Codigo Interno",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        textBox14.Text = dbCon.nuevoid("procodigo", "productos").ToString();
                    }
                    else { SetError(textBox14, "Ya existe este codigo."); return; }
                }
                reader.Close();
                query = @"INSERT INTO `productos`(`ProCodigo`, `ProCodBar`, `ProDescripcion`, `ProStockActual`, `ProPrecioCosto`, `ProPrecioNeto`, `ProIVA`, `ProCantMinima`, `ProFecha`, `ProBaja`, `Fabricante`, `ProFechaUActualizacion`, `Categoria`, `Descripcion`, `EsBundle`) VALUES (" + textBox14.Text+ ",'" + textBox13.Text + "','" + textBox2.Text + "','" + textBox6.Text.Replace(',','.') + "','"+ textBox1.Text.Replace(',', '.')+"','" + textBox4.Text.Replace(',','.') + "','" + comboBox1.Text.Replace(',','.') + "','" + textBox7.Text.Replace(',','.') +"','"+ fechahoy+ "','" + baja + "','','" + fechahoy + "','" + dbCon.extraerCodigo(comboBox2.Text) + "','" + textBox5.Text + "','0')";
            }
            else // es una modificacion
            {
                query = "update `productos` SET procodbar='" + textBox13.Text + "',prodescripcion='" + textBox2.Text + "',propreciocosto='" + textBox1.Text.Replace(',', '.') + "',proiva='" + comboBox1.Text.Replace(',', '.') + "',proprecioneto='" + textBox4.Text.Replace(',', '.') + "',prostockactual='" + textBox6.Text.Replace(',', '.') + "',ProCantMinima='" + textBox7.Text.Replace(',', '.') + "',probaja='" + baja + "', profechauactualizacion='" + fechahoy + "',categoria='" + dbCon.extraerCodigo(comboBox2.Text) + "',descripcion='" + textBox5.Text + "' where procodigo=" + textBox14.Text;

            }
            

            if (dbCon.IsConnect())
            {
                cmd = new MySqlCommand(query, dbCon.Connection);
                reader = cmd.ExecuteReader();
                if (cmd.LastInsertedId == 0)
                {
                    codigo = long.Parse(textBox14.Text);
                }
                else
                {
                    codigo = cmd.LastInsertedId;
                }
                reader.Close();

                cmd= new MySqlCommand("delete from productolista where idproducto="+codigo, dbCon.Connection);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into productolista values (1,"+codigo+",'" + textBox8.Text.Replace(',', '.')+ "','" + textBox9.Text.Replace(',', '.') + "',0)", dbCon.Connection);
                reader = cmd.ExecuteReader();
                reader.Close();
            }

            


            if (padre != null)
            {
                /*padre.prod.Add(textBox2.Text);
                padre.textBox2.AutoCompleteCustomSource = padre.prod;
                padre.cargarProducto(codigo.ToString());*/
                this.Close();
            }
            bunifuFlatButton2_Click(this, e);
            buscarprod();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dbCon.IsConnect())
            {
                string query = "select productos.*,categorias.nombre as categoria from productos inner join categorias on categorias.IDCategoria=productos.categoria where productos.procodigo=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox14.Text = reader.GetString(0); //procodigo
                    textBox13.Text = reader.GetString(1); //procodbarrra
                    textBox2.Text = reader.GetString(2); ; //prodescripcion
                    textBox6.Text = reader.GetString(3); //stock actual

                    textBox1.Text = reader.GetString(4); //propreciocosto (costo unidad)
                    textBox4.Text = reader.GetString(5); //proprecioneto (costo unidad)
                    comboBox1.Text = reader.GetString(6); //proiva

                    textBox7.Text = reader.GetString(7); //stockmin
                    label12.Text = "Ultima Actualización: "+reader.GetString(11); //fecha
                    comboBox2.Text = reader.GetString(12) + "-" + reader.GetString(15); //categoria

                    textBox5.Text = reader.GetString(13); //descripcion

                    // groupBox1.Text = reader.GetString(0); //codigo
                    if (reader.GetString(9) == "1") { bunifuiOSSwitch1.Value= false; } else { bunifuiOSSwitch1.Value = true; } //probaja
                }
                reader.Close();
                cmd = new MySqlCommand("select * from productolista where idlista=1 and idproducto=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), dbCon.Connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox8.Text = reader.GetString(2);
                    textBox9.Text = reader.GetString(3);
                }
                reader.Close();

            }
        }
    }
}
