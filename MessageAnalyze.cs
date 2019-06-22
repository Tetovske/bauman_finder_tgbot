using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using PersonFinder.src.UsersDatabase;
using PersonFinder.src.Scenarious;

namespace PersonFinder.src
{
    public static class MessageAnalyze
    {
        public static void MessageFromUser(Message message)
        {
            // Текстовые сообщения
            if(message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if(Application.messagesEnabled) Utilities.Notify($"{message.From.FirstName} [{DateTime.Now.Hour}:{DateTime.Now.Minute}]: {message.Text}");
                PersonFinderUser currentUser = UsersDatabase.UsersDatabase.CheckIsUserInDatabase(message.From.Id);
                if(currentUser == null)
                {
                    currentUser = new Scenarious.PersonFinderUser()
                    {
                        telegramUser = message.From,
                        userID = message.From.Id
                    };
                    Save.users.Add(currentUser);
                }
                string str = message.Text;
                if (str == "/start")
                {
                    Responce<string> responce = new Responce<string>()
                    {
                        id = message.From.Id,
                        messageType = Responce<string>.ResponceType.text,
                        message = $"⚡️Введите ФИО человека: "
                    };
                    BotController.SendMessage<string>(responce);
                }
                else
                {

                }
            }
            else if(message.Type == Telegram.Bot.Types.Enums.MessageType.Sticker)
            {

            }
        }
    }

    public class Responce<T>
    {
        public T message { get; set; }
        public ResponceType messageType { get; set; }
        public ChatId id { get; set; }
        public enum ResponceType
        {
            text,
            image
        }
    }
}
