using System.Collections.Generic;
using QueueForChildren.Data.Entities.Abstract;

namespace QueueForChildren.Data.Entities
{
    public class School : EducationalInstitution
    {
        public virtual ICollection<SchoolLanguage> Languages { get; set; } = new List<SchoolLanguage>();

        public virtual ICollection<QueueToSchool> Queue { get; set; } = new List<QueueToSchool>();
    }
}