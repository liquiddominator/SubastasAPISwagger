using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Notificacione
{
    public int NotificacionId { get; set; }

    public int? UsuarioId { get; set; }

    public string Tipo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public bool? Leida { get; set; }

    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
}
