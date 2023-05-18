using System.Collections.Generic;
using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    public class Kindergarten : EducationalInstitution
    {
        public virtual ICollection<KindergartenLanguage> Languages { get; set; } = new List<KindergartenLanguage>();

        public virtual ICollection<QueueToKindergarten> Queue { get; set; } = new List<QueueToKindergarten>();
    }
}