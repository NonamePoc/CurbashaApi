namespace CurbashaApi.DB.Entities
{
    public class UserOrder
    {
        public int Id { get; set; }

        public string UserAuthLogin { get; set; }

        public string Total { get; set; }

        public int UserAuthId { get; set; }

        public DateTime CreateOrder { get; set; }

        public DateTime TakeOrder { get; set; }

        public UserAuth UserAuth { get; set; }

        public List<UserItem> UserItems { get; set; }
    }
}
