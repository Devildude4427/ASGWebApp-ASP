using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.ViewModels;
using Core.Persistence.Repositories;

namespace Core.Services
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetAll();
        Task<bool> UpdateDetails(UpdateContactDetails updateContactDetails);
    }
    
    class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        private readonly IUserIdentity _user;
        
        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<Candidate>> GetAll()
        {
            return await _candidateRepository.GetAll();
        }

        private async Task<Candidate> FindByUserId(long userId)
        {
            return await _candidateRepository.FindByUserId(userId);
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