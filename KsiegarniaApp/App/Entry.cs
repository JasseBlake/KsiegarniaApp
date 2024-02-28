using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace KsiegarniaApp.App
{
    class Entry
    {
        static void Main(string[] args)
        {
            Title = "Ksiegarnia w69138";
            App ksiegarniaApp = new App("data.json");
            App.MainMenu();
        }
    }
}
