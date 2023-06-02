using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace prestaToolsApi.models_DB;

public partial class CategoryEntity
{
    public int IdCat { get; set; }

    public string NombreCat { get; set; } = null!;

    public string DescripCat { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ToolEntity> Tools { get; set; } = new List<ToolEntity>();
}
