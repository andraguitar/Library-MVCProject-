namespace BLL.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public MyRoles Role { get; set; }
    }

    public enum MyRoles {
        Admin = 1,
        User = 2
    }
}