using System.Collections.Generic;
using Domain.APIModels;

namespace Infrastructure.API.ConvertorInterfaces;

public interface ITicketsJsonConvertor : IJsonConvertor<IEnumerable<Ticket>>
{
    
}