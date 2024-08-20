using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            try
            {
                await _context.Comments.AddAsync(commentModel);
                await _context.SaveChangesAsync();
                return commentModel;
            }
            catch (DbUpdateException err)
            {

                Console.WriteLine($"Error al crear la entidad en la base de datos: {err.Message}");

                // Podrías registrar el error o lanzar una excepción personalizada
                throw new InvalidOperationException("No se pudo añadir la entidad a la base de datos.", err);

            }
            catch (Exception ex)
            {
                // Manejo de otros errores generales
                Console.WriteLine($"Ocurrió un error: {ex.Message}");

                // Podrías registrar el error o lanzar una excepción personalizada
                throw new Exception("Ocurrió un error inesperado al añadir la entidad.", ex);
            }
        }

        public async Task<Comment> DeleteCommentAsync(int id)
        {
            var commentId = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentId == null)
            {
                return null;
            }
            _context.Remove(commentId);
            await _context.SaveChangesAsync();
            return commentId;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateByIdAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (existingComment != null)
            {
                existingComment.Title = commentModel.Title;
                existingComment.Content = commentModel.Content;

                await _context.SaveChangesAsync();

                return existingComment;
            }
            return null;


        }
    }
}