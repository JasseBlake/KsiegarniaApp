using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class Ksiegarnia
    {
        public List<KsiazkaIlosc> inwentarz { get; set; }
        public List<Klient> klienci { get; set; }
        public bool czyOtwarta { get; set; }

        public Ksiegarnia()
        {
            inwentarz = new List<KsiazkaIlosc>();
            klienci = new List<Klient>();
            czyOtwarta = true;
        }

        public Ksiegarnia(List<KsiazkaIlosc> inwentarz)
        {
            this.inwentarz = inwentarz;
            klienci = new List<Klient>();
            czyOtwarta = true;
        }

        public Ksiegarnia(List<Klient> klienci)
        {
            inwentarz = new List<KsiazkaIlosc>();
            this.klienci = klienci;
            czyOtwarta = true;
        }

        public Ksiegarnia(List<KsiazkaIlosc> inwentarz, List<Klient> klienci)
        {
            this.inwentarz = inwentarz;
            this.klienci = klienci;
            czyOtwarta = true;
        }

        public List<string> InwentarzToListString()
        {
            return inwentarz.Select(ksiazkaIlosc => ksiazkaIlosc.ToString()).ToList();
        }
    }
}
