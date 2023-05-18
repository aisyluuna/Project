namespace QueueForChildren.Data.Entities.Abstract
{
    /// <summary>
    /// Язык обучения
    /// </summary>
    public abstract class Language : BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}