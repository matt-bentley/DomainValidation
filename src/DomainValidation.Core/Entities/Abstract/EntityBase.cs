
namespace DomainValidation.Core.Entities.Abstract
{
    public abstract class EntityBase : IEqualityComparer<EntityBase>, IEquatable<EntityBase>
    {
        protected EntityBase(Guid id)
        {
            Id = id;
            CreatedDate = DateTime.UtcNow;
        }

        protected EntityBase() : this(Guid.NewGuid())
        {

        }

        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; private set; }

        public override bool Equals(object obj) => this.Equals(obj as EntityBase);

        public bool Equals(EntityBase other)
        {
            if (other is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return Id.Equals(other.Id);
        }

        public bool Equals(EntityBase x, EntityBase y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode() => (this.GetType().ToString() + Id.ToString()).GetHashCode();

        public int GetHashCode(EntityBase obj)
        {
            return obj.GetHashCode();
        }
    }
}
