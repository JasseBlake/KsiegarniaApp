using KsiegarniaApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Console;

namespace KsiegarniaApp.UI
{
    internal static class Utility
    {
        public static void PressEnterToContinue()
        {
            WriteLine("\n\n Nacisjij Enter aby kontynułować...\n");
            ReadLine();
        }

        public static void Wyjdz()//wychodzenie z apliakcji -całkowicie
        {
            Clear();
            WriteLine("\n Nacisji dowolny przycisk by wyjść ");
            ReadKey(true);
            Environment.Exit(0);
        }

        public static SecureString maskInputString()
        {
            SecureString pass = new SecureString();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass.AppendChar(keyInfo.KeyChar);
                    Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                Clear();
                return pass;
            }
        }

        public static void SerializeKsiegarniaToFile(Ksiegarnia ksiegarnia, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true, ReferenceHandler = ReferenceHandler.Preserve };
            string jsonString = JsonSerializer.Serialize(ksiegarnia, options);
            File.WriteAllText(filePath, jsonString);
        }

        public static Ksiegarnia DeserializeKsiegarniaFromFile(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Ksiegarnia>(jsonString, options);
        }

        public static void SerializeAdminToFile(Admin admin, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string jsonString = JsonSerializer.Serialize(admin, options);
            File.WriteAllText(filePath, jsonString);
        }

        public static Admin DeserializeAdminFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Admin>(jsonString);
        }
    }
}
