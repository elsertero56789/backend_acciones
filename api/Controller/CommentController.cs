using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/v1/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository repo, IStockRepository stockRepo)
        {
            _repo = repo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _repo.GetAllAsync();
            var commentDto = commentModel.Select(s => s.ToCommentDto());
            if (commentModel != null)
            {
                return Ok(commentDto);
            }
            return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _repo.GetByIdAsync(id);
            if (comment != null)
            {
                return Ok(comment.ToCommentDto());
            }
            else
                return NotFound();
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepo.stockExists(stockId))
            {
                return BadRequest("No existe la accion");
            }

            var commentModel = comment.ToCreateCommentDTO(stockId);
            await _repo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(getCommentById), new { id = commentModel.Id }, commentModel.ToCommentDto());

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDTO updateCommentRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _repo.UpdateByIdAsync(id, updateCommentRequestDTO.ToUpdateCommentRequestDTO());
            if (comment != null)
            {
                return Ok(comment.ToCommentDto());
            }
            else
            {
                return NotFound("Comentario no Encontrado.");
            }



        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _repo.DeleteCommentAsync(id);
            if(comment != null)
            {
                return NoContent();
            }
            else{
                return NotFound("El Comentario no existe");
            }
        }
    }
}