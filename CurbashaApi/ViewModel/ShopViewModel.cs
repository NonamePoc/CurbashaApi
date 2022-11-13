using System;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurbashaApi.ViewModel
{
    public class ShopViewModel
    {
        public List<AspProduct>? Products { get; set; }
        public List<AspSelections>? Selections { get; set; }
    }
}