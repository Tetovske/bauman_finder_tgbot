using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using iTextSharp;
using System.IO;
using PersonFinder.src.UsersDatabase;

namespace PersonFinder.src
{
    public sealed class Application
    {
        public static bool messagesEnabled { get; private set; } = true;
        public static void Main()
        {
            Initialization();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"Бот запущен!");
            Console.WriteLine(@"Флаги консоли: ");
            Console.WriteLine(@"exit - закрыть бота");
            Console.WriteLine(@"-m - включить/выключить уведомления о входящих сообщениях");
            bool programStillAlive = true;
            while(programStillAlive)
            {
                string flag = Console.ReadLine();
                switch(flag)
                {
                    case "exit":
                        programStillAlive = false;
                        break;
                    case "-m":
                        messagesEnabled = !messagesEnabled;
                        break;
                }
            }
        }

        public static void Initialization()
        {
            Save.InitSave();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            BotController.InitBot();
            List<Document> doc = Database.GetDocuments();
            List<DatabaseCell> database = PdfExplorer.Parse(Database.GetDocuments());
            Database.mainDatabase = new List<DatabaseCell>(database);
        }
    }
}
