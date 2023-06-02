using System;
using System.Collections.Generic;

namespace prestaToolsApi.models_DB;

public partial class ToolEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Reference { get; set; } = null!;

    public bool Nuevo { get; set; }

    public string Model { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Widgets { get; set; }

    public decimal ValueCommercial { get; set; }

    public decimal ValueRent { get; set; }

    public int YearBuy { get; set; }

    public decimal Weigt { get; set; }

    public decimal Mesuare { get; set; }

    public int NumberPiece { get; set; }

    public string UrlImage1 { get; set; } = null!;

    public string UrlImage2 { get; set; } = null!;

    public string UrlImage3 { get; set; } = null!;

    public string TermsUse { get; set; } = null!;

    public string BreakDowns { get; set; } = null!;

    public decimal TimeUse { get; set; }

    public int IdCategory { get; set; }

    public int IdUser { get; set; }

    public int IdLenders { get; set; }

    public DateTime? DateUp { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual CategoryEntity ObjetCategory { get; set; } = null!;

    public virtual LenderEntity IdLendersNavigation { get; set; } = null!;

    public virtual UserEntity IdUserNavigation { get; set; } = null!;
}
