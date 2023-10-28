﻿using System.Collections.Generic;

namespace Domain.APIModels;

public class CoinShortWithTickets
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public IEnumerable<Ticket> Tickets { get; set; } = null!;
}