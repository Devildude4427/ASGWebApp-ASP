using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels
{
    public class UpdateContactDetails
    {
        public Address Address { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public long UserId { get; set; }
    }
}