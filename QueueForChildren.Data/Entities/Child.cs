using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    public class Child : Person
    {
        public string ActNumber { get; set; } = null!;        

        public long ParentId { get; set; }
        /// <summary>
        /// Заявитель/родитель
        /// </summary>
        public virtual Parent Parent { get; set; } = null!;
    }
}
