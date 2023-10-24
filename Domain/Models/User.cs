using Microsoft.AspNetCore.Identity;

namespace WebAPIKurs;

public partial class User : IdentityUser
{
    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
