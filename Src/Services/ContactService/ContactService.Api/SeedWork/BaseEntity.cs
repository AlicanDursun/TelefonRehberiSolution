namespace ContactService.Api.SeedWork
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }= DateTime.Now;
    }
}
