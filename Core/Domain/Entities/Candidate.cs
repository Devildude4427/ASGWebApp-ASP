namespace Core.Domain.Entities
{
    public class Candidate
    {
        public string Name { get; set; }
        public string ReferenceNumber { get; set; }
        public string PreferredCourseLocation { get; set; }
        public LastCompletedStage LastCompletedStage { get; set; }
    }

    public class NewCandidate
    {
        public long UserId { get; set; }
        public string ReferenceNumber { get; set; }
        public long ContactInfoId { get; set; }
        public long GeneralInfoId { get; set; }
        public LastCompletedStage CandidateStage = 0;
    }
    
    public enum LastCompletedStage
    {
        //For testing purposes only
        Unregistered = -1,
        
        Registered = 0,
        Paid = 1,
        AssignedCourse = 2,
        CourseJoiningInstructions = 3,
        GroundSchoolSyllabus = 4,
        GroundSchoolExam = 5,
        ApproveExamResults = 6,
        ReturnExamResults = 7,
        GivenOpsManualTemplate = 8,
        ReturnedOpsManual = 9,
        InsuranceCheck = 10,
        FlightAssessment = 11,
        CaaPfCoRecommendation = 12,
        CourseReview = 13
    }
}