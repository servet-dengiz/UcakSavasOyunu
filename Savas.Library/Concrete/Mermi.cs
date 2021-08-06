using Savas.Library.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas.Library.Concrete
{
    internal class Mermi:Cisim
    {
        public Mermi(Size hareketAlaniBoyutlari, int namluOrtasıX):base(hareketAlaniBoyutlari)
        {

            BaslangicKonumunuAyarla(namluOrtasıX);
            HareketMesafesi = (int)(Height*1.5);
        }

        private void BaslangicKonumunuAyarla(int namluOrtasıX)
        {
            Bottom = HareketAlaniBoyutlari.Height;
            Center = namluOrtasıX;
        }
    }
}
