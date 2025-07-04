using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Mensaje
{
    public int MensajeId { get; set; }

    public int EmisorId { get; set; }

    public int ReceptorId { get; set; }

    public string? Mensaje1 { get; set; }

    public string TipoContenido { get; set; } = null!;

    public string? UrlArchivo { get; set; }

    public string? NombreArchivoOriginal { get; set; }

    public string? ExtensionArchivo { get; set; }

    public int? PesoArchivoKb { get; set; }

    public DateTime Fecha { get; set; }

    public bool? Leido { get; set; }

    public int? ProductoId { get; set; }

    [JsonIgnore]
    public virtual Usuario Emisor { get; set; } = null!;

    [JsonIgnore]
    public virtual Producto? Producto { get; set; }

    [JsonIgnore]
    public virtual Usuario Receptor { get; set; } = null!;
}
