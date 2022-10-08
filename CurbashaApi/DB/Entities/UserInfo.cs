namespace CurbashaApi.DB.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int UserAuthId { get; set; }

        public UserAuth UserAuth { get; set; }
    }
}
