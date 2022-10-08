namespace CurbashaApi.DB.Entities
{
    public class UserItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public string Count { get; set; }

        public string Price { get; set; }

        public int UserOrderId { get; set; }

        public UserOrder UserOrder { get; set; }

    }
}
