using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaPagosServiceBus.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    TransaccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoTransaccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CuentaDestino = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    DetallesAdicionales = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaProcesamiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaNotificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.TransaccionId);
                });

            migrationBuilder.CreateTable(
                name: "EventosTransaccion",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransaccionId = table.Column<int>(type: "int", nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosTransaccion", x => x.EventoId);
                    table.ForeignKey(
                        name: "FK_EventosTransaccion_Transacciones_TransaccionId",
                        column: x => x.TransaccionId,
                        principalTable: "Transacciones",
                        principalColumn: "TransaccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    NotificacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransaccionId = table.Column<int>(type: "int", nullable: false),
                    EmailCliente = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EstadoNotificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.NotificacionId);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Transacciones_TransaccionId",
                        column: x => x.TransaccionId,
                        principalTable: "Transacciones",
                        principalColumn: "TransaccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Transacciones",
                columns: new[] { "TransaccionId", "CuentaDestino", "DetallesAdicionales", "Estado", "FechaCreacion", "FechaNotificacion", "FechaProcesamiento", "Monto", "TipoTransaccion" },
                values: new object[,]
                {
                    { 1, "4532123412341234", "Compra portátil gaming", "Exitosa", new DateTime(2024, 2, 10, 10, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 10, 10, 17, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 10, 10, 16, 30, 0, DateTimeKind.Unspecified), 450.90m, "PagoConTarjeta" },
                    { 2, "ES9121000418450200051332", "Pago mensual alquiler", "Pendiente", new DateTime(2024, 2, 12, 9, 30, 0, 0, DateTimeKind.Unspecified), null, null, 1200.00m, "TransferenciaBancaria" },
                    { 3, "5555444433332222", "Suscripción plataforma streaming", "Fallida", new DateTime(2024, 2, 8, 18, 45, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 2, 8, 18, 46, 10, 0, DateTimeKind.Unspecified), 89.99m, "PagoConTarjeta" }
                });

            migrationBuilder.InsertData(
                table: "EventosTransaccion",
                columns: new[] { "EventoId", "Descripcion", "FechaEvento", "TipoEvento", "TransaccionId" },
                values: new object[,]
                {
                    { 1, "Pago autorizado por el banco", new DateTime(2024, 2, 10, 10, 16, 30, 0, DateTimeKind.Unspecified), "Transacción aprobada", 1 },
                    { 2, "Esperando validación de fondos", new DateTime(2024, 2, 12, 9, 30, 0, 0, DateTimeKind.Unspecified), "Transacción creada", 2 },
                    { 3, "Saldo insuficiente", new DateTime(2024, 2, 8, 18, 46, 10, 0, DateTimeKind.Unspecified), "Transacción fallida", 3 }
                });

            migrationBuilder.InsertData(
                table: "Notificaciones",
                columns: new[] { "NotificacionId", "EmailCliente", "EstadoNotificacion", "FechaEnvio", "TransaccionId" },
                values: new object[] { 1, "cliente1@email.com", "Enviada", new DateTime(2024, 2, 10, 10, 17, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_EventosTransaccion_TransaccionId",
                table: "EventosTransaccion",
                column: "TransaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_TransaccionId",
                table: "Notificaciones",
                column: "TransaccionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_Estado",
                table: "Transacciones",
                column: "Estado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventosTransaccion");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Transacciones");
        }
    }
}
