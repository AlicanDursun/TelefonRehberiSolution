using ContactService.Api.SeedWork;

namespace ContactService.Api.Core.Domain
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }

        public readonly List<PersonInformation> _personInformations;

        public IReadOnlyCollection<PersonInformation> PersonInformations => _personInformations;
        public Person()
        {
            Id = Guid.NewGuid();
            _personInformations = new List<PersonInformation>();


        }

        public Person(string name, string surName, string company

            )
        {
            _personInformations = new List<PersonInformation>();
            CreatedDate = DateTime.Now;
            Name = name;
            SurName = surName;
            Company = company;

        }
    }
}
