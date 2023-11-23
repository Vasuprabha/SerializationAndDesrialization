using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using HRRecruitment.Exceptions;

namespace HRRecruitment.Entity
{
    public class Candidate
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (isValidUserName(value))
                {
                    name = value;
                }
                else
                {
                    throw new InvalidFormatException();
                }
            }
        }
        public List<string> Skills { get; set; }

        private bool isValidUserName(string candidateName)
        {
            return !string.IsNullOrWhiteSpace(candidateName) && candidateName.All(char.IsLetter);
        }

        public static List<Candidate> GetCandidates()
        {
            string filePath = "candidates.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Candidate>>(json);
            }
            else
            {
                return new List<Candidate>();
            }
        }

        public static void SaveCandidates(List<Candidate> candidates)
        {
            string filePath = "candidates.json";
            string json = JsonConvert.SerializeObject(candidates, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Assuming 'User' is part of the HRRecruitment.Entity namespace
        public List<User> Users { get; set; }
    }
}
