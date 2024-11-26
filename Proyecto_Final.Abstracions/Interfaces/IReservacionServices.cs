using Proyecto_Final.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final.Abstracions.Interfaces;

public interface IReservacionServices
{
    Task<bool> Guardar(ReservacionesDto reservacionDto);
    Task<ReservacionesDto> Buscar(int id);
    Task<List<ReservacionesDto>> Listar(Expression<Func<ReservacionesDto, bool>> criterio);
    Task<bool> ExisteViaje(int clienteId, int reservacionId, int ViajeId);
}
