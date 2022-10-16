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

        public string? Name { get; set; }

        public string? Size { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public int AspOrderId { get; set; }

        public AspUserOrder? AspUserOrder { get; set; }
    }
}
