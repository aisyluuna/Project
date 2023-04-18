using QueueForChildren.Data.Enums;
using QueueForChildren.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Data.Entities
{
    public sealed class Address : IEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип региона
        /// </summary>
        public RegionType RegionType { get; set; }

        /// <summary>
        /// Наименование региона
        /// </summary>
        public string RegionName { get; set; } = default!;

        /// <summary>
        /// Тип населенного пункта
        /// </summary>
        public SettlementType SettlementType { get; set; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        public string Settlement { get; set; } = default!;

        /// <summary>
        /// Район внутри города
        /// </summary>
        public string? MicroRegion { get; set; } = default!;

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; } = default!;

        /// <summary>
        /// Дом
        /// </summary>
        public int HouseNumber { get; set; }

        /// <summary>
        /// Подъезд
        /// </summary>
        public int? Entry { get; set; }

        /// <summary>
        /// Квартира
        /// </summary>
        public int? Apartment { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DeletedDate { get; set; }

        public bool Deleted { get; set; }
    }
}
