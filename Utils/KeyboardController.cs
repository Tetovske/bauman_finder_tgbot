using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PersonFinder.src.Utils
{
    public class KeyboardController
    {
        public InlineKeyboard[] defaultButtons { get; private set; }
        public ReplyKeyboard[] messageButtons { get; private set; }

        public InlineKeyboardMarkup this[InlineKeyboardType type]
        {
            get
            {
                foreach(var keyboard in defaultButtons)
                {
                    if(keyboard.type == type)
                    {
                        return keyboard.keyboard;
                    }
                }
                return null;
            }
            private set
            {
                for(int i = 0; i < defaultButtons.Length; i++)
                {
                    if(defaultButtons[i].type == type)
                    {
                        defaultButtons[i].keyboard = value;
                        break;
                    }
                }
            }
        }
        public void InitKeyboard()
        {

        }
        public enum ReplyKeyboardType
        {
            
        }
        public enum InlineKeyboardType
        {
            settings,
            facultetSelection
        }
        public struct InlineKeyboard
        {
            public InlineKeyboardMarkup keyboard { get; set; }
            public InlineKeyboardType type { get; set; }
            public InlineKeyboard(InlineKeyboardMarkup keyboard, InlineKeyboardType type)
            {
                this.keyboard = keyboard;
                this.type = type;
            }
        }
        public struct ReplyKeyboard
        {
            public ReplyKeyboardMarkup keyboard { get; set; }
            public ReplyKeyboardType type { get; set; }
            public ReplyKeyboard(ReplyKeyboardMarkup keyboard, ReplyKeyboardType type)
            {
                this.keyboard = keyboard;
                this.type = type;
            }
        }
    }
}
