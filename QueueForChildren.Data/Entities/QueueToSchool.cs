using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    /// <summary>
    /// Очередь в школу
    /// </summary>
    public class QueueToSchool : Queue
    {
        public long SchoolId { get; set; }
        public virtual School School { get; set; } = null!;
    }
}