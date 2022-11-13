using CurbashaApi.Areas.Identity.Entity;

namespace CurbashaApi.ViewModel
{
    public class BasketViewModel
    {
        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
