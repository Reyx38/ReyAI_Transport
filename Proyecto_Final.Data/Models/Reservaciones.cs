using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final.Data.Models;

public class Reservaciones
{
    public int ReservacionId { get; set; }

    [ForeignKey("Viaje")]
    public int ViajeId { get; set; }
    public Viajes? Viaje { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public bool Pago { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public string? Recibo { get; set; }

}
