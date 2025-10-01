namespace frontend.Models
{
    public class UserModel
    {
        public string Id { get; set; } = string.Empty;  // should be string to match backend IdentityUser
        public string UserName { get; set; } = string.Empty;
    }
}