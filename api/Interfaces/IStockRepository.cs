using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreatedAtStockAsync(Stock stockModel);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);

        Task<Stock> DeleteStockAsync (int id);

        Task<bool> stockExists(int id);
    }
}