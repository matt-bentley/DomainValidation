
namespace DomainValidation.Application.ReadModels
{
    public class CustomerReadModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool HasLeft { get; set; }
        public AddressReadModel Address { get; set; }
    }
}
