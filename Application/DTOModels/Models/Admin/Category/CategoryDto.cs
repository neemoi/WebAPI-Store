using System.ComponentModel.DataAnnotations;

namespace Application.DTOModels.Models.Admin.Category
{
    public interface CategoryDto
    {

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
