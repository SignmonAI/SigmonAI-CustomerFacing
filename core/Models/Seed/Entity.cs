namespace core.Models.Seed
{
    public abstract class Entity
    {
        private Guid _id;

        public virtual Guid Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        public Entity(Guid? id = null)
        {
            _id = id ?? Guid.NewGuid();
        }
    }
}