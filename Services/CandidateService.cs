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
            } else {
                newReferenceNumber = "ASG-" + "001" + "-" + DateTime.Now.ToString("yy-MM");
            }
            
            return newReferenceNumber;
        }

        public async Task<bool> Register(CourseRegistration courseRegistration)
        {
            courseRegistration.ReferenceNumber = await GenerateReferenceNumber();
            courseRegistration.UserId = _user.Id;
            return await _candidateRepository.Register(courseRegistration);
        }
    }
}