namespace HRRecruitment.Service
{
    using HRRecruitment.Entity;
    using HRRecruitment.Exceptions;
    using HRRecruitment.Helper;
    using System.Collections.Generic;
    using System.Linq;

    public class InterviewService
    {
        private UserHelper userHelper;

        // Correct the property names
        public List<Interview> Interviews { get; set; }
        public int InterviewIdCounter { get; set; }

        public InterviewService(UserHelper userHelper)
        {
            this.userHelper = userHelper;
            // Correct the property names
            Interviews = Interview.GetInterviews(); // Initialize interviews from storage
        }

        public bool ScheduleInterview(int candidateId, string interviewerId, string interviewDate)
        {
            // Correct the property names
            Interviews.Add(new Interview
            {
                Id = InterviewIdCounter++,
                CandidateId = candidateId,
                InterviewerId = interviewerId,
                InterviewDate = interviewDate,
                Status = InterviewStatus.SCHEDULED
            });

            Interview.SaveInterviews(Interviews);
            return true;
        }

        public void UpdateInterviewStatus(int interviewId, InterviewStatus newStatus, int ratings, string comments)
        {
            Interview interviewToUpdate = Interviews.Find(interview => interview.Id == interviewId);
            if (interviewToUpdate != null)
            {
                if (interviewToUpdate.Status == InterviewStatus.SCHEDULED && (newStatus == InterviewStatus.SELECTED || newStatus == InterviewStatus.REJECTED))
                {
                    interviewToUpdate.Status = newStatus;
                    interviewToUpdate.Rating = ratings;
                    interviewToUpdate.Comments = comments;
                }
                else
                {
                    throw new InterviewStatusException();
                }

                Interview.SaveInterviews(Interviews);
            }
            else
            {
                throw new InvalidInputException($"interview id {interviewId}");
            }
        }

        public List<Interview> GetInterviewsList()
        {
            Interviews = Interview.GetInterviews();
            return Interviews;
        }
    }
}

//dotnet add package Newtonsoft.Json
