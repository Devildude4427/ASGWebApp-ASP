using System;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ViewModels;

namespace Services
{
    public class CandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        private readonly IUserIdentity _user;

        public CandidateService(ICandidateRepository candidateRepository, IUserIdentity user)
        {
            _candidateRepository = candidateRepository;
            _user = user;
        }
        
        //TODO Tests can't give a user identity, so figure out whether identities should be gathered here, or elsewhere
        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        
        public async Task<Candidate> FindByUserId()
        {
            return await _candidateRepository.FindByUserId(_user.Id);
        }

        public async Task<string> GenerateReferenceNumber()
        {
            var previousReferenceNumber = await _candidateRepository.PreviousCandidateReferenceNumber();
            var referenceNumberParts = previousReferenceNumber.Split('-');
            string newReferenceNumber;
            
            if(DateTime.Now.ToString("MM").Equals(referenceNumberParts[3]))
            {
                var newUniqueNum = $"{(int.Parse(referenceNumberParts[1]) + 1):000}";
                newReferenceNumber = "ASG-" + newUniqueNum + "-" + DateTime.Now.ToString("yy-MM");
            } else
                newReferenceNumber = "ASG-" + "001" + "-" + DateTime.Now.ToString("yy-MM");
            
            return newReferenceNumber;
        }

        public async Task<CandidateResponse> Register(CourseRegistration courseRegistration)
        {
            var user = await FindByUserId();
            if(user != null)
                return new CandidateResponse(CandidateRegistrationResponse.AlreadyCourseRegistered);
            
            courseRegistration.ReferenceNumber = await GenerateReferenceNumber();
            courseRegistration.UserId = _user.Id;
            
            var registered = await _candidateRepository.Register(courseRegistration);
            return registered ? new CandidateResponse(CandidateRegistrationResponse.Successful)
                : new CandidateResponse(CandidateRegistrationResponse.UnknownError);
        }
        
        public async Task<bool> UpdateDetails(UpdateContactDetails updateContactDetails)
        {
            updateContactDetails.UserId = _user.Id;
            return await _candidateRepository.UpdateDetails(updateContactDetails);
        }
    }
    
    public class CandidateResponse
    {
        public CandidateRegistrationResponse CandidateRegistrationResponse { get; }
        
        public CandidateResponse(CandidateRegistrationResponse candidateRegistrationResponse)
        {
            CandidateRegistrationResponse = candidateRegistrationResponse;
        }
    }

    public enum CandidateRegistrationResponse
    {
        AlreadyCourseRegistered,
        Successful,
        UnknownError
    }
}