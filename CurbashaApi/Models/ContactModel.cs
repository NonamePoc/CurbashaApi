using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CurbashaApi.Models
{
    public class ContactModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
