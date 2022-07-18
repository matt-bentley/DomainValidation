
namespace DomainValidation.Core.Entities.Abstract
{
    public abstract class AggregateRoot : EntityBase
    {
        protected AggregateRoot(Guid id) : base(id)
        {

        }

        protected AggregateRoot()
        {

        }
    }
}
