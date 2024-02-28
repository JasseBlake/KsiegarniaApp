using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class Skarbonka
    {
        public decimal saldo { get; set; }

        public Skarbonka()
        {
            saldo = 0m;
        }

        public Skarbonka(decimal srodki)
        {
            this.saldo = srodki;
        }

        public void DodajSrodki(decimal kwota)
        {
            if (kwota > 0)
            {
                saldo += kwota;
                Console.WriteLine($"Dodano {kwota} zł. Aktualne saldo: {saldo} zł.");
            }
            else
            {
                Console.WriteLine("Kwota do dodania powinna być większa od 0.");
            }
        }

        public bool OdejmijSrodki(decimal kwota)
        {
            if (kwota <= 0)
            {
                Console.WriteLine("Kwota do odjęcia musi być większa od 0.");
                return false;
            }
            else if (saldo >= kwota)
            {
                saldo -= kwota;
                Console.WriteLine($"Odebrano {kwota} zł. Aktualne saldo: {saldo} zł.");
                return true;
            }
            else
            {
                Console.WriteLine("Nie można odjąć kwoty. Saldo skarbonki jest zbyt niskie.");
                return false;
            }
        }
    }
}
