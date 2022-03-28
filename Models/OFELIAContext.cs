using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Prueba.Backend.Models
{
    public partial class OFELIAContext : DbContext
    {
        public OFELIAContext()
        {
        }

        public OFELIAContext(DbContextOptions<OFELIAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCliente> TblClientes { get; set; }
        public virtual DbSet<TblFactura> TblFacturas { get; set; }
        public virtual DbSet<TblFacturaProducto> TblFacturaProductos { get; set; }
        public virtual DbSet<TblInventario> TblInventarios { get; set; }
        public virtual DbSet<TblProducto> TblProductos { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=OFELIA;user id=sa;password=12345;");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Identificador unico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Direccion del cliente");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasComment("Fecha de nacimiento del cliente");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre del cliente");

                entity.Property(e => e.NumeroDocumento)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("Numero de documento del cliente");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Telefono del cliente");

                entity.Property(e => e.TipoDocumento)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("Tipo de documento del cliente");
            });

            modelBuilder.Entity<TblFactura>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Identificador unico");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasComment("Fecha de generacion");

                entity.Property(e => e.IdCliente).HasComment("Identificador del cliente");

                entity.Property(e => e.TotalVenta)
                    .HasColumnType("money")
                    .HasComment("Valor total de la venta");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TblFacturas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblFacturas_TblClientes");
            });

            modelBuilder.Entity<TblFacturaProducto>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Identificador unico");

                entity.Property(e => e.Cantidad).HasComment("Cantidad de producto vendido");

                entity.Property(e => e.IdFactura).HasComment("Identificador de la factura a la cual pertenecen los productos");

                entity.Property(e => e.IdProducto).HasComment("Identificador del producto");

                entity.Property(e => e.ValorTotal)
                    .HasColumnType("money")
                    .HasComment("Valor total del producto (Cantidad x PrecioUnitario)");

                entity.Property(e => e.ValorUnitario)
                    .HasColumnType("money")
                    .HasComment("Valor unitario del producto");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.TblFacturaProductos)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblFacturaProductos_TblFacturas");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TblFacturaProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblFacturaProductos_TblProductos");
            });

            modelBuilder.Entity<TblInventario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TblInventario");

                entity.Property(e => e.CantidadActual).HasComment("Existencia actual del producto");

                entity.Property(e => e.CantidadMinima)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasComment("Identificador unico");

                entity.Property(e => e.IdProducto).HasComment("Identificador del producto");
            });

            modelBuilder.Entity<TblProducto>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Identificador unico");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Codigo del producto");

                entity.Property(e => e.Estado).HasComment("Estado del producto (1=activo, 0=inactivo)");

                entity.Property(e => e.ExistenciaActual).HasComment("Existencia del producto");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre del producto");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("money")
                    .HasComment("Precio de venta del producto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
