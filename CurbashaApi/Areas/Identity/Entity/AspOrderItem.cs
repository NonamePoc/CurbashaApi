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

        public int ProductId { get; set; }
        
        public AspProduct Product { get; set; }

        public string Size { get; set; }

        public string Quantity { get; set; }

        public int Price { get; set; }

        public string OrderId { get; set; }
    }
}
