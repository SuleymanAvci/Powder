using System.ComponentModel.DataAnnotations;

namespace Powder.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage ="Ad alanı boş geçilemz.")]
        public string Name { get; set; }
    }
}
