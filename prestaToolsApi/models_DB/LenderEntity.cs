using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace prestaToolsApi.models_DB;

public partial class LenderEntity
{
    public int Id { get; set; }

    public string DIdentidad { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string NumberBank { get; set; } = null!;

    public decimal BalanceWallet { get; set; }

    public DateTime? DateUp { get; set; }

    public bool State { get; set; }

    [JsonIgnore]
    public virtual ICollection<ToolEntity> Tools { get; set; } = new List<ToolEntity>();
}
