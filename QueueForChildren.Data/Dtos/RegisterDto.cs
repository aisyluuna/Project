using QueueForChildren.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Data.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Не указано фамилия")]
        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; }        

        [RegularExpression(
            @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        public string EMail { get; set; } = null!;        

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
