using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Puja
{
    public int PujaId { get; set; }

    public int? SubastaId { get; set; }

    public int? UsuarioId { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public string? Estado { get; set; }

    [JsonIgnore]
    public virtual Subasta? Subasta { get; set; }

    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
}
