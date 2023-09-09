using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace prestaToolsApi.ModelsEntity;

public partial class Category
{
    public int? IdCat { get; set; }
    public string? TitleCat { get; set; } = null!;
    public string? DescripCat { get; set; } = null!;
    public string? UrlImagen { get; set; } = null!;
    public string? UrlImagenBanner { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Tool>? Tools { get; set; } = new List<Tool>();
}
