using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace prestaToolsApi.models_DB;

public partial class UserEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Address { get; set; }

    public string? DIdentidad { get; set; }

    public DateTime? DateUp { get; set; }

    public bool State { get; set; }

    [JsonIgnore]
    public virtual ICollection<ToolEntity> Tools { get; set; } = new List<ToolEntity>();

    [JsonIgnore]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
