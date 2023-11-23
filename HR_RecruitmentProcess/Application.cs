using System;
using System.Collections.Generic;
using HRRecruitment.Entity;
using HRRecruitment.Exceptions;
using HRRecruitment.Service;
using HRRecruitment.Helper;


public class Application
{
    private UserHelper userHelper;
    private LoginService loginService;
    private RecruitmentService recruitmentService;
    private InterviewService interviewService;

    public Application()
    {
        userHelper = new UserHelper();
        loginService = new LoginService(userHelper.GetUsers());
        recruitmentService = new RecruitmentService(userHelper);
        interviewService = new InterviewService(userHelper);
    }

    public Response Login(string userName, string password)
    {
        Response response = new Response();
        try
        {
            bool login = loginService.ValidateUser(userName, password);
            if (login)
            {
                response.Status = "Success";
                response.Message = "Login successfully";
            }
            else
            {
                throw new HRRecruitmentException("Invalid username or password.");
            }
        }
        catch (HRRecruitmentException ex)
        {
            response.Status = "Failure";
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.Status = "Failure";
            response.Message = $"An unexpected error occurred: {ex.Message}";
        }
        return response;
    }

    public Response AddCandidateProfile(Candidate candidate)
    {
        Response response = new Response();
        try
        {
            recruitmentService.AddCandidateProfile(candidate);
            response.Status = "Success";
            response.Message = "Candidate profile added successfully";
        }
        catch (InvalidFormatException ex)
        {
            response.Status = "Failure";
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.Status = "Failure";
            response.Message = $"Error adding candidate profile: {ex.Message}";
        }
        return response;
    }

    public Response GetCandidateList()
    {
        Response response = new Response();
        try
        {
            List<Candidate> candidates = recruitmentService.GetCandidateList();
            response.Status = "Success";
            response.Data = candidates;
            response.Message = "Candidate list retrieved successfully";
        }
        catch (ListNotFoundException ex)
        {
            response.Status = "Failure";
            response.Message = $"Error getting candidate list: {ex.Message}";
        }
        return response;
    }

    public Response FilterCandidatesBySkill(string skillToFilter)
    {
        Response response = new Response();
        try
        {
            List<Candidate> filteredCandidates = recruitmentService.FilterCandidatesBySkill(skillToFilter);
            response.Status = "Success";
            response.Data = filteredCandidates;
            response.Message = "Candidates filtered by skill successfully";
        }
        catch (CandidateFilterException ex)
        {
            response.Status = "Failure";
            response.Message = $"Error filtering candidates by skill: {ex.Message}";
        }
        return response;
    }

    public Response FilterCandidatesByNameOrId(string filterUser)
    {
        Response response = new Response();
        try
        {
            List<Candidate> filterCandidates = recruitmentService.FilterCandidatesByNameOrId(filterUser);
            response.Status = "Success";
            response.Data = filterCandidates;
            response.Message = "Candidates filtered by name or ID successfully";
        }
        catch (CandidateFilterException ex)
        {
            response.Status = "Failure";
            response.Message = $"Error filtering candidates by name or ID: {ex.Message}";
        }
        return response;
    }

    public Response RemoveCandidateProfile(int candidateIdToRemove)
    {
        Response response = new Response();
        try
        {
            string removedCandidate = recruitmentService.RemoveCandidateProfile(candidateIdToRemove);
            if (removedCandidate != null)
            {
                response.Status = "Success";
                response.Message = "Candidate profile removed successfully";
            }
            else
            {
                throw new RemoveCandidateException();
            }
        }
        catch (RemoveCandidateException ex)
        {
            response.Status = "Failure";
            response.Message = $"Error removing candidate profile: {ex.Message}";
        }
        return response;
    }

    public Response ScheduleInterview(int candidateId, string interviewerId, string interviewDate)
    {
        Response response = new Response();
        try
        {
            bool scheduleInterview = interviewService.ScheduleInterview(candidateId, interviewerId, interviewDate);
            if (scheduleInterview)
            {
                response.Status = "Success";
                response.Message = "Interview scheduled successfully";
            }
            else
            {
                throw new ScheduleInterviewException("Error scheduling interview");
            }
        }
        catch (ScheduleInterviewException ex)
        {
            response.Status = "Failure";
            response.Message = $"Error scheduling interview: {ex.Message}";
        }
        return response;
    }

    public Response UpdateInterviewStatus(int interviewId, InterviewStatus newStatus, int ratings, string comments)
    {
        Response response = new Response();
        try
        {
            interviewService.UpdateInterviewStatus(interviewId, newStatus, ratings, comments);
            response.Status = "Success";
            response.Message = "Interview status updated successfully";
        }
        catch (InvalidInputException inEx)
        {
            response.Status = "Failure";
            response.Message = $"Error updating interview: {inEx.Message}";
        }
        catch (InterviewStatusException ex)
        {
            response.Status = "Failure";
            response.Message = $"Error updating interview status: {ex.Message}";
        }
        return response;
    }

    public Response GetInterviewsList()
    {
        Response response = new Response();
        try
        {
            List<Interview> interviews = interviewService.GetInterviewsList();
            response.Status = "Success";
            response.Data = interviews;
            response.Message = "Interviews list retrieved successfully";
        }
        catch (ListNotFoundException ex)
        {
            response.Status = "Failure";
            response.Message = $"Error getting interviews list: {ex.Message}";
        }
        return response;
    }

      public void SaveData()
    {
        userHelper.SaveUsers(loginService.Users);
        Candidate.SaveCandidates(recruitmentService.GetCandidateList());
        Interview.SaveInterviews(interviewService.Interviews);
    }

    public void LoadData()
    {
        loginService.Users = userHelper.GetUsers();
        recruitmentService.SetCandidates(Candidate.GetCandidates());
        interviewService.Interviews = Interview.GetInterviews();
    }
}



