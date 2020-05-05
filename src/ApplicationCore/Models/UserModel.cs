namespace RecipeManager.ApplicationCore.Models
{
    public class UserModel
    {
        public string UserName { get; set; } = string.Empty;

        public bool IsAuthenticated { get; set; }
    }
}
