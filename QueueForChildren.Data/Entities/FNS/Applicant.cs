using QueueForChildren.Data.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueueForChildren.Data.Entities.FNS
{
    public class Applicant : BaseEntity
    {
        public long ParentId { get; set; }
        public virtual Parent Parent { get; set; } = null!;

        public int FamilyMembersCount { get; set; }

        public decimal FamilyIncomeForYear { get; set; }
        
        /// <summary>
        /// Льгота
        /// </summary>
        public bool IsBenefit { get; set; }

        public virtual ICollection<Estate> Estates { get; set; } = new List<Estate>();

        /// <summary>
        /// Средний доход
        /// </summary>
        [NotMapped]
        public decimal AverageIncome => FamilyIncomeForYear / FamilyMembersCount / 12;
    }
}
