using System.Threading.Tasks;
using Domain.Entities;
using Domain.ViewModels;

namespace Domain.RepositoryInterfaces
{
    public interface ICandidateRepository
    {
        //CRUD
        
        Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest);
        Task<Candidate> FindByUserId(long id);
        Task<string> PreviousCandidateReferenceNumber();
        Task<bool> Register(CourseRegistration courseRegistration);
        Task<bool> UpdateDetails(UpdateContactDetails updateContactDetails);
    }
}