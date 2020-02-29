using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemaGestion
{
    public partial class Form1 : Form
    {
        string nombreUsuario;string tipoUsuario; string idUsuario;
       
        public Form1(string nombre,string tipo,string id)
        {
            InitializeComponent();
            nombreUsuario = nombre;
            tipoUsuario = tipo;
            idUsuario = id;
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            toolStripStatusLabel2.Text = nombre;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ribbonLabel1.Text = "Versión " + Application.ProductVersion.ToString();
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text =DateTime.Now.ToLongDateString()+" "+ DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Ventas))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Ventas(this,idUsuario);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Presupuesto))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Presupuesto(this, idUsuario);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton11_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ABMProductos))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ABMProductos(this);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Compras))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Compras(this,idUsuario);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton13_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ABMClientes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ABMClientes(this);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton8_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ABMProveedores))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ABMProveedores(this);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton12_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ABMCategorias))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ABMCategorias(this);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton23_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ABMUsuarios))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ABMUsuarios();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ABMTarjetas))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ABMTarjetas();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ABMCuotas))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ABMCuotas();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton24_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Configuracion))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Configuracion(0);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton25_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Configuracion))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Configuracion(1);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton26_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Configuracion))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Configuracion(2);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton27_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Configuracion))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Configuracion(2);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton7_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Kardex))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Kardex();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton17_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Vencimientos))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Vencimientos(this);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton9_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Informes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Informes(this,5);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton16_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Informes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Informes(this, 4);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton14_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Informes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Informes(this, 3);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton15_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Informes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Informes(this, 2);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton18_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Informes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Informes(this,0);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton19_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Informes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Informes(this, 1);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton20_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Informes))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new Informes(this, 2);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void ribbonButton29_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ListadoFacturas))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new ListadoFacturas(this);
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
