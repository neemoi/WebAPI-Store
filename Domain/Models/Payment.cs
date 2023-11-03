namespace WebAPIKurs;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Type { get; set; } = null!;

    public decimal Amount { get; set; }

    public virtual Order Order { get; set; } = null!;
}
