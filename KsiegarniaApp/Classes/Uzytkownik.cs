using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class Uzytkownik:Ksiegarnia
    {
        public string nazwaUzytkownika { get; set; }
        public string haslo { get; set; }

        public Uzytkownik(string nazwaUzytkownika, string haslo)
        {
            this.nazwaUzytkownika = nazwaUzytkownika;
            this.haslo = haslo;
        }

        public bool Uwierzytelnij(string podaneUsername, string podaneHaslo)
        {
            return this.nazwaUzytkownika == podaneUsername && this.haslo == podaneHaslo;
        }

        public bool ZmienHaslo(string stareHaslo, string noweHaslo)
        {
            if(stareHaslo != noweHaslo)
            {
                haslo = noweHaslo;
                return true;
            }
            return false;
        }

        public bool SprawdzHaslo(string haslo)
        {
            return this.haslo == haslo;
        }
    }
}
