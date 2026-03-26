using System.ComponentModel.DataAnnotations;

namespace SistemaPagosServiceBus.Models
{
    public class Transaccion
    {
        [Key]
        public int TransaccionId { get; set; }

        [Required(ErrorMessage ="El monto es obligatorio")]

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Selecciona un tipo de transaccion")]
        [StringLength(50)]
        public string? TipoTransaccion { get; set; }

        [Required(ErrorMessage = "Selecciona una cuenta de destino")]
        [StringLength(16, ErrorMessage = "La longitud no es valida")]
        public string? CuentaDestino { get; set; }

        [StringLength(250, ErrorMessage ="El limite de caracteres es 250")]
        public string? DetallesAdicionales { get; set; }

        public string? Estado { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaProcesamiento { get; set; }

        public DateTime? FechaNotificacion { get; set; }

        public ICollection<EventoTransaccion>? Eventos { get; set; }
        public Notificacion? Notificacion { get; set; }
    }
}
