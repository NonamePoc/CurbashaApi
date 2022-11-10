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

        public string? NameProduct { get; set; }

        public string? Description { get; set; }

        public int SelectionId { get; set; }

        public AspSelections AspSelections { get; set; }

        public double Price { get; set; }
        
        public List<AspOrderItem> OrderItems { get; set; }
        
    }
}
