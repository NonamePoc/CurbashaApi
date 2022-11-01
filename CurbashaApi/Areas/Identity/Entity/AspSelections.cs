using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CurbashaApi.Areas.Identity.Entity;

public class AspSelections
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Selection")]
    [Required(ErrorMessage = "Selection name is required")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "The category name can be maximum 20 characters long, minimum - 3")]
    public string? SelectionName { get; set; }

    public virtual List<AspProduct> Products { get; set; }
    
}