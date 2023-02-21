using System.ComponentModel.DataAnnotations;

namespace Powder.Models
{
    public class CategoryUpdateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı boş geçilemz.")]
        public string Name { get; set; }
    }
}
