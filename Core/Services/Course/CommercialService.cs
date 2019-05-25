using System;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Models.Course;
using Core.Persistence.Repositories;

namespace Core.Services.Course
{
    public interface ICommercialService
    {
        Task<CandidateResponse> Register(CommercialRegistrationRequest commercialRegistration, string email);
    }
    
    class CommercialService : ICommercialService
    {
        private readonly ICandidateRepository _candidateRepository;

        private readonly IUserService _userService;
        
        public CommercialService(ICandidateRepository candidateRepository, IUserService userService)
        {
            _candidateRepository = candidateRepository;
            _userService = userService;
        }
        
        private async Task<string> GenerateReferenceNumber()
        {
            var previousReferenceNumber = await _candidateRepository.PreviousCandidateReferenceNumber();
            var referenceNumberParts = previousReferenceNumber.Split('-');
            string newReferenceNumber;
            
            if(DateTime.Now.ToString("MM").Equals(referenceNumberParts[3]))
            {
                var newUniqueNum = $"{int.Parse(referenceNumberParts[1]) + 1:000}";
                newReferenceNumber = "ASG-" + DateTime.Now.ToString("yy-MM") + "-" + newUniqueNum;
            } else
                newReferenceNumber = "ASG-" + DateTime.Now.ToString("yy-MM")  + "-" + "001";
            
            return newReferenceNumber;
        }

        public async Task<CandidateResponse> Register(CommercialRegistrationRequest commercialRegistration, string email)
        {
            var candidate = await _candidateRepository.CheckExists(email);
            if(candidate)
                return new CandidateResponse(CandidateRegistrationResponse.AlreadyCourseRegistered);
            
            commercialRegistration.ReferenceNumber = await GenerateReferenceNumber();
            commercialRegistration.UserId = _userService.FindByEmail(email).Id;
            
            var registered = await _candidateRepository.Register(commercialRegistration);
            return registered ? new CandidateResponse(CandidateRegistrationResponse.Successful)
                : new CandidateResponse(CandidateRegistrationResponse.UnknownError);
        }
    }
}