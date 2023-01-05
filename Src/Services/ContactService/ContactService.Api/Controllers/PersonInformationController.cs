using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Core.Domain;
using ContactService.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace ContactService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInformationController : ControllerBase
    {

        private readonly IGenericRepository<PersonInformation> _personInformationRepository;
        private readonly IGenericRepository<Person> _personRepository;
        public PersonInformationController(IGenericRepository<PersonInformation> personInformationRepository,
            IGenericRepository<Person> personRepository)
        {
            _personInformationRepository = personInformationRepository;
            _personRepository = personRepository;

        }


        [HttpPost]
        [Route("personInformation")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PersonInformation), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreatePersonInformationAsync([FromBody] PersonInformation information)
        {
            if (await _personRepository.GetById(information.PersonId) == null)
                return BadRequest();

            information = await _personInformationRepository.AddAsync(information);

            return Ok(information);

        }
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeletePersonInformationAsync(Guid id)
        {
            var dbPersonInfo = await _personInformationRepository.GetById(id);
            if (dbPersonInfo == null)
                return NotFound();

            await _personInformationRepository.Delete(id);
            return NoContent();
        }
    }
}
