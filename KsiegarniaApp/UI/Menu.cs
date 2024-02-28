using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace KsiegarniaApp.UI
{
    class Menu
    {
        private static int SelectedIndex;
                                                           
        private static void DisplayOptions(string? prompt, string[] Options)
        {
            WriteLine(prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string CurrentOption = Options[i];

                string prefix;
                if (i == SelectedIndex)
                {
                    prefix = "* ";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                WriteLine($"{prefix}<< {CurrentOption} >>");
            }
            ResetColor();
        }

        public static int Run(string? prompt, string[] Options)
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                Menu.DisplayOptions(prompt, Options);//wyswietlanie opcji
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    if (--SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    if (++SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            int selectedOption = SelectedIndex;
            SelectedIndex = 0;//trzeba zeby zawsze menu było ustawione najpierw na pierwszej opcji!
            return selectedOption;
        }
    }
}