using ContactService.Api.Core.Application.Interfaces.Repositories;
using ContactService.Api.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime;

namespace ContactService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IGenericRepository<Person> _genericRepository;
        public PersonController(IGenericRepository<Person> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Contact Service is Up and Running");
        }

        [HttpGet]
        [Route("persons")]
        [ProducesResponseType(typeof(IEnumerable<Person>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPersonsAsync()
        {
            var items = await _genericRepository.GetAll();
            return Ok(items);
        }


        [HttpPost]
        [Route("person")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreatePersonAsync([FromBody] Person person)
        {
            person = await _genericRepository.AddAsync(person);
            return Ok(person);
            //return CreatedAtRoute("ItemByIdAsync", new { id = person.Id }, null);
        }
        [HttpGet]
        [Route("person/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Person>> PersonByIdAsync(Guid id)
        {
            if (id.ToString() == null)
                return BadRequest();

            var item = await _genericRepository.GetByIdAsync(id);

            if (item != null)
                return item;

            return NotFound();
        }

        [HttpGet]
        [Route("personwithdetails")]
        [ProducesResponseType(typeof(List<Person>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PersonWithDetailsAsync()
        {
            var personsWithDetails = await _genericRepository.Get(i=>i.PersonInformations);

            if (personsWithDetails.Count > 0)
                return Ok(personsWithDetails);

            return NotFound();
        }
        
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeletePersonAsync(Guid id)
        {
            var dbPerson = await _genericRepository.GetById(id);
            if (dbPerson == null)
                return NotFound();

            await _genericRepository.Delete(id);


            return NoContent();
        }


    }
}
