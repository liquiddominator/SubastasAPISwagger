using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Envio
{
    public int EnvioId { get; set; }

    public int VentaId { get; set; }

    public string MetodoEnvio { get; set; } = null!;

    public string? ProveedorEnvio { get; set; }

    public string? NumeroGuia { get; set; }

    public string? UrlSeguimiento { get; set; }

    public decimal? CostoEnvio { get; set; }

    public string Estado { get; set; } = null!;

    public string? DireccionEnvio { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string? Observaciones { get; set; }

    [JsonIgnore]
    public virtual Venta Venta { get; set; } = null!;
}
