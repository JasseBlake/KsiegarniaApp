using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiegarniaApp.Classes
{
    internal class Ksiazka:Ksiegarnia
    {
        public string tytul { get; set; }
        public List<Recenzja> recenzje { get; set; }
        public decimal sredniaOcen { get; set; }
        public decimal cena { get; set; }
        public string kategoria { get; set; }

        public Ksiazka()
        {
            tytul = "Nieznany";
            recenzje = new List<Recenzja>();
            sredniaOcen = 0m;
            cena = 0m;
            kategoria = "Nieznana";
        }

        public Ksiazka(string tytul, decimal cena)
        {
            this.tytul = tytul;
            this.cena = cena;
            recenzje = new List<Recenzja>();
            sredniaOcen = 0m;
            kategoria = "Nieznana";
        }

        public Ksiazka(string tytul, decimal cena, string kategoria)
        {
            this.tytul = tytul;
            this.cena = cena;
            recenzje = new List<Recenzja>();
            sredniaOcen = 0m;
            this.kategoria = kategoria;
        }

        public Ksiazka(string tytul, decimal cena, List<Recenzja> recenzje)
        {
            this.tytul = tytul;
            this.cena = cena;
            this.recenzje = recenzje;
            foreach(Recenzja recenzja in recenzje)
            {
                if (recenzja.ocena >= 1 && recenzja.ocena <= 5)
                {
                    sredniaOcen += recenzja.ocena;
                }
            }
            sredniaOcen /= recenzje.Count;
            kategoria = "Nieznana";
        }

        public Ksiazka(string tytul, decimal cena, string kategoria, List<Recenzja> recenzje)
        {
            this.tytul = tytul;
            this.cena = cena;
            this.recenzje = recenzje;
            foreach (Recenzja recenzja in recenzje)
            {
                if (recenzja.ocena >= 1 && recenzja.ocena <= 5)
                {
                    sredniaOcen += recenzja.ocena;
                }
            }
            sredniaOcen /= recenzje.Count;
            this.kategoria = kategoria;
        }

        public void DodajRecenzje(Recenzja recenzja)
        {
            recenzje.Add(recenzja);
            sredniaOcen += (recenzja.ocena - sredniaOcen)/recenzje.Count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string formattedAverageRating = sredniaOcen.ToString("0.00");

            sb.AppendLine($"Tytuł: {tytul}, Kategoria: {kategoria}, Cena: {cena} zł, Średnia ocen: {formattedAverageRating}, Liczba recenzji: {recenzje.Count}");

            if (recenzje.Any())
            {
                sb.AppendLine("Recenzje:");
                foreach (var recenzja in recenzje)
                {
                    string autorUsername = recenzja.autor != null ? recenzja.autor.nazwaUzytkownika : "Anonim";
                    sb.AppendLine($"- Autor: {autorUsername}, Ocena: {recenzja.ocena}\nOpis: \"{recenzja.opis}\"\n");
                }
            }
            else
            {
                sb.AppendLine("Brak recenzji.");
            }

            return sb.ToString();
        }

    }
}
