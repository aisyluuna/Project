using QueueForChildren.Data.Entities.Abstract;
using QueueForChildren.Data.Entities.FNS;
using QueueForChildren.Data.Enums;
using QueueForChildren.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Data.Entities
{
    public class Address : BaseEntity
    {     
        /// <summary>
        /// Широта
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double? Longitude { get; set; }

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
        /// Добавочная буква к номеру дома
        /// </summary>
        public string? AdditionalChar { get; set; }

        /// <summary>
        /// Подъезд
        /// </summary>
        public int? Entry { get; set; }

        /// <summary>
        /// Квартира
        /// </summary>
        public int? Apartment { get; set; }

        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Estate> Estates { get; set; }

        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<Kindergarten> Kindergartens { get; set; }

        public Address()
        {
            Children = new List<Child>();
            Parents = new List<Parent>();
            Estates = new List<Estate>();
            Schools = new List<School>();
            Kindergartens = new List<Kindergarten>();
        }
    }
}
