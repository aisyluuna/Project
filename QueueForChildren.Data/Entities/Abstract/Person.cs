using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Data.Entities.Abstract
{
    public abstract class Person
    {
        public long Id { get; set; }

        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual string Serial { get; set; } = default!;

        public virtual string Number { get; set; } = default!;

        public Address Address { get; set; } = default!;

        public virtual string IssuedBy { get; set; } = default!;
    }
}
