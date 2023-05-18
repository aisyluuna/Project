using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    public class SchoolLanguage : Language
    {
        public long SchoolId { get; set; }
        public virtual School School { get; set; } = null!;
    }
}