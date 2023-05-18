using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Data.Dtos
{
    public class CreateChildDto
    {
        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Serial { get; set; } = default!;

        public string Number { get; set; } = default!;        

        public string IssuedBy { get; set; } = default!;

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssuedDate { get; set; }

        public string ActNumber { get; set; } = null!;      
    }
}
