using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CurbashaApi.Models
{
    public class ContactModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Use at least 2 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(70, ErrorMessage = "Use not more than 70 charecters")]
        [MinLength(2, ErrorMessage = "Use at least 2 characters")]
        public string Subject { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please, enter e-mail")]
        public string Email { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Use at least 2 characters")]
        public string Body { get; set; }
        //public IFormFile Attachment { get; set; }
    }
}
