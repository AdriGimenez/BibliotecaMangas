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
    public class EditorialesRepository(BibliotecaContext _context) : IEditorialesRepository
    {
        public async Task<List<EditorialDTO>> GetAll()
        {
            return await _context.Editoriales
                .Where(e => !e.IsDeleted)
                .Select(e => new EditorialDTO
                {
                    Nombre = e.Nombre,
                    Pais = e.Pais
                }).ToListAsync();
        }

        public async Task<EditorialDTO?> GetById(int id)
        {
            return await _context.Editoriales
                .Where(e => e.EditorialId == id && !e.IsDeleted)
                .Select(e => new EditorialDTO
                {
                    Nombre = e.Nombre,
                    Pais = e.Pais
                }).FirstOrDefaultAsync();
        }
        public async Task<bool> Create(EditorialDTO editorial)
        {
            var nombreNormalizado = editorial.Nombre.Trim();
            var paisNormalizado = editorial.Pais.Trim();

            var editorialExistente = await _context.Editoriales
                .FirstOrDefaultAsync(e =>
                    e.Nombre.ToLower() == nombreNormalizado.ToLower() &&
                    e.Pais.ToLower() == paisNormalizado.ToLower());

            if (editorialExistente != null)
            {   
                if (!editorialExistente.IsDeleted)
                {
                    return false;
                }

                editorialExistente.IsDeleted = false;
                editorialExistente.Nombre = nombreNormalizado;
                editorialExistente.Pais = paisNormalizado;

                return await _context.SaveChangesAsync() > 0;
            }

            var nuevaEditorial = new Editoriales
            {
                Nombre = nombreNormalizado,
                Pais = paisNormalizado,
                IsDeleted = false
            };

            _context.Editoriales.Add(nuevaEditorial);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Update(int id, EditorialDTO editorial)
        {
            var editorialExistente = await _context.Editoriales
                .FirstOrDefaultAsync(e =>
                    e.EditorialId == id &&
                    !e.IsDeleted);

            if (editorialExistente == null)
            {
                return false;
            }

            var nombreNormalizado = editorial.Nombre.Trim();
            var paisNormalizado = editorial.Pais.Trim();

            var existeOtraEditorial = await _context.Editoriales
                .AnyAsync(e =>
                    e.EditorialId != id &&
                    !e.IsDeleted &&
                    e.Nombre.ToLower() == nombreNormalizado.ToLower() &&
                    e.Pais.ToLower() == paisNormalizado.ToLower());

            if (existeOtraEditorial)
            {
                return false;
            }
            editorialExistente.Nombre = nombreNormalizado;
            editorialExistente.Pais = paisNormalizado;

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var editorialExistente = await _context.Editoriales
                .FirstOrDefaultAsync(e =>
                    e.EditorialId == id &&
                    !e.IsDeleted);

            if (editorialExistente == null)
            {
                return false;
            }
            editorialExistente.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
