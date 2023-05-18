using System;

namespace QueueForChildren.Data.Dtos
{
    public class ChildDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
