using System;
using System.Collections.Generic;

namespace prestaToolsApi.ModelsEntity;

public partial class Tool
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Reference { get; set; } = null!;

    public bool? New { get; set; }

    public string? Model { get; set; }

    public string? Description { get; set; }

    public bool? Widgets { get; set; }

    public decimal ValueCommercial { get; set; }

    public decimal ValueRent { get; set; }

    public int? YearBuy { get; set; }

    public decimal? Weigt { get; set; }

    public decimal? Mesuare { get; set; }

    public int? NumberPiece { get; set; }

    public string? UrlImage { get; set; }

    public string? UrlImage2 { get; set; }

    public string? UrlImage3 { get; set; }

    public string? TermsUse { get; set; }

    public string BreakDowns { get; set; } = null!;

    public decimal? TimeUse { get; set; }

    public int IdCategory { get; set; }

    public string IdLenders { get; set; } = null!;

    public string DateUp { get; set; } = null!;

    public int? Rate { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Category objetoCategoria { get; set; } = null!;

    public virtual Lender objetoLender { get; set; } = null!;
}
