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
    public class AutoresRepository(BibliotecaContext _context) : IAutoresRepository
    {
        public async Task<List<AutorDTO>> GetAll()
        {
            return await _context.Autores
                .Where(a => !a.IsDeleted)
                .Select(a => new AutorDTO
                {
                    Nombre = a.Nombre
                }).ToListAsync();
        }

        public async Task<AutorDTO?> GetById(int id)
        {
            return await _context.Autores
                .Where(a => a.AutorId == id && !a.IsDeleted)
                .Select(a => new AutorDTO
                {
                    Nombre = a.Nombre
                }).FirstOrDefaultAsync();
        }
        public async Task<bool> Create(AutorDTO autor)
        { 
            var nombreNormalizado = autor.Nombre.Trim();
            
            var autorExistente = await _context.Autores
                .FirstOrDefaultAsync(a =>
                    a.Nombre.ToLower() == nombreNormalizado.ToLower());
            
            if (autorExistente != null)
            {
                if (!autorExistente.IsDeleted)
                {
                    return false;
                }

                autorExistente.IsDeleted = false;
                autorExistente.Nombre = nombreNormalizado;

                return await _context.SaveChangesAsync() > 0;
            }

            var nuevoAutor = new Autores
            {
                Nombre = nombreNormalizado,
                IsDeleted = false
            };

            _context.Autores.Add(nuevoAutor);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Update(int id, AutorDTO autor)
        {
            var autorExistente = await _context.Autores
                .FirstOrDefaultAsync(a => a.AutorId == id && !a.IsDeleted);

            if (autorExistente == null)
            {
                return false;
            }

            var nombreNormalizado = autor.Nombre.Trim();

            var existeOtroAutor = await _context.Autores
                .AnyAsync(a =>
                    a.AutorId != id &&
                    !a.IsDeleted &&
                    a.Nombre.ToLower() == nombreNormalizado.ToLower());

            if (existeOtroAutor)
            {
                return false;
            }

            autorExistente.Nombre = nombreNormalizado;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var autorExistente = await _context.Autores
                .FirstOrDefaultAsync(a =>
                    a.AutorId == id &&
                    !a.IsDeleted);

            if (autorExistente == null)
            {
                return false;
            }
            autorExistente.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
