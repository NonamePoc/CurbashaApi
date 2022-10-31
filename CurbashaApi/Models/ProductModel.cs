using System;
using CurbashaApi.Areas.Identity.Entity;

namespace CurbashaApi.Models
{
    public class ProductModel
    {
        public AspProduct? Product { get; set; }

        public string? SectionName { get; set; }

        public string[]? ImagePathes { get; set; }
    }
}