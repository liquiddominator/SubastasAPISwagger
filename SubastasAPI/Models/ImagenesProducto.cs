using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class ImagenesProducto
{
    public int ImagenId { get; set; }

    public int? ProductoId { get; set; }

    public string? UrlImagen { get; set; }

    [JsonIgnore]
    public virtual Producto? Producto { get; set; }
}
