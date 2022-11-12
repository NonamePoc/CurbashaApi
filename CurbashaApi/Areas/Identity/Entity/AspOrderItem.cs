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

        public int ProductId { get; set; }
        
        public AspProduct Product { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public int OrderId { get; set; }

        public string ProductName { get; set; }

    }
}
