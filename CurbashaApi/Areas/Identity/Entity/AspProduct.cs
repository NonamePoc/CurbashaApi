using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CurbashaApi.Areas.Identity.Entity
{
    public class AspProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string? NameProduct { get; set; }

        public string? Description { get; set; }

        [Required]
        public int SelectionId { get; set; }

        public AspSelections AspSelections { get; set; }

        [DataType(DataType.Currency)]
       // [Column(TypeName = "decimal(18, 2)")]
        public int Price { get; set; }
        
        public List<AspOrderItem> OrderItems { get; set; }
        
    }
}
