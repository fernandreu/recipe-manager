using System.Collections.Generic;

namespace RecipeManager.ApplicationCore.Models
{
    public class RegisterResult
    {
        public bool Successful { get; set; }

        public IEnumerable<string>? Errors { get; set; }
    }
}
