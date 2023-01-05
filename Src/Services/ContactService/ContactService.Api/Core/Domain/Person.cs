using ContactService.Api.SeedWork;

namespace ContactService.Api.Core.Domain
{
    public class Person : BaseEntity
    {
        public string Name { get;private set; }
        public string SurName { get; private set; }
        public string Company { get; private set; }

        public readonly List<PersonInformation> _personInformations;

        public IReadOnlyCollection<PersonInformation> PersonInformations => _personInformations;
       

        public Person(string name, string surName, string company

            )
        {
            Id = Guid.NewGuid();
            _personInformations = new List<PersonInformation>();
            CreatedDate = DateTime.Now;
            Name = name;
            SurName = surName;
            Company = company;

        }
    }
}
