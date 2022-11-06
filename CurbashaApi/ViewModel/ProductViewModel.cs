using System;
using CurbashaApi.Areas.Identity.Entity;

namespace CurbashaApi.ViewModel
{
    public class ProductViewModel
    {
        public AspProduct? Product { get; set; }

        public string? SectionName { get; set; }

        public string[]? ImagePathes { get; set; }

        public string Size { get; set; }
    }
}