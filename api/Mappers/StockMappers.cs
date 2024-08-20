using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDTO ToStockDto(this Stock stockModel)
        {
            return new StockDTO{
                Id = stockModel.Id,
                Simbolo = stockModel.Symbol,
                NombreCompania= stockModel.CompanyName,
                Compra = stockModel.Purchase,
                UltimoDividendo = stockModel.LastDiv,
                Industria = stockModel.Industry,
                CapitalizdeMercado = stockModel.MarketCapital,
                Comentarios = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto (this CreateStockRequestDto stockDto)
        {
            return new Stock{
                Symbol = stockDto.Simbolo,
                CompanyName = stockDto.NombreCompania,
                Purchase = stockDto.Compra,
                LastDiv = stockDto.UltimoDividendo,
                Industry = stockDto.Industria,
                MarketCapital = stockDto.CapitalizdeMercado
            };
        }
    }
}