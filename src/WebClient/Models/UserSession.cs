using RecipeManager.ApplicationCore.Resources;

namespace WebClient.Models
{
    /// <summary>
    /// Holds any persistent data that needs to be kept between sections of the website
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// The user currently logged in.
        /// </summary>
        public UserResource User { get; set; }
    }
}
