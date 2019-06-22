using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace PersonFinder.src
{
    public static class BotController
    {
        public static ITelegramBotClient bot { get; private set; }
        //private static string apiKey = ""; 
        private static string apiKey = "";
        public static void InitBot()
        {
            bot = new TelegramBotClient(apiKey)
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            bot.StartReceiving();
            bot.OnMessage += MessageRecieved;
        }

        private static void MessageRecieved(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            MessageAnalyze.MessageFromUser(e.Message);
            /*
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (Application.messagesEnabled) Console.WriteLine($"{e.Message.From.FirstName} [{DateTime.Now.Hour}:{DateTime.Now.Minute}]: {e.Message.Text}");
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
            */
        }

        public static async void SendMessage<T>(Responce<T> responce)
        {
            try
            {
                switch (responce.messageType)
                {
                    case Responce<T>.ResponceType.text:
                        await bot.SendTextMessageAsync(responce.id, responce.message.ToString());
                        break;
                }
            }
            catch(Exception ex)
            {
                Utilities.LogError(ex.ToString());
            }
        }
    }
}
