﻿using Proyecto_Final.Domain.Dto;
using System.Linq.Expressions;

namespace Proyecto_Final.Abstracions.Interface
{
    public interface ITaxistaServices
    {
        Task<bool> Guardar(TaxistaDto taxistaDto);
        Task<bool> Eliminar(TaxistaDto taxistaDto);
        Task<bool> Buscar(int id);
        Task<bool> Listar(Expression<Func<TaxistaDto, bool>> criterio);
        Task<bool> ExisteTaxista(string nombre, int id, string correo, string provincia);
    }
}