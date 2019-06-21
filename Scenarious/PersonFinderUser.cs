using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PersonFinder.src.Scenarious
{
    public class PersonFinderUser
    {
        public User telegramUser { get; set; }
        public Scenario scenario { get; set; }

    }
}
