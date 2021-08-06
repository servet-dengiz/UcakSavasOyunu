using Savas.Library.Concrete;
using Savas.Library.Enum;
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
    public partial class AnaForm : Form
    {
       private readonly Oyun _oyun;
        
        public bool bitti;
        public AnaForm()
        {
            InitializeComponent();
            _oyun = new Oyun(UcakSavarPanel,savasAlaniPanel,1,bilgiPanel,GirisForm.seviye);
            _oyun.GecenSureDegisti += Oyun_GecenSureDegisti;
            
            
        }

        private void AnaForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                   
                      _oyun.Baslat();
                    break;

                case Keys.Right:
                    _oyun.UcaksavariHareketEttir(Yon.Saga);
                break;

                case Keys.Left:
                    _oyun.UcaksavariHareketEttir(Yon.Sola);
                    break;
                case Keys.Space:
                    _oyun.AtesEt();
                    break;
                case Keys.B:

                    this.Hide();
                    GirisForm g = new GirisForm();
                    g.ShowDialog();

                    break;
            }
        }
        private void Oyun_GecenSureDegisti(object sender,EventArgs e)
        {
            sureLabel.Text = _oyun.GecenSure.ToString(@"m\:ss");
            //sureLabel.Text = $"{ _oyun.GecenSure.Minutes}:{ _oyun.GecenSure.Seconds.ToString("D2")} ";
        }
    }
}
