using System.ComponentModel.DataAnnotations;

namespace QueueForChildren.Data.Enums
{
    /// <summary>
    /// Тип населенного пункта
    /// </summary>
    public enum SettlementType
    {
        /// <summary>
        /// Город
        /// </summary>
        [Display(Name = "Город")]
        City = 10,

        /// <summary>
        /// Деревня
        /// </summary>
        [Display(Name = "Дервеня")]
        Village = 20,

        /// <summary>
        /// Село
        /// </summary>
        [Display(Name = "Село")]
        Hamlet = 30
    }
}
