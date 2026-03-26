using Microsoft.EntityFrameworkCore;
using SistemaPagosServiceBus.Models;

namespace SistemaPagosServiceBus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<EventoTransaccion> EventosTransaccion { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaccion>()
                .HasMany(t => t.Eventos)
                .WithOne(e => e.Transaccion)
                .HasForeignKey(e => e.TransaccionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaccion>()
                .HasOne(t => t.Notificacion)
                .WithOne(n => n.Transaccion)
                .HasForeignKey<Notificacion>(n => n.TransaccionId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Transaccion>()
                .Property(t => t.Monto)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.TipoTransaccion)
                .HasMaxLength(50);

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.CuentaDestino)
                .HasMaxLength(34);

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.DetallesAdicionales)
                .HasMaxLength(250);

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.Estado)
                .HasMaxLength(20);

            modelBuilder.Entity<EventoTransaccion>()
                .Property(e => e.TipoEvento)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.EmailCliente)
                .HasMaxLength(120);

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.EstadoNotificacion)
                .HasMaxLength(50);


            modelBuilder.Entity<Transaccion>()
                .HasIndex(t => t.Estado);

            modelBuilder.Entity<Transaccion>().HasData(
                new Transaccion
                {
                    TransaccionId = 1,
                    Monto = 450.90m,
                    TipoTransaccion = "PagoConTarjeta",
                    CuentaDestino = "4532123412341234",
                    DetallesAdicionales = "Compra portátil gaming",
                    Estado = "Exitosa",
                    FechaCreacion = new DateTime(2024, 2, 10, 10, 15, 0),
                    FechaProcesamiento = new DateTime(2024, 2, 10, 10, 16, 30),
                    FechaNotificacion = new DateTime(2024, 2, 10, 10, 17, 0)
                },
                new Transaccion
                {
                    TransaccionId = 2,
                    Monto = 1200.00m,
                    TipoTransaccion = "TransferenciaBancaria",
                    CuentaDestino = "ES9121000418450200051332",
                    DetallesAdicionales = "Pago mensual alquiler",
                    Estado = "Pendiente",
                    FechaCreacion = new DateTime(2024, 2, 12, 9, 30, 0)
                },
                new Transaccion
                {
                    TransaccionId = 3,
                    Monto = 89.99m,
                    TipoTransaccion = "PagoConTarjeta",
                    CuentaDestino = "5555444433332222",
                    DetallesAdicionales = "Suscripción plataforma streaming",
                    Estado = "Fallida",
                    FechaCreacion = new DateTime(2024, 2, 8, 18, 45, 0),
                    FechaProcesamiento = new DateTime(2024, 2, 8, 18, 46, 10)
                }
            );

            modelBuilder.Entity<EventoTransaccion>().HasData(
                new EventoTransaccion
                {
                    EventoId = 1,
                    TransaccionId = 1,
                    TipoEvento = "Transacción aprobada",
                    Descripcion = "Pago autorizado por el banco",
                    FechaEvento = new DateTime(2024, 2, 10, 10, 16, 30)
                },
                new EventoTransaccion
                {
                    EventoId = 2,
                    TransaccionId = 2,
                    TipoEvento = "Transacción creada",
                    Descripcion = "Esperando validación de fondos",
                    FechaEvento = new DateTime(2024, 2, 12, 9, 30, 0)
                },
                new EventoTransaccion
                {
                    EventoId = 3,
                    TransaccionId = 3,
                    TipoEvento = "Transacción fallida",
                    Descripcion = "Saldo insuficiente",
                    FechaEvento = new DateTime(2024, 2, 8, 18, 46, 10)
                }
            );

            modelBuilder.Entity<Notificacion>().HasData(
                new Notificacion
                {
                    NotificacionId = 1,
                    TransaccionId = 1,
                    EmailCliente = "cliente1@email.com",
                    EstadoNotificacion = "Enviada",
                    FechaEnvio = new DateTime(2024, 2, 10, 10, 17, 0)
                }
            );
        }
    }
}