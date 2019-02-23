using System;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;

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
        
        public async Task<Candidate> FindByUserId(long id)
        {
            //TODO Work out authorities here
            // if (!_user.IsAuthorisedAtLevel(UserRole.Admin) && _user.Id != id)
            //     throw new UnauthorizedAccessException();
            
            return await _candidateRepository.FindByUserId(id);
        }
    }
}