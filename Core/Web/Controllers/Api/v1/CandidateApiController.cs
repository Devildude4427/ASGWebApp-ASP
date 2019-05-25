using System.Threading.Tasks;
using Core.Domain.ViewModels;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers.Api.v1
{
    [Route("/api/v1/candidate")]
    public class CandidateApiController : RootApiController
    {
        private readonly ICandidateService _candidateService;

        public CandidateApiController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        
        [HttpPost("updatePersonalDetails")]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdateContactDetails updateContactDetails)
        {
            var result = await _candidateService.UpdateDetails(updateContactDetails);
            return result ? Json(new {Success = false, Response = "Unknown Server Error"})
                : Json(new {Success = true});
        }
    }
}