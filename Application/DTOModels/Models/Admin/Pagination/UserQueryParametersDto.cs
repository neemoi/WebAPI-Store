﻿using System.ComponentModel.DataAnnotations;

namespace Application.DtoModels.Models.Pagination
{
    public class UserQueryParametersDto
    {
        [Range(0, 1001, ErrorMessage = "Page must be between 0 and 100.")]
        public int Page { get; set; } = 1;

        [Range(0, 1001, ErrorMessage = "PageSize must be between 0 and 100.")]
        public int PageSize { get; set; } = 1001;

        public string SortField { get; set; } = "Id";

        public string SortOrder { get; set; } = "asc";

        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

    }
}
