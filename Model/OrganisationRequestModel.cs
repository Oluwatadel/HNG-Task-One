using System.ComponentModel.DataAnnotations;

namespace User_Registartion.Model
{
    public class OrganisationRequestModel
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
