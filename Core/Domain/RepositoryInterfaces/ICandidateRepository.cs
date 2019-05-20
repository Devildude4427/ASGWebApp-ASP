using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.ViewModels;

namespace Core.Domain.RepositoryInterfaces
{
    public interface ICandidateRepository
    {
        //CRUD
        
        Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest);
        Task<IEnumerable<Candidate>> GetAll();
        Task<Candidate> FindByUserId(long id);
        Task<string> PreviousCandidateReferenceNumber();
        Task<bool> Register(CourseRegistration courseRegistration);
        Task<bool> UpdateDetails(UpdateContactDetails updateContactDetails);
    }
}