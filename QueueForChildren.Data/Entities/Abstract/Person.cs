using System;

namespace QueueForChildren.Data.Entities.Abstract
{
    public abstract class Person : BaseEntity
    {     
        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Serial { get; set; } = default!;

        public string Number { get; set; } = default!;

        public long AddressId { get; set; }

        public virtual Address Address { get; set; } = default!;

        public string IssuedBy { get; set; } = default!;

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssuedDate { get; set; }

    }
}