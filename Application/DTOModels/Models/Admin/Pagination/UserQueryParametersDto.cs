using System.ComponentModel.DataAnnotations;

namespace Application.DtoModels.Models.Pagination
{
    public class UserQueryParametersDto
    {
        [Range(0, 100, ErrorMessage = "Page must be between 0 and 100.")]
        public int Page { get; set; } = 1;

        [Range(0, 100, ErrorMessage = "PageSize must be between 0 and 100.")]
        public int PageSize { get; set; } = 10;

        public string SortField { get; set; } = "Id";

        public string SortOrder { get; set; } = "asc";

        public string? SearchUserId { get; set; }

        public string? SearchUserName { get; set; }

        public string? SearchEmail { get; set; }

        public string? SearchPhoneNumber { get; set; }

    }
}
