using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using iTextSharp;
using System.IO;

namespace PersonFinder.src
{
    public sealed class Application
    {
        static string botAPI = "761033843:AAF8yccr-S976aCGLtjNrJvWeQ4IvtkIyuIa";
        public static TelegramBotClient bot;
        private static bool messagesEnabled = true;
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
            bot = new TelegramBotClient(botAPI)
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            List<Document> doc = Database.GetDocuments();
            List<DatabaseCell> database = PdfExplorer.Parse(Database.GetDocuments());
            Database.mainDatabase = new List<DatabaseCell>(database);
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            if(e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (messagesEnabled) Console.WriteLine($"{e.Message.From.FirstName} [{DateTime.Now.Hour}:{DateTime.Now.Minute}]: {e.Message.Text}");
                if (e.Message.Text == "/start")
                {
                    bot.SendTextMessageAsync(e.Message.Chat.Id, $"⚡️Введите ФИО человека: ");
                }
                else
                {
                    List<Person> res = PdfExplorer.Find(e.Message.Text);
                    if (res != null && res.Count > 0)
                    {
                        if (res.Count == 1)
                        {
                            string formOfStudy = "";
                            switch (res[0].formOfStudy)
                            {
                                case Person.StudyForm.budget: formOfStudy = "бюджет"; break;
                                case Person.StudyForm.paid: formOfStudy = "платная"; break;
                                case Person.StudyForm.targeting: formOfStudy = "целевик"; break;
                            }
                            bot.SendTextMessageAsync(e.Message.Chat.Id, $"Найден студент:\n\n✅{res[0].fullName}\n💳 Форма обучения: {formOfStudy}\n📚 Сумма баллов: " +
                                $"{res[0].points}\n⚡️ Группа: {res[0].group}\n📅 Год поступления: {res[0].year}");
                        }
                        else if (res.Count > 1)
                        {
                            bot.SendTextMessageAsync(e.Message.Chat.Id, $"Найдено студентов: {res.Count}\n");
                            int counter = 0;
                            string responce = "";
                            foreach (Person per in res)
                            {
                                string formOfStudy = "";
                                switch (per.formOfStudy)
                                {
                                    case Person.StudyForm.budget: formOfStudy = "бюджет"; break;
                                    case Person.StudyForm.paid: formOfStudy = "платная"; break;
                                    case Person.StudyForm.targeting: formOfStudy = "целевик"; break;
                                }
                                responce += $"✅{per.fullName}\n💳 Форма обучения: {formOfStudy}\n📚 Сумма баллов: " +
                                    $"{per.points}\n⚡️ Группа: {per.group}\n📅 Год поступления: {per.year}\n\n--------------------\n\n";
                                counter++;
                                if (counter > 10)
                                {
                                    counter = 0;
                                    bot.SendTextMessageAsync(e.Message.Chat.Id, responce);
                                    responce = "";
                                }
                            }
                            bot.SendTextMessageAsync(e.Message.Chat.Id, responce);
                        }
                    }
                    else bot.SendTextMessageAsync(e.Message.Chat.Id, $"Студент не найден");
                }
            }
        }
    }
}
