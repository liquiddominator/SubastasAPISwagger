using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int VentaId { get; set; }

    public int UsuarioId { get; set; }

    public decimal Monto { get; set; }

    public string? Moneda { get; set; }

    public string Estado { get; set; } = null!;

    public string MetodoPago { get; set; } = null!;

    public string? Proveedor { get; set; }

    public string? IdTransaccionExterna { get; set; }

    public string? Referencia { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public DateTime? FechaProcesamiento { get; set; }

    public string? Ipcliente { get; set; }

    public string? UserAgent { get; set; }

    public int? Intentos { get; set; }

    [JsonIgnore]
    public virtual Usuario Usuario { get; set; } = null!;

    [JsonIgnore]
    public virtual Venta Venta { get; set; } = null!;
}
