﻿namespace Domain.APIModels;

public class Exchange
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Image { get; set; } = null!;
}