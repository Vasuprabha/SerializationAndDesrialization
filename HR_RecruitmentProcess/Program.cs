using System;
using HRRecruitment.Entity;

public class HRRecruitmentProcess
{
    private static Application application = new Application();

    public static void Main(string[] args)
    {
        Console.WriteLine("Please login to continue..");
        Console.WriteLine("Enter UserName: ");
        string userName = Console.ReadLine();
        Console.WriteLine("Enter Password: ");
        string password = Console.ReadLine();

        Response verifyUser = application.Login(userName, password);

        if (verifyUser.Status == "Success")
        {
            Console.WriteLine("Welcome!!");
            RunHRRecruitment();
        }
        else
        {
            Console.WriteLine($"Login failed: {verifyUser.Message}");
        }
    }

    public static void RunHRRecruitment()
    {
        while (true)
        {
            Console.WriteLine("\nHR Recruitment Console Application");
            Console.WriteLine("1. Add Candidate Profile");
            Console.WriteLine("2. Show Candidate Status");
            Console.WriteLine("3. Filter Candidates by Skill");
            Console.WriteLine("4. Filter Candidates by name or id");
            Console.WriteLine("5. Schedule Interview");
            Console.WriteLine("6. Update Interview Status");
            Console.WriteLine("7. View Scheduled Interviews");
            Console.WriteLine("8. Remove Candidate Profile");
            Console.WriteLine("9. Logout");
            Console.Write("Enter your choice: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Response addCandidateResponse = application.AddCandidateProfile(GetCandidateDetails());
                            Console.WriteLine(addCandidateResponse.Message);
                            break;
                        case 2:
                            Response getCandidateListResponse = application.GetCandidateList();
                            DisplayCandidates((List<Candidate>)getCandidateListResponse.Data);
                            Console.WriteLine(getCandidateListResponse.Message);
                            break;
                        case 3:
                            string skillToFilter = GetSkillToFilter();
                            Response filterCandidatesBySkillResponse = application.FilterCandidatesBySkill(skillToFilter);
                            DisplayCandidates((List<Candidate>)filterCandidatesBySkillResponse.Data);
                            Console.WriteLine(filterCandidatesBySkillResponse.Message);
                            break;
                        case 4:
                            string filterUser = GetUserByNameOrId();
                            Response filterCandidatesByNameOrIdResponse = application.FilterCandidatesByNameOrId(filterUser);
                            DisplayCandidates((List<Candidate>)filterCandidatesByNameOrIdResponse.Data);
                            Console.WriteLine(filterCandidatesByNameOrIdResponse.Message);
                            break;
                        case 5:
                            Response scheduleInterviewResponse = ScheduleInterview();
                            Console.WriteLine(scheduleInterviewResponse.Message);
                            break;
                        case 6:
                            Response updateInterviewStatusResponse = UpdateInterviewStatus();
                            Console.WriteLine(updateInterviewStatusResponse.Message);
                            break;
                        case 7:
                            Response viewInterviewsDetailsResponse = application.GetInterviewsList();
                            DisplayInterviews("Scheduled Interviews", (List<Interview>)viewInterviewsDetailsResponse.Data);
                            Console.WriteLine(viewInterviewsDetailsResponse.Message);
                            break;
                        case 8:
                            Response removeCandidateProfileResponse = application.RemoveCandidateProfile(GetCandidateIdToRemove());
                            Console.WriteLine(removeCandidateProfileResponse.Message);
                            break;
                        case 9:
                            Console.WriteLine("Logged out successfully!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.");
            }
        }
    }

    private static Candidate GetCandidateDetails()
    {
        Console.Write("Enter candidate name: ");
        string name = Console.ReadLine();
        Console.Write("Enter candidate skills (comma-separated): ");
        List<string> skills = Console.ReadLine().Split(',').Select(s => s.Trim()).ToList();

        return new Candidate { Name = name, Skills = skills };
    }

    private static int GetCandidateIdToRemove()
    {
        Console.Write("Enter candidate ID to remove: ");
        int idToRemove;
        while (!int.TryParse(Console.ReadLine(), out idToRemove))
        {
            Console.WriteLine("Invalid ID. Please enter a valid ID.");
        }
        return idToRemove;
    }

    private static string GetSkillToFilter()
    {
        Console.Write("Enter skill to filter candidates: ");
        return Console.ReadLine();
    }

    private static string GetUserByNameOrId()
    {
        Console.Write("Enter user name or user id to filter candidate: ");
        return Console.ReadLine();
    }

    private static Response ScheduleInterview()
    {
        Console.WriteLine("Enter Candidate ID: ");
        int candidateId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Interviewer ID (Username): ");
        string interviewerId = Console.ReadLine();

        Console.WriteLine("Enter Interview Date (yyyy-MM-dd): ");
        string interviewDate = Console.ReadLine();

        return application.ScheduleInterview(candidateId, interviewerId, interviewDate);
    }

    private static Response UpdateInterviewStatus()
    {
        Console.WriteLine("Enter Interview ID to update status: ");
        int interviewId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter New Interview Status (SCHEDULED, SELECTED, REJECTED): ");
        string statusString = Console.ReadLine();

        Console.WriteLine("Enter Ratings: ");
        int ratings = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Comments: ");
        string comments = Console.ReadLine();

        if (Enum.TryParse(statusString, out InterviewStatus newStatus))
        {
            return application.UpdateInterviewStatus(interviewId, newStatus, ratings, comments);
        }
        else
        {
            Console.WriteLine("Invalid status. Please enter SCHEDULED, SELECTED, or REJECTED.");
            return new Response { Status = "Failure", Message = "Invalid status entered." };
        }
    }

    private static void DisplayCandidates(List<Candidate> candidates)
    {
        if (candidates.Count > 0)
        {
            Console.WriteLine("Candidates:");
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"ID: {candidate.Id}, Name: {candidate.Name}, Skills: {string.Join(", ", candidate.Skills)}");
            }
        }
        else
        {
            Console.WriteLine("No candidates found.");
        }
    }

    private static void DisplayInterviews(string title, List<Interview> interviews)
    {
        Console.WriteLine($"\n{title}:");
        if (interviews.Count > 0)
        {
            foreach (var interview in interviews)
            {
                Console.WriteLine($"ID: {interview.Id}, Candidate ID: {interview.CandidateId}, Interviewer ID: {interview.InterviewerId}, " +
                                  $"Date: {interview.InterviewDate}, Status: {interview.Status}, Rating: {interview.Rating}, Comments: {interview.Comments}");
            }
        }
        else
        {
            Console.WriteLine("No interviews found.");
        }
    }
}
