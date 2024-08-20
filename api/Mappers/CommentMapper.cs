using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDto(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Titulo = commentModel.Title,
                Contenido = commentModel.Content,
                FechaCreacion = commentModel.CreatedOn,
                AccionId = commentModel.StockId
            };
        }
        public static Comment ToCreateCommentDTO(this CreateCommentDTO createCommentDto, int StockId)
        {
            return new Comment
            {
                Title = createCommentDto.Titulo,
                Content = createCommentDto.Contenido,
                StockId = StockId
            };
        }
        public static Comment ToUpdateCommentRequestDTO(this UpdateCommentRequestDTO createCommentDto)
        {
            return new Comment
            {
                Title = createCommentDto.Titulo,
                Content = createCommentDto.Contenido,
            };
        }
    }
}