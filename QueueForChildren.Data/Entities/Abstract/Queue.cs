namespace QueueForChildren.Data.Entities.Abstract
{
    public abstract class Queue : BaseEntity
    {
        public long ChildId { get; set; }
        public virtual Child Child { get; set; } = null!;
    }
}