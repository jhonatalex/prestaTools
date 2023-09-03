using Newtonsoft.Json;
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
    public decimal Total { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }

    //Datos de ResponseCommit
    public string BuyOrder { set; get; }
    public string SessionId { set; get; }
    public string PaymentTypeCode { set; get; }
    public decimal InstallmentsAmount { set; get; }
    public int InstallmentsNumber { set; get; }
    public string Token { set; get; }
    public int RentalDays { get; set; }
    [JsonIgnore]
    public virtual Tool? IdToolNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Ventum? IdVentaNavigation { get; set; } = null!;

}
