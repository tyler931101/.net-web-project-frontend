namespace frontend.Models
{
    public class UserModel
    {
        public string Id { get; set; } = string.Empty;  // should be string to match backend IdentityUser
        public string Name { get; set; } = string.Empty;
    }
}