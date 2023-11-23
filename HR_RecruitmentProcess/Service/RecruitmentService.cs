namespace HRRecruitment.Service
{
    using HRRecruitment.Entity;
    using HRRecruitment.Helper;
    using System.Collections.Generic;
    using System.Linq;

    public class RecruitmentService
    {
        private UserHelper userHelper;
        private int candidateIdCounter;
        private List<Candidate> candidates;

        public RecruitmentService(UserHelper userHelper)
        {
            this.userHelper = userHelper;
            this.candidates = Candidate.GetCandidates(); // Initialize candidates from storage
        }

        public void AddCandidateProfile(Candidate candidate)
        {
            candidate.Id = candidateIdCounter++;
            candidates.Add(candidate);
        }

        public List<Candidate> GetCandidateList()
        {
            return candidates;
        }

        public string RemoveCandidateProfile(int candidateId)
        {
            Candidate candidateToRemove = candidates.FirstOrDefault(candidate => candidate.Id == candidateId);
            if (candidateToRemove != null)
            {
                candidates.Remove(candidateToRemove);
                return $"{candidateToRemove}";
            }
            else
            {
                return null;
            }
        }

        public List<Candidate> FilterCandidatesBySkill(string skillToFilter)
        {
            return candidates
                .Where(candidate => candidate.Skills.Contains(skillToFilter))
                .ToList();
        }

        public List<Candidate> FilterCandidatesByNameOrId(string userInput)
        {
            int userId;
            bool isNumeric = int.TryParse(userInput, out userId);
            var filteredCandidates = candidates
                .Where(candidate => candidate.Name.Contains(userInput) || (isNumeric && candidate.Id == userId))
                .ToList();
            return filteredCandidates;
        }

        public void SetCandidates(List<Candidate> candidates)
        {
            this.candidates = candidates;
        }
    }
}
