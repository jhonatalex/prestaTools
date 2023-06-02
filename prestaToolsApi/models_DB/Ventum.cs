using System;
using System.Collections.Generic;

namespace prestaToolsApi.models_DB;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int IdUser { get; set; }

    public string TypeComprobante { get; set; } = null!;

    public string NumberComprobante { get; set; } = null!;

    public DateTime? DateUp { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public bool State { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual UserEntity IdUserNavigation { get; set; } = null!;
}
