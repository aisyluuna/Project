using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    public class SchoolFillInfo : FillInfo
    {
        public long SchoolId { get; set; }

        /// <summary>
        /// Ссылка на школу
        /// </summary>
        public virtual School School { get; set; } = null!;
    }
}