using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class WalletTransaccione
{
    public int TransaccionId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Tipo { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Referencia { get; set; }

    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
}
