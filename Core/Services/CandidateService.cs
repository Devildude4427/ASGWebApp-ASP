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
        Task<Candidate> FindByUserId(long id);
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

        public async Task<Candidate> FindByUserId(long id)
        {
            return await _candidateRepository.FindByUserId(id);
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