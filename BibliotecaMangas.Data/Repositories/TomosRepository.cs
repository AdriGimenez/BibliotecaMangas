using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Abstractions.Models;
using BibliotecaMangas.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMangas.Data.Repositories
{
    public class TomosRepository(BibliotecaContext _context) : ITomosRepository
    {
        public async Task<List<TomoDTO>> GetAll()
        {
            return await _context.Tomos
                .Where(t => !t.IsDeleted)
                .Select(t => new TomoDTO
                {
                    ObraId = t.ObraId,
                    NumeroTomo = t.NumeroTomo
                }).ToListAsync();
        }

        public async Task<TomoDTO?> GetById(int id)
        {
            return await _context.Tomos
                .Where(t => t.TomoId == id && !t.IsDeleted)
                .Select(t => new TomoDTO
                {   
                    ObraId = t.ObraId,
                    NumeroTomo = t.NumeroTomo
                }).FirstOrDefaultAsync();
        }
        public async Task<bool> Create(TomoDTO tomo)
        {
            if (tomo.ObraId == null || 
                tomo.NumeroTomo == null ||
                tomo.NumeroTomo <= 0)
            {
                return false;
            }

            var obraId = tomo.ObraId.Value;
            var numeroTomo = tomo.NumeroTomo.Value;

            var obraExistente = await _context.Obras
                .AnyAsync(o => o.ObraId == obraId);

            if(!obraExistente)
            {
                return false;
            }

            var tomoExistente = await _context.Tomos
                .FirstOrDefaultAsync(t =>
                    t.ObraId == obraId &&
                    t.NumeroTomo == numeroTomo);

            if (tomoExistente != null)
            {
                if (!tomoExistente.IsDeleted)
                {
                    return false;
                }

                tomoExistente.IsDeleted = false;
                tomoExistente.ObraId = obraId;
                tomoExistente.NumeroTomo = numeroTomo;

                return await _context.SaveChangesAsync() > 0;
            }

            var nuevoTomo = new Tomos
            {
                ObraId = obraId,
                NumeroTomo = numeroTomo,
                IsDeleted = false
            };

            _context.Tomos.Add(nuevoTomo);
            
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Update(int id, TomoDTO tomo)
        {
            if (tomo.ObraId == null ||
                tomo.NumeroTomo == null ||
                tomo.NumeroTomo <= 0)
            {
                return false;
            }

            var obraId = tomo.ObraId.Value;
            var numeroTomo = tomo.NumeroTomo.Value;

            var tomoExistente = await _context.Tomos
                .FirstOrDefaultAsync(t =>
                    t.TomoId == id &&
                    !t.IsDeleted);

            if (tomoExistente == null)
            {
                return false;
            }

            var obraExistente = await _context.Obras
                .AnyAsync(o => o.ObraId == obraId);

            if(!obraExistente)
            {
                return false;
            }

            var existeOtroTomo = await _context.Tomos
                .AnyAsync(t =>
                    t.TomoId != id &&
                    !t.IsDeleted &&
                    t.ObraId == obraId &&
                    t.NumeroTomo == numeroTomo);

            if (existeOtroTomo)
            {
                return false;
            }

            tomoExistente.ObraId = obraId;
            tomoExistente.NumeroTomo = numeroTomo;

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var tomoExistente = await _context.Tomos
                .FirstOrDefaultAsync(t =>
                    t.TomoId == id &&
                    !t.IsDeleted);

            if (tomoExistente == null)
            {
                return false;
            }

            tomoExistente.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
