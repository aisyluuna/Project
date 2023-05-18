using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities.FNS
{
    /// <summary>
    /// Имущество
    /// </summary>
    public class Estate : BaseEntity
    {
        public string Name { get; set; } = null!;

        public long? AddressId { get; set; }
        public virtual Address? Address { get; set; } = null!;

        public double? Square { get; set; }

        public long ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; } = null!;
    }
}
