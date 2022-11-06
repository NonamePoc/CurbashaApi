using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;

namespace CurbashaApi.Areas.Identity.Entity
{
    public class AspOrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        //[NotMapped]
        public AspProduct Product { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [StringLength(5)]
        public string Size { get; set; }

        public string ProductName { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "The maximum quantity is 100, minimum - 1")]
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}