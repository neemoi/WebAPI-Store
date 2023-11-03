namespace Application.DTOModels.Response.Admin
{
    public class PaymentResponseDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Type { get; set; } = null!;

        public decimal Amount { get; set; }
    }
}
