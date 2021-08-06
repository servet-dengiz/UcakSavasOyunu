﻿using Savas.Library.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas.Library.Concrete
{
    class Ucak:Cisim
    {
        private static readonly Random Random = new Random();
        public Ucak(Size hareketAlaniBoyutlari,double ucakHareketMesafe,int zorlukSeviyesi):base(hareketAlaniBoyutlari)
        {
            HareketMesafesi = (int)(Height * ucakHareketMesafe/zorlukSeviyesi);
            Left = Random.Next(hareketAlaniBoyutlari.Width - Width + 1);
        }

        public Mermi VurulduMu(List<Mermi> mermiler)
        {
            foreach (var mermi in mermiler)
            {
                var vurulduMu = mermi.Top < Bottom && mermi.Right > Left && mermi.Left < Right;
                if (vurulduMu) return mermi;
            }
            return null;
        }
    }
}
