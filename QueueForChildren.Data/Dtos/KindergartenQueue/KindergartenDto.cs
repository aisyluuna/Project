using System.Collections.Generic;
using QueueForChildren.Data.Entities;

namespace QueueForChildren.Data.Dtos.KindergartenQueue
{
    /// <summary>
    /// Дто школы для вывода в список
    /// </summary>
    public class KindergartenDto
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

        public ICollection<KindergartenLanguage> Languages { get; set; } = null!;
        
        public double Rating { get; set; }

        public string Phone { get; set; } = null!;
    }
}