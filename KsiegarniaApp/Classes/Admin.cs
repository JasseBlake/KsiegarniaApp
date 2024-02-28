using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class Admin : Uzytkownik
    {
        public Admin(string nazwaUzytkownika, string haslo) : base(nazwaUzytkownika, haslo) { }

        public bool DodajKsiazke(Ksiegarnia ksiegarnia, Ksiazka ksiazka)
        {
            if (ksiegarnia == null || ksiazka == null)
            {
                return false;
            }

            var istniejacaKsiazkaIlosc = ksiegarnia.inwentarz.FirstOrDefault(ki => ki.Ksiazka.tytul == ksiazka.tytul);

            if (istniejacaKsiazkaIlosc != null)
            {
                istniejacaKsiazkaIlosc.Ilosc += 1;
            }
            else
            {
                ksiegarnia.inwentarz.Add(new KsiazkaIlosc(ksiazka, 1));
            }
            return true;
        }


        public bool UsunKsiazke(Ksiegarnia ksiegarnia, Ksiazka ksiazka)
        {
            if (ksiegarnia == null || ksiazka == null)
            {
                return false;
            }

            var istniejacaKsiazkaIloscIndex = ksiegarnia.inwentarz.FindIndex(ki => ki.Ksiazka.tytul == ksiazka.tytul);
            if (istniejacaKsiazkaIloscIndex != -1)
            {
                if(ksiegarnia.inwentarz.ElementAt(istniejacaKsiazkaIloscIndex).Ilosc == 1)
                {
                    ksiegarnia.inwentarz.RemoveAt(istniejacaKsiazkaIloscIndex);
                }
                else
                {
                    ksiegarnia.inwentarz.ElementAt(istniejacaKsiazkaIloscIndex).Ilosc--;
                }
                    
                return true;
            }
            return false;
        }

        public bool ZmienIloscKsiazki(Ksiegarnia ksiegarnia, Ksiazka ksiazka, int nowaIlosc)
        {
            if (ksiegarnia == null || ksiazka == null || nowaIlosc < 0)
            {
                return false;
            }

            var istniejacaKsiazkaIlosc = ksiegarnia.inwentarz.FirstOrDefault(ki => ki.Ksiazka.tytul == ksiazka.tytul);
            if (istniejacaKsiazkaIlosc != null)
            {
                if (nowaIlosc == 0)
                {
                    ksiegarnia.inwentarz.Remove(istniejacaKsiazkaIlosc);
                }
                else
                {
                    istniejacaKsiazkaIlosc.Ilosc = nowaIlosc;
                }
                return true;
            }
            return false;
        }

        public bool ZmienTytul(Ksiazka ksiazka, string nowyTytul)
        {
            if (ksiazka != null)
            {
                ksiazka.tytul = nowyTytul;
                return true;
            }
            return false;
        }

        public bool ZmienKategorie(Ksiazka ksiazka, string nowaKategoria)
        {
            if (ksiazka != null)
            {
                ksiazka.kategoria = nowaKategoria;
                return true;
            }
            return false;
        }

        public bool ZmienCene(Ksiazka ksiazka, decimal nowaCena)
        {
            if (ksiazka != null)
            {
                ksiazka.cena = nowaCena;
                return true;
            }
            return false;
        }

        public bool ZamknijKsiegarnie(Ksiegarnia ksiegarnia)
        {
            if(ksiegarnia != null && ksiegarnia.czyOtwarta)
            {
                ksiegarnia.czyOtwarta = false;
                return true;
            }
            return false;
        }

        public bool OtworzKsiegarnie(Ksiegarnia ksiegarnia)
        {
            if (ksiegarnia != null && !ksiegarnia.czyOtwarta)
            {
                ksiegarnia.czyOtwarta = true;
                return true;
            }
            return false;
        }
    }
}
