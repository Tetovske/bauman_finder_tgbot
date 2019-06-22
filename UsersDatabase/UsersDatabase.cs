using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonFinder.src.Scenarious;
using Newtonsoft.Json;
using System.IO;

namespace PersonFinder.src.UsersDatabase
{
    [Serializable]
    public class UsersDatabase
    {
        public List<PersonFinderUser> users;
        public UsersDatabase(List<PersonFinderUser> users)
        {
            this.users = new List<PersonFinderUser>(users);
        }

        public static PersonFinderUser CheckIsUserInDatabase(int id)
        {
            if(Save.users != null)
            {
                foreach (var user in Save.users)
                {
                    if (user.userID == id) return user;
                }
                return null;
            }
            else
            {
                Utilities.LogError("Список пользователей не инициализирован");
                return null;
            }
        }
    }

    public static class Save
    {
        public static List<PersonFinderUser> users { get; set; } = new List<PersonFinderUser>();
        public static string path;
        public static UsersDatabase InitSave()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "data.json");
            if(!File.Exists(path)) return null;
            else
            {
                try
                {
                    File.OpenRead(path);
                    string fileStr = "";
                    string[] fileStrs = File.ReadAllLines(path);
                    foreach (string str in fileStrs) fileStr += str;
                    UsersDatabase data = JsonConvert.DeserializeObject<UsersDatabase>(fileStr) as UsersDatabase;
                    return data;
                }
                catch(Exception ex)
                {
                    Utilities.LogError(ex.ToString());
                    return null;
                }
            }
        }

        public static void SaveData(UsersDatabase user)
        {
            FileStream stream; 
            if (!File.Exists("data.json")) stream = File.Create(path);
            else
            {
                using (File.Open(path, FileMode.Create))
                {
                    string jsonString = JsonConvert.SerializeObject(user);
                    string[] jsonStrings = new string[jsonString.Length / 200];
                    int strCounter = 0, c = 0;
                    for (int i = 0; i < jsonString.Length / 200; i++)
                    {
                        jsonStrings[c] += jsonString[i];
                        if (strCounter > 199)
                        {
                            c++;
                            strCounter = 0;
                        }
                        strCounter++;
                    }
                    File.WriteAllLines(path, jsonStrings);
                }
            }
        }
    }
}
