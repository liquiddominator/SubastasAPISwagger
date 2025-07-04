using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SubastasAPI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Calle { get; set; }

    public string? Ciudad { get; set; }

    public string? CodigoPostal { get; set; }

    public string? UrlImagen { get; set; }

    public string Estado { get; set; } = null!;

    public double? Reputacion { get; set; }

    public decimal Saldo { get; set; }

    public decimal SaldoBloqueado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    [JsonIgnore]
    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    [JsonIgnore]
    public virtual ICollection<Mensaje> MensajeEmisors { get; set; } = new List<Mensaje>();

    [JsonIgnore]
    public virtual ICollection<Mensaje> MensajeReceptors { get; set; } = new List<Mensaje>();

    [JsonIgnore]
    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    [JsonIgnore]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    [JsonIgnore]
    public virtual ICollection<Puja> Pujas { get; set; } = new List<Puja>();

    [JsonIgnore]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

    [JsonIgnore]
    public virtual ICollection<Subasta> SubastaGanadors { get; set; } = new List<Subasta>();

    [JsonIgnore]
    public virtual ICollection<Subasta> SubastaVendedors { get; set; } = new List<Subasta>();

    [JsonIgnore]
    public virtual ICollection<Valoracione> ValoracioneCompradors { get; set; } = new List<Valoracione>();

    [JsonIgnore]
    public virtual ICollection<Valoracione> ValoracioneVendedors { get; set; } = new List<Valoracione>();

    [JsonIgnore]
    public virtual ICollection<Venta> VentaCompradors { get; set; } = new List<Venta>();

    [JsonIgnore]
    public virtual ICollection<Venta> VentaVendedors { get; set; } = new List<Venta>();

    [JsonIgnore]
    public virtual ICollection<WalletTransaccione> WalletTransacciones { get; set; } = new List<WalletTransaccione>();

    [JsonIgnore]
    public virtual ICollection<Role> Rols { get; set; } = new List<Role>();
}
