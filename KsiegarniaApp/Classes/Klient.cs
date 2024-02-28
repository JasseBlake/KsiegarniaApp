using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class Klient : Uzytkownik
    {
        public Skarbonka skarbonka { get; set; }
        public string adresDostawy { get; set; }
        public List<Ksiazka> posiadaneKsiazki { get; set; }

        public Klient() : base("defaultUsername", "defaultPassword")
        {
            skarbonka = new Skarbonka();
            adresDostawy = "";
            posiadaneKsiazki = new List<Ksiazka>();
        }

        public Klient(string nazwaUzytkownika, string haslo) : base(nazwaUzytkownika, haslo)
        {
            skarbonka = new Skarbonka();
            adresDostawy = "";
            posiadaneKsiazki = new List<Ksiazka>();
        }

        public Klient(string nazwaUzytkownika, string haslo, decimal saldo) : base(nazwaUzytkownika, haslo)
        {
            skarbonka = new Skarbonka(saldo);
            adresDostawy = "";
            posiadaneKsiazki = new List<Ksiazka>();
        }

        public Klient(string nazwaUzytkownika, string haslo, decimal saldo, string adresDostawy) : base(nazwaUzytkownika, haslo)
        {
            skarbonka = new Skarbonka(saldo);
            this.adresDostawy = adresDostawy;
            posiadaneKsiazki = new List<Ksiazka>();
        }

        public Klient(string nazwaUzytkownika, string haslo, decimal saldo, string adresDostawy, List<Ksiazka> posiadaneKsiazki) : base(nazwaUzytkownika, haslo)
        {
            skarbonka = new Skarbonka(saldo);
            this.adresDostawy = adresDostawy;
            this.posiadaneKsiazki = posiadaneKsiazki;
        }

        public bool WystawOcene(Ksiazka ksiazka, decimal ocena)
        {
            if(PosiadaKsiazke(ksiazka))
            {
                var recenzja = ksiazka.recenzje.Find(recenzja => recenzja.autor == this);
                if (recenzja != null && recenzja.ocena == -1m)
                {
                    recenzja.ocena = ocena;
                    return true;
                }
                else if (recenzja == null)
                {
                    ksiazka.DodajRecenzje(new Recenzja(this, ocena));
                    return true;
                }
            }
            return false;
        }

        public bool WystawRecenzje(Ksiazka ksiazka, string opis)
        {
            if (PosiadaKsiazke(ksiazka))
            {
                var recenzja = ksiazka.recenzje.Find(recenzja => recenzja.autor == this);
                if (recenzja != null && recenzja.opis == "")
                {
                    recenzja.opis = opis;
                    return true;
                }
                else if(recenzja == null)
                {
                    ksiazka.DodajRecenzje(new Recenzja(this, opis));
                    return true;
                }
            }
            return false;
        }

        public bool KupKsiazke(Ksiazka ksiazka)
        {
            if(ksiazka == null)
            {
                return false;
            }
            if(!PosiadaKsiazke(ksiazka) && skarbonka.OdejmijSrodki(ksiazka.cena))
            {
                posiadaneKsiazki.Add(ksiazka);
                return true;
            }
            return false;
        }

        private bool PosiadaKsiazke(Ksiazka ksiazka)
        {
            return posiadaneKsiazki.Contains(ksiazka);
        }

        public List<string> PosiadaneKsiazkiToListString()
        {
            List<string> ksiazki = new List<string>();
            foreach(Ksiazka ksiazka in posiadaneKsiazki)
            {
                ksiazki.Add($"{ksiazka.tytul}");
            }
            return ksiazki;
        }

        public override string ToString()
        {
            string text = $"Nazwa użytkownika: {nazwaUzytkownika}\nHasło: {haslo}\nAdres dostawy: {adresDostawy}\nPosiadane ksiazki:";
            foreach(Ksiazka ksiazka in posiadaneKsiazki)
            {
                text += $"- {ksiazka.tytul}\n";
            }
            return text;
        }
    }
}
