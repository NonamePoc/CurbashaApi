using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CurbashaApi.Areas.Identity.Entity;

public class AspSelections
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string? SelectionName { get; set; }

    public List<AspProduct> Products { get; set; }
    
}