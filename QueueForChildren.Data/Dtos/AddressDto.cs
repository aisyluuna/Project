using System.ComponentModel.DataAnnotations;

namespace QueueForChildren.Data.Dtos
{
    public class AddressDto
    {
        /// <summary>
        /// Широта
        /// </summary>
        public string? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public string? Longitude { get; set; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        [Required(ErrorMessage = "Введите наименование населенного пункта")]
        public string Settlement { get; set; } = default!;

        /// <summary>
        /// Район внутри города
        /// </summary>
        public string? MicroRegion { get; set; } = default!;

        /// <summary>
        /// Улица
        /// </summary>
        [Required(ErrorMessage = "Введите название улицы")]
        public string Street { get; set; } = default!;

        /// <summary>
        /// Дом
        /// </summary>
        [Required(ErrorMessage = "Введите номер дома")]
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
    }
}
