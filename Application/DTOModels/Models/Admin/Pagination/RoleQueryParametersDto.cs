using System.ComponentModel.DataAnnotations;

namespace Application.DtoModels.Models.Pagination
{
    public class RoleQueryParametersDto
    {
        [Range(0, 1001, ErrorMessage = "Page must be between 0 and 100.")]
        public int Page { get; set; } = 1;

        [Range(0, 1001, ErrorMessage = "PageSize must be between 0 and 100.")]
        public int PageSize { get; set; } = 10;

        public string SortField { get; set; } = "Id";

        public string SortOrder { get; set; } = "asc";

        public string? IdRole { get; set; }

        public string? NameRole { get; set; }
    }
}
