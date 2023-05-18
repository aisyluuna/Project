using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    public class KindergartenFillInfo : FillInfo
    {
        public long KindergartenId { get; set; }
        /// <summary>
        /// Ссылка на десткий сад
        /// </summary>
        public virtual Kindergarten Kindergarten { get; set; } = null!;
    }
}