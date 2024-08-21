using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreatedAtStockAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;

        }

        public async Task<Stock> DeleteStockAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.NombreCompania))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.NombreCompania));
            }
            if(!string.IsNullOrWhiteSpace(query.Simbolo))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Simbolo));
            }
            if(!string.IsNullOrEmpty(query.OrdenarPor))
            {
                if(query.OrdenarPor.Equals("Simbolo", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> stockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockExistente = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockExistente == null)
            {
                return null;

            }
            stockExistente.Symbol = stockDto.Simbolo;
            stockExistente.CompanyName = stockDto.NombreCompania;
            stockExistente.Purchase = stockDto.Compra;
            stockExistente.LastDiv = stockDto.UltimoDividendo;
            stockExistente.Industry = stockDto.Industria;
            stockExistente.MarketCapital = stockDto.CapitalizdeMercado; 

           await _context.SaveChangesAsync();
            return stockExistente;
        }
    }
}