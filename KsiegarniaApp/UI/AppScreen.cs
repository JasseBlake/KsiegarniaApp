using System;
using KsiegarniaApp.App;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace KsiegarniaApp.UI
{
    public class AppScreen
    {
        public static int WelcomeMenu()
        {
            string[] MenuOpcje1 = { "Administrator", "Kupujacy", "Wyjdz" };
            string prompt = @"


  _  __    _                             _                    __ ___  __ ____   ___  
 | |/ /   (_)                           (_)                  / // _ \/_ |___ \ / _ \ 
 | ' / ___ _  ___  __ _  __ _ _ __ _ __  _  __ _  __      __/ /| (_) || | __) | (_) |
 |  < / __| |/ _ \/ _` |/ _` | '__| '_ \| |/ _` | \ \ /\ / / '_ \__, || ||__ < > _ < 
 | . \\__ \ |  __/ (_| | (_| | |  | | | | | (_| |  \ V  V /| (_) |/ / | |___) | (_) |
 |_|\_\___/_|\___|\__, |\__,_|_|  |_| |_|_|\__,_|   \_/\_/  \___//_/  |_|____/ \___/ 
                   __/ |                                                             
                  |___/                                                              

Witaj w Naszej ksiegarnii.Wybierz Proszę sposob logowania:
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)

";

            int SelectedIndex = Menu.Run(prompt, MenuOpcje1);
            return SelectedIndex;
        }

        public static int czyAdminLogInMenu()
        {
            string[] options = { "Zaloguj się", "Powrót do menu głównego" };
            string prompt = @"
Czy chcesz się zalogować jako Administrator? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";

            int SelectedIndex = Menu.Run(prompt, options);
            return SelectedIndex;
        }

        public static int klientLoginRegisterMenu()
        {
            string[] options = { "Zaloguj się", "Załóż nowe konto", "Powrót do menu głównego" };
            string prompt = @"
Czy chcesz się zalogować/zarejestrować jako Klient? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";

            int SelectedIndex = Menu.Run(prompt, options);
            return SelectedIndex;
        }

        public static int adminOpcjeMenu(bool ksiegarniaOtwarta)
        {
            string[] options = {
                "Wyświetl inwentarz księgarni",
                "Wyświetl klientów księgarni",
                ksiegarniaOtwarta ? "Zamknij ksiegarnie" : "Otwórz księgarnie",
                "Modyfikacja asortymentu",
                "Zmiana hasła",
                "Powrót do menu głównego",
                "Zapisz i wyjdź"
            };
            string prompt = @"
Jesteś zalogowany jako Administrator. Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";

            int SelectedIndex = Menu.Run(prompt, options);
            return SelectedIndex;
        }

        public static int klientOpcjeMenu(string adresKlienta)
        {
            string[] options = {
                "Wyświetl posiadane książki",
                (adresKlienta == "") ? "Dodaj adres dostawy" : "Zmień adres dostawy",
                "Doładuj skarbonke",
                "Wybierz ze skarbonki",
                "Kup książkę",
                "Oceń książkę",
                "Dodaj recenzje",
                "Powrót do menu głównego",
                "Zapisz i wyjdź"
            };
            string prompt = @"
Jesteś zalogowany jako Klient. Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";

            int SelectedIndex = Menu.Run(prompt, options);
            return SelectedIndex;
        }

        public static int ModyfikacjaAsortymentu()
        {
            string[] options = { "Dodaj książkę", "Usuń książkę", "Zmień ilość książki", "Zmień tytuł książki", "Zmień kategorie książki", "Zmień cene książki", "Powrót do menu administratora" };
            string prompt = @"
Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";

            int SelectedIndex = Menu.Run(prompt, options);
            return SelectedIndex;
        }

        public static int ZestawKsiazek(List<string> listOptions)
        {
            string additionalOption = "Powrót do poprzedniego menu";
            listOptions.Add(additionalOption);

            string[] options = listOptions.ToArray();
            string prompt = @"
Wybierz książkę :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";

            int SelectedIndex = Menu.Run(prompt, options);
            return SelectedIndex;
        }
    }
}