using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Valoracione
{
    public int ValoracionId { get; set; }

    public int? VendedorId { get; set; }

    public int? CompradorId { get; set; }

    public int? VentaId { get; set; }

    public int Puntuacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime? Fecha { get; set; }

    [JsonIgnore]
    public virtual Usuario? Comprador { get; set; }

    [JsonIgnore]
    public virtual Usuario? Vendedor { get; set; }

    [JsonIgnore]
    public virtual Venta? Venta { get; set; }
}
