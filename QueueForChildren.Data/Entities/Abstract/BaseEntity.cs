using QueueForChildren.Data.Interfaces;
using System;

namespace QueueForChildren.Data.Entities.Abstract
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Дата удаление
        /// </summary>
        public DateTime? DeletedDate { get; set; }
        /// <summary>
        /// Признак удаленности
        /// </summary>
        public bool Deleted { get; set; }
    }
}
