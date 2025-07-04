using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Subasta
{
    public int SubastaId { get; set; }

    public int ProductoId { get; set; }

    public int VendedorId { get; set; }

    public decimal PrecioInicial { get; set; }

    public decimal? PrecioActual { get; set; }

    public decimal? IncrementoMinimo { get; set; }

    public decimal? PrecioReserva { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Estado { get; set; } = null!;

    public int? GanadorId { get; set; }

    public int? CantidadPujas { get; set; }

    [JsonIgnore]
    public virtual Usuario? Ganador { get; set; }

    [JsonIgnore]
    public virtual Producto Producto { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Puja> Pujas { get; set; } = new List<Puja>();

    [JsonIgnore]
    public virtual Usuario Vendedor { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
