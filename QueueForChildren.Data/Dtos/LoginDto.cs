using System.ComponentModel.DataAnnotations;

namespace QueueForChildren.Data.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;


        public string? ReturnUrl { get; set; }
    }
}
