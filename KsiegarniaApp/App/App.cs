using KsiegarniaApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Security;
using KsiegarniaApp.Classes;

namespace KsiegarniaApp.App
{
    public class App
    {
        private readonly AppScreen screen;
        private static Ksiegarnia ksiegarnia;
        private static Admin admin;
        private static Klient zalogowanyKlient = null;
        private static string ksiegarniaDataPath;
        private static string adminDataPath;

        public App(string filepath)
        {
            ksiegarniaDataPath = filepath;
            adminDataPath = "adminData.json";
            screen = new AppScreen();

            InitializeData(ksiegarniaDataPath);
            InitializeAdmin(adminDataPath);
        }

        public static void MainMenu()
        {
            int SelectedIndex = AppScreen.WelcomeMenu();
            Clear();
            switch (SelectedIndex)
            {
                case 0:
                    AdminLoginMenu();
                    break;
                case 1:
                    KlientLoginMenu();
                    break;
                case 2:
                    Utility.Wyjdz();
                    break;
            }
        }

        public static void AdminLoginMenu()
        {
            int SelectedIndex = AppScreen.czyAdminLogInMenu();
            Clear();
            switch (SelectedIndex)
            {
                case 0:
                    AdminLogin();
                    break;
                case 1:
                    MainMenu();
                    break;
            }
        }

        public static void AdminLogin()
        {
            Clear();
            WriteLine("Wprowadź nazwę użytkownika do konta administratora:");
            string podanaNazwaUzytkownika = ReadLine();
            WriteLine("Wprowadz hasło do konta Administratora:");
            SecureString pass = Utility.maskInputString();
            string podaneHaslo = new System.Net.NetworkCredential(string.Empty, pass).Password;

            if (admin.Uwierzytelnij(podanaNazwaUzytkownika, podaneHaslo))
            {
                Clear();
                AdminMenu();
            }
            else
            {
                Clear();
                WriteLine("Hasło błędne !! Spróbuj ponownie");
                Thread.Sleep(2000);
                AdminLoginMenu();
            }
        }

        public static void AdminMenu()
        {
            while (true)
            {
                int SelectedIndex = AppScreen.adminOpcjeMenu(ksiegarnia.czyOtwarta);
                Clear();
                switch (SelectedIndex)
                {
                    case 0:
                        WyswietlKsiazkiWKsiegarni();
                        break;
                    case 1:
                        WyswietlKlientowKsiegarni();
                        break;
                    case 2:
                        ksiegarnia.czyOtwarta ^= true;
                        Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
                        WriteLine("Księgarnia jest teraz" + (ksiegarnia.czyOtwarta ? " otwarta." : " zamknięta."));
                        Thread.Sleep(2000);
                        break;
                    case 3:
                        ModyfikacjaAsortymentu();
                        Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
                        break;
                    case 4:
                        ZmianaHaslaAdmin();
                        Utility.SerializeAdminToFile(admin, adminDataPath);
                        break;
                    case 5:
                        MainMenu();
                        return;
                    case 6:
                        Utility.Wyjdz();
                        return;
                    default:
                        WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        Thread.Sleep(2000);
                        Clear();
                        break;
                }
            }
        }

        public static void WyswietlKsiazkiWKsiegarni()
        {
            if (ksiegarnia.InwentarzToListString().Count == 0)
            {
                WriteLine("Nie ma żadnych książek w księgarni.");
                Thread.Sleep(2000);
                Clear();
                return;
            }
            foreach (KsiazkaIlosc ksiazkaIlosc in ksiegarnia.inwentarz)
            {
                WriteLine(ksiazkaIlosc.Ksiazka);
                WriteLine($"Ilość egzemplarzy: {ksiazkaIlosc.Ilosc}\n");
            }
            WriteLine("Wciśnij klawisz aby wrócić do poprzedniego menu.");
            ReadKey();
            Clear();
        }

        public static void WyswietlKlientowKsiegarni()
        {
            if (ksiegarnia.klienci.Count == 0)
            {
                WriteLine("Księgarnia nie ma żadnych klientów.");
                Thread.Sleep(2000);
                Clear();
                return;
            }
            foreach (Klient klient in ksiegarnia.klienci)
            {
                WriteLine(klient);
            }
            WriteLine("\nWciśnij klawisz aby wrócić do poprzedniego menu.");
            ReadKey();
            Clear();
        }

        public static void ModyfikacjaAsortymentu()
        {
            int SelectedIndex = AppScreen.ModyfikacjaAsortymentu();

            switch (SelectedIndex)
            {
                case 0:
                    UtworzKsiazke();
                    break;
                case 1:
                    admin.UsunKsiazke(ksiegarnia, WybierzKsiazke());
                    break;
                case 2:
                    ZmienIlosc();
                    break;
                case 3:
                    ZmienTytul();
                    break;
                case 4:
                    ZmienKategorie();
                    break;
                case 5:
                    ZmienCene();
                    break;
                case 6:
                    return;
            }
        }

        private static void UtworzKsiazke()
        {
            Clear();
            WriteLine("Podaj tytuł książki:");
            string tytul = ReadLine();
            WriteLine("Podaj kategorie książki:");
            string kategoria = ReadLine();
            WriteLine("Podaj cenę książki:");

            decimal cena;
            while (!decimal.TryParse(ReadLine(), out cena) || cena < 0)
            {
                WriteLine("Nieprawidłowa cena. Podaj cenę jeszcze raz (wartość musi być liczbą dodatnią):");
            }

            admin.DodajKsiazke(ksiegarnia, new Ksiazka(tytul, cena, kategoria));
        }

        private static void ZmienIlosc()
        {
            Ksiazka ksiazka = WybierzKsiazke();
            if (ksiazka != null)
            {
                WriteLine("Podaj nową ilość (0 powoduje usunięcie książki):");
                int ilosc;
                string input = ReadLine();
                while (!int.TryParse(input, out ilosc) || ilosc < 0)
                {
                    WriteLine("Nieprawidłowa wartość. Ilość musi być liczbą większą lub równą 0. Spróbuj ponownie:");
                    input = ReadLine();
                }

                if (ilosc > 0)
                {
                    bool result = admin.ZmienIloscKsiazki(ksiegarnia, ksiazka, ilosc);
                    if (result)
                    {
                        WriteLine("Ilość książki została zaktualizowana.");
                    }
                    else
                    {
                        WriteLine("Nie udało się zaktualizować ilości książki.");
                    }
                }
                else if (ilosc == 0)
                {
                    bool result = admin.ZmienIloscKsiazki(ksiegarnia, ksiazka, ilosc);
                    if (result)
                    {
                        WriteLine("Książka została usunięta z inwentarza.");
                    }
                    else
                    {
                        WriteLine("Nie udało się usunąć książki.");
                    }
                }
            }
            else
            {
                return;
            }
            Thread.Sleep(2000);
            Clear();
        }

        private static void ZmienTytul()
        {
            Ksiazka ksiazka = WybierzKsiazke();
            if (ksiazka != null)
            {
                WriteLine("Podaj nowy tytuł książki:");
                string nowyTytul = ReadLine();

                bool result = admin.ZmienTytul(ksiazka, nowyTytul);
                if (result)
                {
                    WriteLine("Tytuł książki został zaktualizowany.");
                }
                else
                {
                    WriteLine("Nie udało się zaktualizować tytułu książki.");
                }
            }
            else
            {
                return;
            }
            Thread.Sleep(2000);
            Clear();
        }

        private static void ZmienKategorie()
        {
            Ksiazka ksiazka = WybierzKsiazke();
            if (ksiazka != null)
            {
                WriteLine("Podaj nową kategorię książki:");
                string nowaKategoria = ReadLine();

                bool result = admin.ZmienKategorie(ksiazka, nowaKategoria);
                if (result)
                {
                    WriteLine("Kategoria książki została zaktualizowana.");
                }
                else
                {
                    WriteLine("Nie udało się zaktualizować kategorii książki.");
                }
            }
            else
            {
                return;
            }
            Thread.Sleep(2000);
            Clear();
        }

        private static void ZmienCene()
        {
            Ksiazka ksiazka = WybierzKsiazke();
            if (ksiazka != null)
            {
                WriteLine("Podaj nową cenę książki:");
                decimal nowaCena;
                string input = ReadLine();
                while (!decimal.TryParse(input, out nowaCena) || nowaCena <= 0)
                {
                    WriteLine("Nieprawidłowa wartość. Cena musi być liczbą dodatnią. Spróbuj ponownie:");
                    input = ReadLine();
                }

                bool result = admin.ZmienCene(ksiazka, nowaCena);
                if (result)
                {
                    WriteLine("Cena książki została zaktualizowana.");
                }
                else
                {
                    WriteLine("Nie udało się zaktualizować ceny książki.");
                }
            }
            else
            {
                return;
            }
            Thread.Sleep(2000);
            Clear();
        }

        private static Ksiazka WybierzKsiazke()
        {
            List<string> ksiazkiIlosci = ksiegarnia.InwentarzToListString();
            if (ksiazkiIlosci.Count == 0)
            {
                WriteLine("Księgarnia nie ma żadnych książek.");
                Thread.Sleep(2000);
                Clear();
                return null;
            }
            int SelectedIndex = AppScreen.ZestawKsiazek(ksiazkiIlosci);
            if (SelectedIndex == ksiazkiIlosci.Count() - 1)
            {
                return null;
            }
            else
            {
                return ksiegarnia.inwentarz.ElementAt(SelectedIndex).Ksiazka;
            }
        }

        public static void ZmianaHaslaAdmin()
        {
            string stareHaslo, noweHaslo;
            do
            {
                do
                {
                    WriteLine("Wprowadź stare hasło:");
                    SecureString pass = Utility.maskInputString();
                    stareHaslo = new System.Net.NetworkCredential(string.Empty, pass).Password;
                    if (!admin.SprawdzHaslo(stareHaslo))
                    {
                        WriteLine("Hasło które wprowadziłaś/eś jest niepoprawne.");
                        Thread.Sleep(1000);
                        Clear();
                    }
                } while (!admin.SprawdzHaslo(stareHaslo));

                WriteLine("Wprowadź nowe hasło:");
                SecureString npass = Utility.maskInputString();
                noweHaslo = new System.Net.NetworkCredential(string.Empty, npass).Password;

                if (noweHaslo == stareHaslo)
                {
                    WriteLine("Hasło musi byc inne.");
                }
                Thread.Sleep(1000);
                Clear();
            } while (!admin.ZmienHaslo(stareHaslo, noweHaslo));

            WriteLine("Hasło zmienione!");
            Thread.Sleep(2000);
            Clear();
        }

        private void InitializeData(string filepath)
        {
            if (File.Exists(filepath))
            {
                ksiegarnia = Utility.DeserializeKsiegarniaFromFile(filepath);
            }
            else
            {
                ksiegarnia = new Ksiegarnia();
                Utility.SerializeKsiegarniaToFile(ksiegarnia, filepath);
            }
        }

        private void InitializeAdmin(string filepath)
        {
            if (File.Exists(filepath))
            {
                admin = Utility.DeserializeAdminFromFile(filepath);
            }
            else
            {
                // Stworzenie admina z domyslnym username i haslem
                admin = new Admin("admin", "admin");
                Utility.SerializeAdminToFile(admin, filepath); // To zapewni ze plik zostanie stworzony
            }
        }


        public static void KlientLoginMenu()
        {
            int SelectedIndex = AppScreen.klientLoginRegisterMenu();
            Clear();
            switch (SelectedIndex)
            {
                case 0:
                    KlientLogin();
                    break;
                case 1:
                    KlientRegister();
                    break;
                case 2:
                    MainMenu();
                    break;
            }
        }

        public static void KlientLogin()
        {
            if(ksiegarnia.klienci.Count == 0)
            {
                WriteLine("Nie ma żadnych zarejestrowanych użytkowników.");
                Thread.Sleep(2000);
                Clear();
                KlientLoginMenu();
                return;
            }
            Clear();
            WriteLine("Wprowadź nazwę użytkownika do konta klienta:");
            string podanaNazwaUzytkownika = ReadLine();
            WriteLine("Wprowadz hasło do konta klienta:");
            SecureString pass = Utility.maskInputString();
            string podaneHaslo = new System.Net.NetworkCredential(string.Empty, pass).Password;

            foreach (var klient in ksiegarnia.klienci)
            {
                if (klient.Uwierzytelnij(podanaNazwaUzytkownika, podaneHaslo))
                {
                    zalogowanyKlient = klient;
                    break;
                }
            }

            if (zalogowanyKlient != null)
            {
                Clear();
                KlientMenu();
            }
            else
            {
                Clear();
                WriteLine("Hasło błędne !! Spróbuj ponownie");
                Thread.Sleep(2000);
                KlientLoginMenu();
            }
        }

        public static void KlientRegister()
        {
            Clear();
            WriteLine("Rejestracja nowego konta klienta:");
            WriteLine("Wprowadź nazwę użytkownika:");
            string nazwaUzytkownika = ReadLine();

            var existingKlient = ksiegarnia.klienci.FirstOrDefault(k => k.nazwaUzytkownika == nazwaUzytkownika);
            if (existingKlient != null)
            {
                WriteLine("Użytkownik o tej nazwie już istnieje. Spróbuj ponownie z inną nazwą.");
                Thread.Sleep(2000);
                Clear();
                KlientLoginMenu();
                return;
            }

            WriteLine("Wprowadz hasło:");
            SecureString pass = Utility.maskInputString();
            string haslo = new System.Net.NetworkCredential(string.Empty, pass).Password;

            Klient newKlient = new Klient(nazwaUzytkownika, haslo);
            ksiegarnia.klienci.Add(newKlient);
            zalogowanyKlient = newKlient;

            WriteLine("Rejestracja zakończona sukcesem.");
            Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
            Thread.Sleep(2000);
            Clear();

            KlientMenu();
        }

        public static void KlientMenu()
        {
            while (true)
            {
                int SelectedIndex = AppScreen.klientOpcjeMenu(zalogowanyKlient.adresDostawy);
                Clear();
                switch (SelectedIndex)
                {
                    case 0:
                        WyswietlPosiadaneKsiazki();
                        break;
                    case 1:
                        ZmianaAdresuKlient();
                        break;
                    case 2:
                        DoladujSkarbonkeKlient();
                        break;
                    case 3:
                        WybierzZeSkarbonki();
                        break;
                    case 4:
                        KupKsiazkeKlient();
                        break;
                    case 5:
                        OcenKsiazkeKlient();
                        break;
                    case 6:
                        WystawRecenzjeKlient();
                        break;
                    case 7:
                        zalogowanyKlient = null;
                        MainMenu();
                        return;
                    case 8:
                        zalogowanyKlient = null;
                        Utility.Wyjdz();
                        return;
                    default:
                        WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        Thread.Sleep(2000);
                        Clear();
                        break;
                }
            }
        }

        private static void WyswietlPosiadaneKsiazki()
        {
            if(zalogowanyKlient.posiadaneKsiazki.Count == 0)
            {
                WriteLine("Nie posiadasz żadnych książek.");
                Thread.Sleep(2000);
                Clear();
                return;
            }
            foreach(Ksiazka ksiazka in zalogowanyKlient.posiadaneKsiazki)
            {
                WriteLine(ksiazka.ToString());
            }
            WriteLine("Wciśnij klawisz aby wrócić do poprzedniego menu.");
            ReadKey();
            Clear();
        }

        private static void ZmianaAdresuKlient()
        {
            if (zalogowanyKlient.adresDostawy != "")
            {
                WriteLine("Wprowadź nowy adres dostawy: ");
            }
            else
            {
                WriteLine("Wprowadź adres dostawy: ");
            }
            zalogowanyKlient.adresDostawy = ReadLine();
            WriteLine("Adres został poprawnie zmieniony");
            Thread.Sleep(2000);
            Clear();
            Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
        }

        private static void DoladujSkarbonkeKlient()
        {
            Write("Podaj kwote do doladowania: ");

            decimal kwota;
            while (!decimal.TryParse(ReadLine(), out kwota) || kwota <= 0)
            {
                WriteLine("Nieprawidlowa kwota. Kwota musi byc liczba dodatnia.");
                Write("Sprobuj ponownie: ");
            }

            zalogowanyKlient.skarbonka.DodajSrodki(kwota);
            Thread.Sleep(2000);
            Clear();
            Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
        }

        private static void WybierzZeSkarbonki()
        {
            Write("Podaj kwote do wybrania: ");

            decimal kwota;
            while (!decimal.TryParse(ReadLine(), out kwota) || kwota <= 0)
            {
                WriteLine("Nieprawidlowa kwota. Kwota musi byc liczba dodatnia.");
                Write("Sprobuj ponownie: ");
            }

            zalogowanyKlient.skarbonka.OdejmijSrodki(kwota);
            Thread.Sleep(2000);
            Clear();
            Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
        }

        private static void KupKsiazkeKlient()
        {
            if (!ksiegarnia.czyOtwarta)
            {
                WriteLine("Księgarnia jest zamknięta, nie można dokonać zakupu.");
                Thread.Sleep(2000);
                Clear();
                return;
            }
            if(zalogowanyKlient.adresDostawy == "")
            {
                WriteLine("Nie podałaś/eś adresu dostawy, nie można dokonać zakupu.");
                Thread.Sleep(2000);
                Clear();
                return;
            }
            Ksiazka ksiazka = WybierzKsiazke();
            if(ksiazka == null)
            {
                return;
            }
            if(ksiazka.cena > zalogowanyKlient.skarbonka.saldo)
            {
                WriteLine($"Nie masz wystarczających środków w skarbonce.\nSaldo {zalogowanyKlient.skarbonka.saldo}\nCena ksiazki: {ksiazka.cena}");
                Thread.Sleep(2000);
                Clear();
                return;
            }
            if (zalogowanyKlient.KupKsiazke(ksiazka))
            {
                admin.UsunKsiazke(ksiegarnia, ksiazka);
                WriteLine("Książka została poprawnie zakupiona.");
                Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
            }
            else
            {
                WriteLine("Posiadasz już tą książkę.");
            }
            Thread.Sleep(2000);
            Clear();
        }

        private static void OcenKsiazkeKlient()
        {
            if (zalogowanyKlient.posiadaneKsiazki.Count == 0)
            {
                WriteLine("Nie posiadasz żadnych książek.");
                Thread.Sleep(2000);
                Clear();
                return;
            }
            List<string> ksiazki = zalogowanyKlient.PosiadaneKsiazkiToListString();
            int SelectedIndex = AppScreen.ZestawKsiazek(ksiazki);
            if (SelectedIndex < ksiazki.Count() - 1)
            {
                Ksiazka ksiazkaDoOceny = zalogowanyKlient.posiadaneKsiazki.ElementAt(SelectedIndex);
                decimal ocena;

                WriteLine("Wprowadź ocene [1-5]:");
                while (!decimal.TryParse(Console.ReadLine(), out ocena) || ocena < 1 || ocena > 5)
                {
                    WriteLine("Nieprawidłowa wartość. Ocena musi być liczbą całkowitą od 1 do 5.");
                    WriteLine("Wprowadź ocene ponownie [1-5]:");
                }

                if (zalogowanyKlient.WystawOcene(ksiazkaDoOceny, ocena))
                {
                    WriteLine($"Dziękujemy za ocenę książki: {ksiazkaDoOceny.tytul}.");
                    Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
                }
                else
                {
                    WriteLine("Nie udało się wystawić oceny. Ta książka została już wcześniej oceniona.");
                }
            }
            Thread.Sleep(2000);
            Clear();
        }

        private static void WystawRecenzjeKlient()
        {
            if (zalogowanyKlient.posiadaneKsiazki.Count == 0)
            {
                WriteLine("Nie posiadasz żadnych książek.");
                Thread.Sleep(2000);
                Clear();
                return;
            }

            List<string> ksiazki = zalogowanyKlient.PosiadaneKsiazkiToListString();
            int SelectedIndex = AppScreen.ZestawKsiazek(ksiazki);
            if (SelectedIndex < ksiazki.Count() - 1)
            {
                Ksiazka ksiazkaDoRecenzji = zalogowanyKlient.posiadaneKsiazki.ElementAt(SelectedIndex);
                string opis;

                WriteLine("Wprowadź opis recenzji:");
                opis = ReadLine();
                while(opis == null || opis.Length == 0)
                {
                    WriteLine("Opis recenzji nie może być pusty.\nWprowadź opis recenzji:");
                    opis = ReadLine();
                }

                if (zalogowanyKlient.WystawRecenzje(ksiazkaDoRecenzji, opis))
                {
                    WriteLine($"Dziękujemy za recenzje książki: {ksiazkaDoRecenzji.tytul}.");
                    Utility.SerializeKsiegarniaToFile(ksiegarnia, ksiegarniaDataPath);
                }
                else
                {
                    WriteLine("Nie udało się wystawić recenzji. Ta książka została już wcześniej opisana.");
                }
            }
            Thread.Sleep(2000);
            Clear();
        }
    }
}

