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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            label3.Text = "Versión " + Application.ProductVersion.ToString();
            
        }

        private void chekUpdates()
        {
            Login log = new sistemaGestion.Login();
            this.Hide();
            timer1.Enabled = false;
            log.Show();
        }

        private void Splash_Activated(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chekUpdates();
        }
    }
}
