using QueueForChildren.Data.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QueueForChildren.Data.Entities
{
    public class Parent : Person
    {        
        public string? Phone { get; set; }
        
        public string EMail { get; set; } = null!;

        public string SubdivisionCode { get; set; } = null!;

        [MaxLength(12)]
        public string INN { get; set; } = null!;

        public virtual ICollection<Child> Children { get; set; }

        public Parent()
        {
            Children = new List<Child>();
        }
    }
}
