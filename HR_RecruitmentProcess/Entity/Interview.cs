using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace HRRecruitment.Entity
{
    public class Interview
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string InterviewerId { get; set; }
        public string InterviewDate { get; set; }
        public InterviewStatus Status { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }

        private const string InterviewsFilePath = "interviews.json";

        public static List<Interview> GetInterviews()
        {
            if (File.Exists(InterviewsFilePath))
            {
                string json = File.ReadAllText(InterviewsFilePath);
                return JsonConvert.DeserializeObject<List<Interview>>(json);
            }
            else
            {
                return new List<Interview>();
            }
        }

        public static void SaveInterviews(List<Interview> interviews)
        {
            string json = JsonConvert.SerializeObject(interviews, Formatting.Indented);
            File.WriteAllText(InterviewsFilePath, json);
        }
    }

    public enum InterviewStatus
    {
        SCHEDULED,
        SELECTED,
        REJECTED
    }
}
