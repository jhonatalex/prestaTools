using System;
using System.Collections.Generic;

namespace prestaToolsApi.ModelsEntity;

public partial class Lender
{
    public int? Id { get; set; }

    public string? DIdentidad { get; set; } = null!;

    public string? Name { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public string? Password { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? Telephone { get; set; } = null!;

    public string? Address { get; set; } = null!;

    public string? NumberBank { get; set; }

    public string? BalanceWallet { get; set; }

    public string? DateUp { get; set; }

    public string? commune { get; set; }

    public string? region { get; set; }
    
    public int? Rate { get; set; }

    public virtual ICollection<Tool> Tools { get; set; } = new List<Tool>();
}
