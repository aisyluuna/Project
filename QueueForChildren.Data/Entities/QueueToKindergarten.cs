using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    /// <summary>
    /// Очередь в десткий сад
    /// </summary>
    public class QueueToKindergarten : Queue
    {
        public long KindergartenId { get; set; }
        public virtual Kindergarten Kindergarten { get; set; } = null!;
    }
}