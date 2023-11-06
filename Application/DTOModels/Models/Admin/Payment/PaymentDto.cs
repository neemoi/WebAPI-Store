using System.ComponentModel.DataAnnotations;

namespace Application.DTOModels.Models.Admin.Payment
{
    public class PaymentDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }
    }
}
