using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Data.Enums
{
    /// <summary>
    /// Тип региона
    /// </summary>
    public enum RegionType
    {
        /// <summary>
        /// Республика
        /// </summary>
        [Display(Name = "Республика")]
        Republic = 10,

        /// <summary>
        /// Область
        /// </summary>
        [Display(Name = "Область")]
        Area = 20,

        /// <summary>
        /// Край
        /// </summary>
        [Display(Name = "Край")]
        Edge = 30
    }
}
