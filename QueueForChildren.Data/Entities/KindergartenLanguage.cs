using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    public class KindergartenLanguage : Language
    {
        public long KindergartenId { get; set; }
        public virtual Kindergarten Kindergarten { get; set; } = null!;
    }
}