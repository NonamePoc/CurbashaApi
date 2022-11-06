using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CurbashaApi.Areas.Identity.Entity
{
    public class AspUserOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("CreatedAt")]
        public DateTime CreateAt { get; set; }


        [DefaultValue(true)]
        public bool IsActive { get; set; }

        //[NotMapped]
        public ICollection<AspOrderItem>? OrderItems { get; set; }

        [Required(ErrorMessage = "Choose user to create order")]
        public IdentityUser User { get; set; }
    }
}