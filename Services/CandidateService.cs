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

        public async Task<string> GenerateReferenceNumber()
        {
            var previousReferenceNumber = await _candidateRepository.PreviousCandidateReferenceNumber();
            var referenceNumberParts = previousReferenceNumber.Split('-');
            string newReferenceNumber;
            
            if(DateTime.Now.ToString("MM").Equals(referenceNumberParts[3]))
            {
                var newUniqueNum = $"{(int.Parse(referenceNumberParts[1]) + 1):000}";
                newReferenceNumber = "ASG-" + newUniqueNum + "-" + DateTime.Now.ToString("yy-MM");
            } else {
                newReferenceNumber = "ASG-" + "001" + "-" + DateTime.Now.ToString("yy-MM");
            }
            
            return newReferenceNumber;
        }
    }
}