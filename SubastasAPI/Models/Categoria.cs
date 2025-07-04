using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public string Estado { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    [JsonIgnore]
    public virtual ICollection<Subcategoria> Subcategoria { get; set; } = new List<Subcategoria>();
}
