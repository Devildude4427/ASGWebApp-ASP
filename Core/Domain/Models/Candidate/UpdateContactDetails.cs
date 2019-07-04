using Core.Domain.Models.Course;

namespace Core.Domain.Models.Candidate
{
    public class UpdateContactDetails
    {
        public Address Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        
        public long UserId { get; set; }
    }
}