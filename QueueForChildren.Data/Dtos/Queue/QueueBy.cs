namespace QueueForChildren.Data.Dtos.Queue
{
    /// <summary>
    /// Список очереди образовательное учреждение
    /// </summary>
    public class QueueBy
    {
        public string ChildName { get; set; }
        
        public long ChildId { get; set; }
        
        public string IsBenefit { get; set; }
        
        public long ParentId { get; set; }

        /// <summary>
        /// Дата подачи заявления
        /// </summary>
        public string Date { get; set; }
    }
}