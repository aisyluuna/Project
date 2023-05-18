namespace QueueForChildren.Data.Entities.Abstract
{
    public abstract class FillInfo : BaseEntity
    {      
        /// <summary>
        /// Заполнено
        /// </summary>
        public int FilledCount { get; set; }
        
        /// <summary>
        /// В очереди
        /// </summary>
        public int InQueueCount { get; set; }
        
        /// <summary>
        /// Свободные места
        /// </summary>
        public int FreePlaceCount { get; set; }
    }
}