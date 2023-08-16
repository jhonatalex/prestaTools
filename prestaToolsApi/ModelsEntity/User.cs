using System;
using System.Collections.Generic;

namespace prestaToolsApi.ModelsEntity;

public partial class User
{
    public int? Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public string? Password { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Address { get; set; }

    public string? DIdentidad { get; set; }

    public string? Date { get; set; }

    public bool? Verify { get; set; }

    public string? commune { get; set; }

    public string? region { get; set; }

    public int? Rate { get; set; }

    public string? TypeUser { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
