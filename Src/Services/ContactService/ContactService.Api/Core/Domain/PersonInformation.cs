using ContactService.Api.SeedWork;

namespace ContactService.Api.Core.Domain
{
    public class PersonInformation : BaseEntity
    {
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Location { get; private set; }
        public string Content { get; private set; }

        public Guid PersonId { get; private set; }
       
        public PersonInformation(string phoneNumber, string email, string location, string content, Guid personId)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            Location = location;
            Content = content;
            PersonId = personId;
        }
    }
}
