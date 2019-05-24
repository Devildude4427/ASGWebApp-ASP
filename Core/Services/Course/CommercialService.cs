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
    
    public class CommercialService : ICommercialService
    {
        private readonly ICandidateRepository _candidateRepository;

        private readonly IUserIdentity _user;
        
        public CommercialService(ICandidateRepository candidateRepository, IUserIdentity user)
        {
            _candidateRepository = candidateRepository;
            _user = user;
        }
        
        public async Task<string> GenerateReferenceNumber()
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
            commercialRegistration.UserId = _user.Id;
            
            var registered = await _candidateRepository.Register(commercialRegistration);
            return registered ? new CandidateResponse(CandidateRegistrationResponse.Successful)
                : new CandidateResponse(CandidateRegistrationResponse.UnknownError);
        }
    }
}