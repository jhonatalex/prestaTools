using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace prestaToolsApi.ModelsEntity;

public partial class Ventum
{
    public int? IdVenta { get; set; }
    public string? TypeComprobante { get; set; }
    public string? NumberComprobante { get; set; }
    public string IdUser { get; set; } //= null!;
    public string? Date { get; set; }
    public string State { get; set; }//Pendiente, Pagada, Rechazada
    [JsonIgnore]
    public virtual ICollection<DetalleVentum>? DetalleVenta { get; set; } //= new List<DetalleVentum>();
    [JsonIgnore]
    public virtual User? IdUserNavigation { get; set; } = null!;

}
