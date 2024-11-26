using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Abstracions.Interfaces;
using Proyecto_Final.Data.Contexto;
using Proyecto_Final.Data.Models;
using Proyecto_Final.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final.Services.Services;

public class ReservacionesServices(IDbContextFactory<Context> DbFactory) : IReservacionServices
{
    public async Task<ReservacionesDto> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var reserva = await contexto.Reservaciones
       .Where(e => e.ReservacionId == id)
       .Select(p => new ReservacionesDto()
       {
           ReservacionId = p.ReservacionId,
           ViajeId = p.ViajeId,
           Pago = p.Pago,
           Recibo = p.Recibo
       })
       .FirstOrDefaultAsync();
        return reserva ?? new ReservacionesDto();
    }

    public async Task<bool> ExisteViaje(int clienteId, int reservacionId, int ViajeId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Reservaciones
            .AnyAsync(e => e.ReservacionId != reservacionId
            && e.Viaje.ClienteId == clienteId
            && e.Viaje.TaxistaId == ViajeId);
    }

    private async Task<bool> Insertar(ReservacionesDto reservacionDto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var reserva = new Reservaciones()
        {
            ReservacionId = reservacionDto.ReservacionId,
            ViajeId = reservacionDto.ViajeId,
            Pago = reservacionDto.Pago,
            Recibo = reservacionDto.Recibo
        };
        contexto.Reservaciones.Add(reserva);
        var guardo = await contexto.SaveChangesAsync() > 0;
        reservacionDto.ReservacionId = reserva.ReservacionId;
        return guardo;
    }

    private async Task<bool> Modificar(ReservacionesDto reservacionDto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var reserva = new Reservaciones()
        {
            ReservacionId = reservacionDto.ReservacionId,
            ViajeId = reservacionDto.ViajeId,
            Pago = reservacionDto.Pago,
            Recibo = reservacionDto.Recibo
        };
        contexto.Update(reserva);
        var modificado = await contexto.SaveChangesAsync() > 0;
        return modificado;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Reservaciones
            .AnyAsync(e => e.ReservacionId == id);
    }

    public async Task<bool> Guardar(ReservacionesDto reservacionDto)
    {
        if (!await Existe(reservacionDto.ViajeId))
            return await Insertar(reservacionDto);
        else
            return await Modificar(reservacionDto);
    }

    public async Task<List<ReservacionesDto>> Listar(Expression<Func<ReservacionesDto, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Reservaciones.Select(p => new ReservacionesDto()
        {
            ReservacionId = p.ReservacionId,
            ViajeId = p.ViajeId,
            Pago = p.Pago,
            Recibo = p.Recibo
        })
        .Where(criterio)
        .ToListAsync();
    }
}
