using System.ComponentModel.DataAnnotations;

namespace Application.DTOModels.Models.Admin.Pagination
{
    public class ProductQueryParametersDto
    {
        [Range(0, 1001, ErrorMessage = "Page must be between 0 and 100.")]
        public int Page { get; set; } = 1;

        [Range(0, 1001, ErrorMessage = "PageSize must be between 0 and 100.")]
        public int PageSize { get; set; } = 10;

        public string SortField { get; set; } = "Id";

        public string SortOrder { get; set; } = "asc";

        public string? SearchProductId { get; set; }

        public string? SearchProductName { get; set; }

        public string? SearchProductDescription { get; set; }

        public string? SearchProductPrice { get; set; }
    }
}
