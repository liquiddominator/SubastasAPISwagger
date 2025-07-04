using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Venta
{
    public int VentaId { get; set; }

    public int ProductoId { get; set; }

    public int? SubastaId { get; set; }

    public int VendedorId { get; set; }

    public int CompradorId { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public string Estado { get; set; } = null!;

    [JsonIgnore]
    public virtual Usuario Comprador { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    [JsonIgnore]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    [JsonIgnore]
    public virtual Producto Producto { get; set; } = null!;

    [JsonIgnore]
    public virtual Subasta? Subasta { get; set; }

    [JsonIgnore]
    public virtual ICollection<Valoracione> Valoraciones { get; set; } = new List<Valoracione>();

    [JsonIgnore]
    public virtual Usuario Vendedor { get; set; } = null!;
}
