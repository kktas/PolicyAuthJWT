
namespace PolicyAuthJWT.Config
{
    public static class Enums
    {
        public static List<UserModel> Users { get; set; } = new List<UserModel>()
        {
            new UserModel(){Id= 1, Username="User1", Password= "User1", Roles = new string[]{"1"} },
            new UserModel(){Id= 2, Username="User2", Password= "User2", Roles = new string[]{"2"} },
            new UserModel(){Id= 3, Username="User3", Password= "User3", Roles = new string[]{"3"} },
            new UserModel(){Id= 4, Username="User4", Password= "User4", Roles = new string[]{"4"} },
            new UserModel(){Id= 5, Username="User5", Password= "User5", Roles = new string[]{"5"} },
        };

    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
