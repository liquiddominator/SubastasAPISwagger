using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Reporte
{
    public int ReporteId { get; set; }

    public int? UsuarioReportanteId { get; set; }

    public string? TipoObjeto { get; set; }

    public int ObjetoId { get; set; }

    public string Motivo { get; set; } = null!;

    public string? Estado { get; set; }

    public DateTime? FechaReporte { get; set; }

    [JsonIgnore]
    public virtual Usuario? UsuarioReportante { get; set; }
}
