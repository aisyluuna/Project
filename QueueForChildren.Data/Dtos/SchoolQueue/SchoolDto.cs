using System.Collections.Generic;
using QueueForChildren.Data.Entities;

namespace QueueForChildren.Data.Dtos.SchoolQueue
{
    /// <summary>
    /// Дто школы для вывода в список
    /// </summary>
    public sealed class SchoolDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        
        public string Name { get; set; } = null!;

        /// <summary>
        /// Район внутри города
        /// </summary>
        public string MicroRegion { get; set; } = null!;

        public string Address { get; set; } = null!;
        
        /// <summary>
        /// Кол-во свободных мест
        /// </summary>
        public int FreePlaceCount { get; set; }

        public ICollection<SchoolLanguage> Languages { get; set; } = null!;
        
        public double Rating { get; set; }

        public string Phone { get; set; } = null!;
    }
}