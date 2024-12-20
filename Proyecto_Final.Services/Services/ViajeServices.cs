﻿using Microsoft.EntityFrameworkCore;
using ReyAI_Trasport.Abstracions.Interfaces;
using ReyAI_Trasport.Data.Contexto;
using ReyAI_Trasport.Domain.Dto;
using ReyAI_Trasport.Domain.Models;
using System.Linq.Expressions;

namespace ReyAI_Trasport.Services.Services;

public class ViajeServices(IDbContextFactory<ApplicationDbContext> DbFactory) : IViajeServices
{
	public async Task<ViajesDto> Buscar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var viaje = await contexto.Viajes
	   .Include(p => p.Imagen)
	   .Where(e => e.ViajeId == id)
	   .Select(p => new ViajesDto()
	   {
		   ViajeId = p.ViajeId,
		   Destino = p.Destino,
           NombreDestino = p.Ciudad.Nombre,
           FechaInicio = p.FechaInicio,
		   FechaCierre = p.FechaCierre,
		   EstadoId = p.EstadoVId,
           NombreEstado = p.EstadoViaje.Descripcion,
           personas = p.personas,
		   Precio = p.Precio,
		   TaxistaId = p.TaxistaId,
		   Descripcion = p.Descripcion,
		   Imagenes = p.Imagen.Select(i => new ImagenDto() // Mapear la colección de imágenes  
		   {
			   Id = i.ImagenId,
			   ImagenUrl = i.ImagenUrl,
			   Alt = i.Alt,
			   Base64 = i.Base64,
			   Titulo = i.Titulo,
		   }).ToList()
	   }).FirstOrDefaultAsync();
		return viaje ?? new ViajesDto();
	}

	public async Task<bool> ExisteViaje(int id, string idTaxista)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Viajes
			.AnyAsync(e => e.ViajeId != id
			&& e.Taxista.Id.ToLower().Equals(idTaxista.ToLower()));
	}

	private async Task<bool> Insertar(ViajesDto viajeDto)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var viaje = new Viajes()
		{
			ViajeId = viajeDto.ViajeId,
			Destino = viajeDto.Destino,
			FechaInicio = viajeDto.FechaInicio,
			FechaCierre = viajeDto.FechaCierre,
			personas = viajeDto.personas,
			EstadoVId = viajeDto.EstadoId,
			TaxistaId = viajeDto.TaxistaId,
			Precio = viajeDto.Precio,
			Descripcion = viajeDto.Descripcion,
			Imagen = viajeDto.Imagenes.Select(imagen => new Imagen
			{
				ImagenUrl = imagen.ImagenUrl,
				Base64 = imagen.Base64,
				Alt = imagen.Alt,
				Titulo = imagen.Titulo
			}).ToList()
		};
		await contexto.Viajes.AddAsync(viaje);
		var guardo = await contexto.SaveChangesAsync() > 0;
		viajeDto.ViajeId = viaje.ViajeId;
		return guardo;
	}

	private async Task<bool> Modificar(ViajesDto viajeDto)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var viaje = new Viajes()
		{
			ViajeId = viajeDto.ViajeId,
			Destino = viajeDto.Destino,
			FechaInicio = viajeDto.FechaInicio,
			FechaCierre = viajeDto.FechaCierre,
			EstadoVId = viajeDto.EstadoId,
			TaxistaId = viajeDto.TaxistaId,
			Precio = viajeDto.Precio,
			Descripcion = viajeDto.Descripcion,
			personas = viajeDto.personas
		};
		contexto.Update(viaje);
		var modificado = await contexto.SaveChangesAsync() > 0;
		return modificado;
	}

	private async Task<bool> Existe(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Viajes
			.AnyAsync(e => e.ViajeId == id);
	}
	public async Task<bool> Guardar(ViajesDto viajeDto)
	{
		if (!await Existe(viajeDto.ViajeId))
			return await Insertar(viajeDto);
		else
			return await Modificar(viajeDto);
	}

	public async Task<List<ViajesDto>> Listar(Expression<Func<ViajesDto, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Viajes
			.Include(v => v.Imagen)
			.Select(p => new ViajesDto()
			{
				ViajeId = p.ViajeId,
				Destino = p.Destino,
				NombreDestino = p.Ciudad.Nombre,
				EstadoId = p.EstadoVId,
				NombreEstado = p.EstadoViaje.Descripcion,
				FechaInicio = p.FechaInicio,
				FechaCierre = p.FechaCierre,
				Precio = p.Precio,
				personas = p.personas,
				TaxistaId = p.TaxistaId,
				Descripcion = p.Descripcion,
				Imagenes = p.Imagen.Select(i => new ImagenDto() // Mapear la colección de imágenes  
				{
					Id = i.ImagenId,
					ImagenUrl = i.ImagenUrl,
					Alt = i.Alt,
					Base64 = i.Base64,
					Titulo = i.Titulo,
				}).ToList()

			})
	.Where(criterio)
	.ToListAsync();
	}

	public async Task<bool> ActualizarEstado(ViajesDto viajeDto, int estado)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var viaje = await contexto.Viajes.FindAsync(viajeDto.ViajeId);
		if (viaje == null) 
			return false;

		viaje.EstadoVId = estado;
		var modificado = await contexto.SaveChangesAsync() > 0;
		return modificado;
	}

}
