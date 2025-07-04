using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string TipoVenta { get; set; } = null!;

    public int VendedorId { get; set; }

    public int CategoriaId { get; set; }

    public int? SubcategoriaId { get; set; }

    public decimal? Precio { get; set; }

    public string Estado { get; set; } = null!;

    public string? Condicion { get; set; }

    public int? Visitas { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    [JsonIgnore]
    public virtual Categoria Categoria { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    [JsonIgnore]
    public virtual ICollection<ImagenesProducto> ImagenesProductos { get; set; } = new List<ImagenesProducto>();

    [JsonIgnore]
    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    [JsonIgnore]
    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();

    [JsonIgnore]
    public virtual Subcategoria? Subcategoria { get; set; }

    [JsonIgnore]
    public virtual Usuario Vendedor { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
