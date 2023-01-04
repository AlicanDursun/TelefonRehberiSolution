using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInformationController : ControllerBase
    {
        private readonly IGenericRepository<PersonInformation> _genericRepository;

        public PersonInformationController(IGenericRepository<PersonInformation> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Contact Service is Up and Running");
        }
        [HttpPost]
        [Route("personInformation")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
       
        public async Task<ActionResult> CreatePersonAsync([FromBody] PersonInformation personInformation)
        {
            personInformation = await _genericRepository.AddAsync(personInformation);
            return Ok(personInformation);
           
        }
    }
}
