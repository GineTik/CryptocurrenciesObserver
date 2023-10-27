using System.Collections.Generic;
using Domain.APIModels;

namespace Infrastructure.API.ConvertorInterfaces;

public interface ICoinsJsonConvertor : IJsonConvertor<IEnumerable<Coin>>
{
    
}