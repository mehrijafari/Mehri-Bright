using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class CustomerForCreation
    {
        [Required(ErrorMessage = "A first name must be provided.")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
