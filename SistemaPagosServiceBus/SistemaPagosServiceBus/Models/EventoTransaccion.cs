using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPagosServiceBus.Models
{
    public class EventoTransaccion
    {
        [Key]
        public int EventoId { get; set; }

        [ForeignKey("Transaccion")]
        public int TransaccionId { get; set; }
        public string TipoEvento { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime FechaEvento { get; set; } = DateTime.Now;
        public Transaccion? Transaccion { get; set; }
    }
}
