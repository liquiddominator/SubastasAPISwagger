using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Subcategoria
{
    public int SubcategoriaId { get; set; }

    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    [JsonIgnore]
    public virtual Categoria Categoria { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
