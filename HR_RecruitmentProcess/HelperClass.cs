using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using HRRecruitment.Entity;

namespace HRRecruitment.Helper
{
    public class UserHelper
    {
        private const string UsersFilePath = "users.json";

        public List<User> GetUsers()
        {
            if (File.Exists(UsersFilePath))
            {
                string json = File.ReadAllText(UsersFilePath);
                return JsonConvert.DeserializeObject<List<User>>(json);
            }
            else
            {
                return new List<User>
                {
                    new User("user1", "pass123"),
                    new User("user2", "pass456")
                };
            }
        }

        public void SaveUsers(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(UsersFilePath, json);
        }
    }
}
