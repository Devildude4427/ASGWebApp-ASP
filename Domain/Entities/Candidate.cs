namespace Domain.Entities
{
    public class Candidate
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ReferenceNumber { get; set; }
        public long ContactInfoId { get; set; }
        public long GeneralInfoId { get; set; }
    }

    public class NewCandidate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role = UserRole.Standard;
        public string PasswordHash { get; set; }
    }
}