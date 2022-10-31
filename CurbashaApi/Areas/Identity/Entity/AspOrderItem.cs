using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CurbashaApi.Areas.Identity.Entity
{
    public class AspOrderItem 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        public AspProduct Product { get; set; }

        [StringLength(1)]
        public string Size { get; set; }

        [Required]
        [Range(1, 100)]
        public string Quantity { get; set; }

        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        public int Price { get; set; }

        [Required]
        public string OrderId { get; set; }
    }
}
