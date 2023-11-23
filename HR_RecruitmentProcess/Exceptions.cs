namespace HRRecruitment.Exceptions
{
    using System;

    public class HRRecruitmentException : Exception
    {
        public HRRecruitmentException(string message) : base(message)
        {
        }
    }

    public class InvalidInputException : HRRecruitmentException
    {
        public InvalidInputException(string message) : base($"Invalid input for {message} please try again!")
        {
        }
    }

    public class InvalidFormatException : HRRecruitmentException
    {
        public InvalidFormatException() : base("Invalid name format!! Name should contain alphabets only")
        {
        }
    }

    public class InterviewStatusException : HRRecruitmentException
    {
        public InterviewStatusException() : base("Invalid interview status. Please enter SELECTED or REJECTED")
        {
        }
    }

    public class ListNotFoundException : HRRecruitmentException
    {
        public ListNotFoundException(string message) : base(message)
        {
        }
    }

    public class CandidateFilterException : HRRecruitmentException
    {
        public CandidateFilterException(string message) : base(message)
        {
        }
    }

    public class RemoveCandidateException : HRRecruitmentException
    {
        public RemoveCandidateException() : base("Candidate id not found ")
        {
        }
    }

    public class ScheduleInterviewException : HRRecruitmentException
    {
        public ScheduleInterviewException(string message) : base(message)
        {
        }
    }
}
