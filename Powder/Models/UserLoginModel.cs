using System.ComponentModel.DataAnnotations;

namespace Powder.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Username cannot be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Key cannot be empty")]
        public string Key { get; set; }
        public bool RememberMe { get; set; }
    }
}
