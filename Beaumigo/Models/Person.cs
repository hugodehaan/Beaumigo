using System.ComponentModel.DataAnnotations;

namespace Beaumigo.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Gelieve uw voornaam in te vullen")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Gelieve uw achternaam in te vullen")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Gelieve uw email in te vullen")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Gelieve een bericht in te vullen")]
        public string Message { get; set; }


    }
}

