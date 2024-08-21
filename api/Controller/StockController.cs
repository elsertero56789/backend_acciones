using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Controller
{
    [Route(("api/v1/stock"))]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockrepository;

        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockrepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult>  GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _stockrepository.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto());
            if (!stocks.IsNullOrEmpty())
            {
                return Ok(stockDto);
            }
            else
                return Conflict("No existe datos en el campo stocks");
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockrepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDTO.ToStockFromCreateDto();
            await _stockrepository.CreatedAtStockAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockrepository.UpdateStockAsync(id, updateDto);
            if(stockModel == null){
                return NotFound(409);
            }

            return Ok(stockModel.ToStockDto());
        } 

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockrepository.DeleteStockAsync(id);
            if(stockModel != null){
                return NoContent();
            }else return NotFound();
        }
    }
}