using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class CustomerToUpdate
    {
        [Required(ErrorMessage = "A first name must be provided.")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A last name must be provided.")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "An email must be provided.")]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "A phone number must be 10 digits.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "A street name is required for shipping purposes.")]
        [MaxLength(50)]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "A zip code is required for shipping purposes.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "A city is required for shipping purposes.")]
        [MaxLength(50)]
        public string City { get; set; }
    }
}
