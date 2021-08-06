using Savas.Library.Enum;
using Savas.Library.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savas.Library.Concrete
{
    public class Oyun : IOyun
    {
        #region Alanlar

        private readonly Timer _gecenSureTimer = new Timer { Interval = 1000 };
        private readonly Timer _hareketTimer = new Timer { Interval = 100 };
        private readonly Timer _ucakOlusturmaTimer = new Timer { Interval = 2000 };
        private TimeSpan _gecenSure;
        private readonly Panel _ucaksavarPanel;
        private readonly Panel _savasAlaniPanel;
        private  Panel _bilgiPanel;
        private Ucaksavar _ucaksavar;
        private readonly List<Mermi> _mermiler = new List<Mermi>();
        private readonly List<Ucak> _ucaklar = new List<Ucak>();
         double ucakHareketMesafesi;
        int toplamUcakVurmaPuani =0;
        Label lbl = new Label();
        #endregion
        #region Olaylar

        public event EventHandler GecenSureDegisti;
        int zorlukSeviye;
        #endregion

        #region Ozellikler

        public bool DevamEdiyorMu { get; private set; }

        public TimeSpan GecenSure {

            get => _gecenSure;
            private set
            {
                _gecenSure = value;
                GecenSureDegisti?.Invoke(this, EventArgs.Empty);
            }
        }
        
        #endregion

        #region Metotlar

        public Oyun(Panel ucaksavarPanel,Panel savasAlaniPanel,double ucakHareketMesafe,Panel bilgiPanel,int zorlukSeviyesi)
        {
            _savasAlaniPanel = savasAlaniPanel;
            _ucaksavarPanel = ucaksavarPanel;
            _gecenSureTimer.Tick += GecenSureTimer_tick;
            _hareketTimer.Tick += HareketTimer_tick;
            _ucakOlusturmaTimer.Tick += UcakOlusturmaTimer_tick;
            ucakHareketMesafesi = ucakHareketMesafe;
            _bilgiPanel = bilgiPanel;
            zorlukSeviye = zorlukSeviyesi;
            bilgiLabel();
        }

        private void GecenSureTimer_tick(object sender, EventArgs e)
        {
            GecenSure += TimeSpan.FromSeconds(1);
        }

        private void UcakOlusturmaTimer_tick(object sender, EventArgs e)
        {
            UcakOlustur();
        }

        private void HareketTimer_tick(object sender, EventArgs e)
        {
            MermileriHareketEttir();
            UcaklariHareketEttir();
            VurulanUcaklariCikar();
        }

        private void VurulanUcaklariCikar()
        {
            for(var i = _ucaklar.Count - 1; i >= 0; i--)
            {
                var ucak = _ucaklar[i];

                var vuranMermi = ucak.VurulduMu(_mermiler);
                if (vuranMermi is null) continue;
                _ucaklar.Remove(ucak);
                _mermiler.Remove(vuranMermi);
                _savasAlaniPanel.Controls.Remove(ucak);
                _savasAlaniPanel.Controls.Remove(vuranMermi);
                toplamUcakVurmaPuani += 10;
                lbl.Text= $"Toplam:{toplamUcakVurmaPuani} ";

            }
        }

        private void geriDönüşButton()
        {
            Button btn = new Button();
            btn.Name = "geriDonusButton";
            btn.Text = "Oyun Bitti..\n Tekrar oynamak için b tuşuna basınız";
            btn.Width = 250;
            btn.Height = 90;
            btn.Font = new Font("Tahoma", 14, FontStyle.Bold);
            btn.Location =new Point((_savasAlaniPanel.Width/2)-75, (_savasAlaniPanel.Height/2)-45);
            btn.ForeColor = Color.White;
            btn.Click += Btn_Click;
            //this.Controls.Add(btn); //bu şekilde form'a ekleme yapılırsa tüm butonlar üst üste çıkacaktır
            _savasAlaniPanel.Controls.Add(btn);
        }

        private void bilgiLabel()
        {
            
            lbl.Text = $"Toplam:{toplamUcakVurmaPuani} ";
            lbl.Name = "lblToplam";
            lbl.Location = new Point(1200, 30);
            lbl.ForeColor = Color.White;
            lbl.Font= new Font("Tahoma", 24, FontStyle.Bold);
            lbl.Height = 60;
            lbl.Width = 300;
            _bilgiPanel.Controls.Add(lbl);


           
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void UcaklariHareketEttir()
        {
            foreach (var ucak in _ucaklar)
            {
                var carptiMi=ucak.HareketEttir(Yon.Asagi);
                if (!carptiMi) continue;
                Bitir();
                break;
            }
        }

        private void MermileriHareketEttir()
        {
            for (int i = _mermiler.Count-1; i >= 0; i--)
            {
                var mermi = _mermiler[i];
                var carptiMi = mermi.HareketEttir(Yon.Yukari);
                if (carptiMi)
                {
                    _mermiler.Remove(mermi);
                    _savasAlaniPanel.Controls.Remove(mermi);
                }
            }
        }

        public void Baslat()
        {
            if (DevamEdiyorMu) return;
            DevamEdiyorMu = true;
            ZamanlayicilariBaslat();
            UcaksavarOlustur();
            UcakOlustur();
            bilgiLabel();
            
        }

        private void UcakOlustur()
        {
            var ucak = new Ucak(_savasAlaniPanel.Size,.3,zorlukSeviye);
            _ucaklar.Add(ucak);
            _savasAlaniPanel.Controls.Add(ucak);
        }

        private void ZamanlayicilariBaslat()
        {
            _gecenSureTimer.Start();
            _hareketTimer.Start();
            _ucakOlusturmaTimer.Start();
        }
        private void ZamanlayicilariDurdur()
        {
            _gecenSureTimer.Stop();
            _hareketTimer.Stop();
            _ucakOlusturmaTimer.Stop();
        }
        private void UcaksavarOlustur()
        {
            /*  var ucaksavar = new PictureBox();
              ucaksavar.Image = Image.FromFile(@"Gorseller\Ucaksavar.png");*/
            _ucaksavar = new Ucaksavar(_ucaksavarPanel.Width,_ucaksavarPanel.Size) ;
            _ucaksavarPanel.Controls.Add(_ucaksavar);
        }

        private void Bitir()
        {
            if (!DevamEdiyorMu) return;
            DevamEdiyorMu = false;
            ZamanlayicilariDurdur();
            geriDönüşButton();
        }

        public void AtesEt()
        {
            if (!DevamEdiyorMu) return;
            var mermi = new Mermi(_savasAlaniPanel.Size,_ucaksavar.Center);
            _mermiler.Add(mermi);
            _savasAlaniPanel.Controls.Add(mermi);
        }

        public void UcaksavariHareketEttir(Yon yon)
        {
            if (!DevamEdiyorMu) return;
            _ucaksavar.HareketEttir(yon);
        }
        #endregion


    }
}
