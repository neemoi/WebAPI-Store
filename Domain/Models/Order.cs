namespace WebAPIKurs;

public partial class Order
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual CustomUser User { get; set; } = null!;
}
