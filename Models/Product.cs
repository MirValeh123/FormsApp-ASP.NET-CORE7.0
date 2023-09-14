using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Models
{
    public class Product
    {
        [Required]

        [Display(Name = "Urun İd")]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Gerekli alan")]
        [Display(Name = "Urun Adı")]
        [StringLength(16)]
        public string Name { get; set; } = null!;
        [Required]

        [Display(Name = "Fiyat")]
        [Range(0,100000)]
        public decimal Price { get; set; }

        [Display(Name = "Resim")]


        public string? Image { get; set; } = string.Empty;

        [Required]

        public bool IsActive { get; set; }

        [Required]

        public int? CategoryId { get; set; }
    }
}