using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class Recenzja
    {
        public Klient autor { get; set; }
        public decimal ocena { get; set; }
        public string opis { get; set; }

        public Recenzja()
        {
            autor = null;
            ocena = -1m;
            opis = string.Empty;
        }

        public Recenzja(Klient autor, decimal ocena)
        {
            this.autor = autor;
            this.ocena = ocena;
            opis = string.Empty;
        }

        public Recenzja(Klient autor, string opis)
        {
            this.autor = autor;
            this.opis = opis;
            ocena = -1m;
        }
    }
}
