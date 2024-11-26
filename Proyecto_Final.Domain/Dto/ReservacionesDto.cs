using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final.Domain.Dto;

public class ReservacionesDto
{
    public int ReservacionId { get; set; }

    public int ViajeId { get; set; }
    
    public bool Pago { get; set; }

    public string? Recibo { get; set; }
}
