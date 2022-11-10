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

        public DateTime CreateAt { get; set; }

        public bool IsActive { get; set; }

        public ICollection<AspOrderItem>? OrderItems { get; set; }

        public double Total { get; set; }

        public IdentityUser User { get; set; }
    }
}