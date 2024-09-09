using System;
using Microsoft.AspNetCore.Mvc;

namespace api.Helpers;

public class QueryObject
{
    public string? Simbolo { get; set; } = null;
    public string? NombreCompania { get; set; } = null;

    public string? OrdenarPor { get; set; } = null;
    public bool IsDescending { get; set; } = false;

    public int NumeroPaginac { get; set; } = 1;

    [FromQuery(Name = "tamañoPaginacion")]
    public int TamañoPaginac { get; set; } = 20;
}
