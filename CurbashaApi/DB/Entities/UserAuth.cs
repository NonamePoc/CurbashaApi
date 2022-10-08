namespace CurbashaApi.DB.Entities
{
    public class UserAuth
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<UserInfo> UserInfos { get; set; }

        public List<UserOrder> UserOrders { get; set; }
    }
}
