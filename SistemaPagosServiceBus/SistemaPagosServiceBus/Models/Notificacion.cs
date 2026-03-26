using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPagosServiceBus.Models
{
    public class Notificacion
    {
        [Key]
        public int NotificacionId { get; set; }

        [ForeignKey("Transaccion")]
        public int TransaccionId { get; set; }
        public string EmailCliente { get; set; } = string.Empty;
        public string EstadoNotificacion { get; set; } = string.Empty;

        public DateTime FechaEnvio { get; set; } = DateTime.Now;

        public Transaccion? Transaccion { get; set; }
    }
}
