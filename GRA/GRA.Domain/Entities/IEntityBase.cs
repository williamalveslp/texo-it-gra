namespace GRA.Domain.Entities
{
    public interface IEntityBase
    {
        public int Id { get; }

        public DateTime CreatedDate { get; }

        public DateTime? UpdatedDate { get; }
    }

    public abstract class EntityBase : IEntityBase
    {
        private int _id;
        public virtual int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        private DateTime _createdDate;
        public virtual DateTime CreatedDate
        {
            get { return _createdDate; }
            protected set { _createdDate = value; }
        }

        private DateTime? _updatedDate;
        public virtual DateTime? UpdatedDate
        {
            get { return _updatedDate; }
            private set { _updatedDate = value; }
        }

        protected EntityBase()
        {
            CreatedDate = DateTime.Now;
        }

        public void RefreshUpdatedDate()
        {
            UpdatedDate = DateTime.Now;
        }

        public EntityBase SetCreatedDate()
        {
            this.CreatedDate = DateTime.Now;
            return this;
        }
    }
}
