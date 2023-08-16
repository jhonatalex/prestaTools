using System;
using System.Collections.Generic;

namespace prestaToolsApi.ModelsEntity;

public partial class DetalleVentum
{
    public int? IdDetalleVenta { get; set; }
    public int IdVenta { get; set; }
    public int IdTool { get; set; }
    public decimal Amount { get; set; }
    public string Date { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Descuento { get; set; }
    public int Total { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int RentalDays { get; set; }
    public virtual Tool IdToolNavigation { get; set; } = null!;
    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
