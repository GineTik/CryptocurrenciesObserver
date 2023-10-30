using System.Collections.Generic;
using Domain.APIModels;

namespace Infrastructure.API.ConvertorInterfaces;

public interface ICoinPriceHistoryJsonConverter : IJsonConvertor<IEnumerable<CoinPriceHistory>>
{
    
}