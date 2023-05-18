namespace QueueForChildren.Data.Entities.Abstract
{
    public abstract class EducationalInstitution : BaseEntity
    {      
        public string Name { get; set; } = null!;

        public long AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        /// <summary>
        /// Рейтинг
        /// </summary>
        public double Rating { get; set; }
    }
}