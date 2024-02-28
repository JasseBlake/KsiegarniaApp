using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class KsiazkaIlosc:Ksiazka
    {
        public Ksiazka Ksiazka { get; set; }
        public int Ilosc { get; set; }

        public KsiazkaIlosc()
        {
            Ksiazka = null;
            Ilosc = 0;
        }

        public KsiazkaIlosc(Ksiazka ksiazka, int ilosc)
        {
            Ksiazka = ksiazka;
            Ilosc = ilosc;
        }

        public override string ToString()
        {
            return $"{Ksiazka.tytul} {Ilosc}";
        }
    }
}
