﻿
namespace ReyAI_Trasport.Domain.Dto;

public class TaxistaDto
{
    public string TaxistaId { get; set; }

    public string NickName { get; set; }

    public string Nombres { get; set; }

    public string Correo { get; set; }

    public bool ExisteVehiculo { get; set; }

    public bool ExisteLicencia { get; set; }
    
    public int EstadoTId { get; set; }
}
