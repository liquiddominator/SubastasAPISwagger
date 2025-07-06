using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public int UsuarioId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expira { get; set; }

    public bool Revocado { get; set; }

    public DateTime Creado { get; set; }

    public string? IpCreacion { get; set; }

    public string? ReemplazadoPor { get; set; }

    [JsonIgnore]
    public virtual Usuario Usuario { get; set; } = null!;
}
