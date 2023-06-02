using System;
using System.Collections.Generic;

namespace prestaToolsApi.models_DB;

public partial class DetalleVentum
{
    public int IdDetalleVenta { get; set; }

    public int IdVenta { get; set; }

    public int IdTool { get; set; }

    public decimal AmountVenta { get; set; }

    public DateTime? DateUp { get; set; }

    public decimal Price { get; set; }

    public decimal Descuento { get; set; }

    public virtual ToolEntity IdToolNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
