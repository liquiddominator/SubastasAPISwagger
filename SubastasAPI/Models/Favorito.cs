using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Favorito
{
    public int FavoritoId { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public DateTime Fecha { get; set; }

    [JsonIgnore]
    public virtual Producto? Producto { get; set; }

    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
}
