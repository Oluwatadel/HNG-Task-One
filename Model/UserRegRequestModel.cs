using System.ComponentModel.DataAnnotations;

namespace User_Registartion.Model
{
    public class UserRegRequestModel
    {

        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string? Email { get; set; } = default!;
        public string? Password { get; set; }

        [Required]
        public string? Phone { get; set; }
    }
}
