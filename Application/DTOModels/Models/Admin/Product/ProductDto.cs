using System.ComponentModel.DataAnnotations;

namespace Application.DTOModels.Models.Admin.Product
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        public string? Color { get; set; }

        [Required]
        public string? Memory { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
