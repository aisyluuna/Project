using System;
using System.ComponentModel.DataAnnotations;

namespace QueueForChildren.Data.Dtos
{
    /// <summary>
    /// Дто для создания родителя
    /// </summary>
    public class CreateParentDto
    {
        public string LastName { get; set; } = null!;

        public string Name { get; set; } = null!;
        
        public string? MiddleName { get; set; }
        
        [Required(ErrorMessage = "Введите день рождения")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Введите серию паспорта")]
        public string Serial { get; set; } = default!;

        [Required(ErrorMessage = "Введите номер паспорта")]
        public string Number { get; set; } = default!;

        [Required]
        public AddressDto Address { get; set; } = default!;

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        [Required(ErrorMessage = "Введите кем выдано")]
        public string IssuedBy { get; set; } = default!;

        //<summary>
        // Дата выдачи
        //</summary>
        [Required(ErrorMessage = "Введите дату выдачи")]
        public DateTime IssuedDate { get; set; }
        
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Введите кад подразделения")]
        public string SubdivisionCode { get; set; } = null!;

        [MaxLength(12, ErrorMessage = "Длина превышает 12")]
        [Required(ErrorMessage = "Введите ИНН")]
        public string INN { get; set; } = null!;
    }
}
