using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savas.Desktop
{
    public partial class GirisForm : Form
    {
        public GirisForm()
        {
            InitializeComponent();
        }
        public static int seviye;
        

        private void btnZor_Click(object sender, EventArgs e)
        {
            seviye = 1;
            this.Hide();
            AnaForm a = new AnaForm();
            a.ShowDialog(); 
        }

        private void GirisForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnKolay_Click(object sender, EventArgs e)
        {
            seviye = 3;
            this.Hide();
            AnaForm a = new AnaForm();
            a.ShowDialog();
        }

        private void btnOrta_Click(object sender, EventArgs e)
        {
            seviye = 2;
            this.Hide();
            AnaForm a = new AnaForm();
            a.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
