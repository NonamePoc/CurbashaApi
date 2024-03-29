﻿using System.ComponentModel;
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

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "The maximum length must be upto 60 characters, minimum - 3")]
        public string? NameProduct { get; set; }

        [Column(TypeName = "ntext")]
        [StringLength(500, ErrorMessage = "The maximum length must be upto 500 characters")]
        public string? Description { get; set; }

        public int SelectionId { get; set; }

        public /*virtual*/ AspSelections AspSelections { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        //[NotMapped]
        public List<AspOrderItem> OrderItems { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsActive { get; set; }

    }
}