using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.ViewModels
{
    public class CourseRegistration
    {
        [Required]
        public Address Address { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        
        
        [Required]
        public int EnglishSpeakingLevel { get; set; }
        
        public string Disability { get; set; }
        
        [Required]
        public string PlaceOfBirth { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        
        [Required]
        public string CompanyName { get; set; }
        
        [Required]
        public string FlightExperience { get; set; }
        
        [Required]
        public string PreferredCourseLocation { get; set; }
        
        [Required]
        public Drone Drone { get; set; }
        

        //TODO set up option to pay later
        // public bool Paid = true;
        
        
        public string ReferenceNumber { get; set; }
        public long UserId { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
    
    public class Drone
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }
}