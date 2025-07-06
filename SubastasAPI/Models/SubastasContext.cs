using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SubastasAPI.Models;

public partial class SubastasContext : DbContext
{
    public SubastasContext()
    {
    }

    public SubastasContext(DbContextOptions<SubastasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<ImagenesProducto> ImagenesProductos { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Puja> Pujas { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subasta> Subastas { get; set; }

    public virtual DbSet<Subcategoria> Subcategorias { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Valoracione> Valoraciones { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<WalletTransaccione> WalletTransacciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E580C64997");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.EnvioId).HasName("PK__Envios__D024E23F38187818");

            entity.HasIndex(e => new { e.Estado, e.FechaSolicitud }, "idx_Envios_Estado_Fecha");

            entity.Property(e => e.CostoEnvio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DireccionEnvio).HasColumnType("text");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");
            entity.Property(e => e.MetodoEnvio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroGuia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones).HasColumnType("text");
            entity.Property(e => e.ProveedorEnvio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UrlSeguimiento)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Venta).WithMany(p => p.Envios)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Envios__VentaId__02FC7413");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.FavoritoId).HasName("PK__Favorito__CFF711E5A9C27B0E");

            entity.HasIndex(e => e.UsuarioId, "idx_Favoritos_UsuarioId");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Producto).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Favoritos__Produ__72C60C4A");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Favoritos__Usuar__71D1E811");
        });

        modelBuilder.Entity<ImagenesProducto>(entity =>
        {
            entity.HasKey(e => e.ImagenId).HasName("PK__Imagenes__0C7D20B7887E86EC");

            entity.ToTable("ImagenesProducto");

            entity.Property(e => e.UrlImagen)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.ImagenesProductos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__ImagenesP__Produ__534D60F1");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.MensajeId).HasName("PK__Mensajes__FEA0555F39D12A22");

            entity.HasIndex(e => new { e.EmisorId, e.ReceptorId, e.Fecha }, "idx_Mensajes_Conversacion");

            entity.Property(e => e.ExtensionArchivo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Leido).HasDefaultValue(false);
            entity.Property(e => e.Mensaje1)
                .HasColumnType("text")
                .HasColumnName("Mensaje");
            entity.Property(e => e.NombreArchivoOriginal)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PesoArchivoKb).HasColumnName("PesoArchivoKB");
            entity.Property(e => e.TipoContenido)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UrlArchivo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Emisor).WithMany(p => p.MensajeEmisors)
                .HasForeignKey(d => d.EmisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensajes__Emisor__6B24EA82");

            entity.HasOne(d => d.Producto).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Mensajes__Produc__6EF57B66");

            entity.HasOne(d => d.Receptor).WithMany(p => p.MensajeReceptors)
                .HasForeignKey(d => d.ReceptorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensajes__Recept__6C190EBB");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC1202467115BFD");

            entity.HasIndex(e => new { e.UsuarioId, e.Leida }, "idx_Notificaciones_Usuario_Leida");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Mensaje).HasColumnType("text");
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Notificac__Usuar__6754599E");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__F00B613844DE922C");

            entity.ToTable(tb => tb.HasTrigger("trg_RegistrarWalletTransaccion_Pago"));

            entity.HasIndex(e => new { e.Estado, e.FechaSolicitud }, "idx_Pagos_Estado_Fecha");

            entity.HasIndex(e => e.IdTransaccionExterna, "idx_Pagos_IdTransaccionExterna");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaProcesamiento).HasColumnType("datetime");
            entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");
            entity.Property(e => e.IdTransaccionExterna)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Intentos).HasDefaultValue(1);
            entity.Property(e => e.Ipcliente)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("IPCliente");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("BOB");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Referencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent).HasColumnType("text");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__UsuarioId__7C4F7684");

            entity.HasOne(d => d.Venta).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__VentaId__7B5B524B");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA3CE0CB0B1");

            entity.HasIndex(e => new { e.CategoriaId, e.SubcategoriaId }, "idx_Productos_CategoriaSubcategoria");

            entity.HasIndex(e => e.Estado, "idx_Productos_Estado");

            entity.HasIndex(e => e.VendedorId, "idx_Productos_VendedorId");

            entity.Property(e => e.Condicion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("subasta");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__Categ__4D94879B");

            entity.HasOne(d => d.Subcategoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.SubcategoriaId)
                .HasConstraintName("FK__Productos__Subca__4E88ABD4");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Productos)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__Vende__4CA06362");
        });

        modelBuilder.Entity<Puja>(entity =>
        {
            entity.HasKey(e => e.PujaId).HasName("PK__Pujas__0F67A0DC12D4B3B2");

            entity.ToTable(tb => tb.HasTrigger("trg_BloquearSaldo_Puja"));

            entity.HasIndex(e => new { e.SubastaId, e.Fecha }, "idx_Pujas_SubastaId_Fecha");

            entity.HasIndex(e => e.UsuarioId, "idx_Pujas_UsuarioId");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Subasta).WithMany(p => p.Pujas)
                .HasForeignKey(d => d.SubastaId)
                .HasConstraintName("FK__Pujas__SubastaId__5BE2A6F2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pujas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Pujas__UsuarioId__5CD6CB2B");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK__RefreshT__F5845E39CE987A98");

            entity.HasIndex(e => e.Token, "IX_RefreshTokens_Token");

            entity.Property(e => e.Creado)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Expira).HasColumnType("datetime");
            entity.Property(e => e.IpCreacion).HasMaxLength(45);
            entity.Property(e => e.ReemplazadoPor).HasMaxLength(255);
            entity.Property(e => e.Token).HasMaxLength(255);

            entity.HasOne(d => d.Usuario).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_RefreshTokens_Usuarios");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.ReporteId).HasName("PK__Reportes__0B29EA6E019C6C03");

            entity.HasIndex(e => new { e.Estado, e.TipoObjeto }, "idx_Reportes_Estado_Tipo");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pendiente");
            entity.Property(e => e.FechaReporte)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Motivo).HasColumnType("text");
            entity.Property(e => e.TipoObjeto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioReportante).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.UsuarioReportanteId)
                .HasConstraintName("FK__Reportes__Usuari__07C12930");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302F11B4A5F79");

            entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCF0BF40034").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subasta>(entity =>
        {
            entity.HasKey(e => e.SubastaId).HasName("PK__Subastas__46C5CE1AB6759314");

            entity.ToTable(tb => tb.HasTrigger("trg_FinalizarSubasta_Desbloquear"));

            entity.HasIndex(e => new { e.Estado, e.FechaFin }, "idx_Subastas_Estado_FechaFin");

            entity.HasIndex(e => e.GanadorId, "idx_Subastas_GanadorId");

            entity.HasIndex(e => e.ProductoId, "idx_Subastas_ProductoId");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.IncrementoMinimo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioActual).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioInicial).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioReserva).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Ganador).WithMany(p => p.SubastaGanadors)
                .HasForeignKey(d => d.GanadorId)
                .HasConstraintName("FK__Subastas__Ganado__59063A47");

            entity.HasOne(d => d.Producto).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subastas__Produc__5629CD9C");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.SubastaVendedors)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subastas__Vended__571DF1D5");
        });

        modelBuilder.Entity<Subcategoria>(entity =>
        {
            entity.HasKey(e => e.SubcategoriaId).HasName("PK__Subcateg__2FEBBB622154B8EB");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Subcategoria)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subcatego__Categ__47DBAE45");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B86AB4C95B");

            entity.HasIndex(e => e.Email, "idx_Usuarios_Email");

            entity.HasIndex(e => e.Username, "idx_Usuarios_Username");

            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Calle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaldoBloqueado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(d => d.Rols).WithMany(p => p.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsuarioRo__RolId__3E52440B"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsuarioRo__Usuar__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("UsuarioId", "RolId").HasName("PK__UsuarioR__24AFD7972877A50A");
                        j.ToTable("UsuarioRoles");
                    });
        });

        modelBuilder.Entity<Valoracione>(entity =>
        {
            entity.HasKey(e => e.ValoracionId).HasName("PK__Valoraci__BFB912FDA11D834F");

            entity.HasIndex(e => e.VendedorId, "idx_Valoraciones_VendedorId");

            entity.Property(e => e.Comentario).HasColumnType("text");
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Comprador).WithMany(p => p.ValoracioneCompradors)
                .HasForeignKey(d => d.CompradorId)
                .HasConstraintName("FK__Valoracio__Compr__76969D2E");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.ValoracioneVendedors)
                .HasForeignKey(d => d.VendedorId)
                .HasConstraintName("FK__Valoracio__Vende__75A278F5");

            entity.HasOne(d => d.Venta).WithMany(p => p.Valoraciones)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK__Valoracio__Venta__778AC167");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.VentaId).HasName("PK__Ventas__5B4150AC2C649CDC");

            entity.ToTable(tb => tb.HasTrigger("trg_ActualizarEstadoProducto_Venta"));

            entity.HasIndex(e => e.CompradorId, "idx_Ventas_Comprador");

            entity.HasIndex(e => new { e.Estado, e.Fecha }, "idx_Ventas_Estado_Fecha");

            entity.HasIndex(e => e.VendedorId, "idx_Ventas_Vendedor");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Comprador).WithMany(p => p.VentaCompradors)
                .HasForeignKey(d => d.CompradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__Comprado__6383C8BA");

            entity.HasOne(d => d.Producto).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__Producto__60A75C0F");

            entity.HasOne(d => d.Subasta).WithMany(p => p.Venta)
                .HasForeignKey(d => d.SubastaId)
                .HasConstraintName("FK__Ventas__SubastaI__619B8048");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.VentaVendedors)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__Vendedor__628FA481");
        });

        modelBuilder.Entity<WalletTransaccione>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__WalletTr__86A849FEAA93AC94");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Referencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.WalletTransacciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__WalletTra__Usuar__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
