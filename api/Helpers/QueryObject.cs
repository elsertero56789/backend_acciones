using System;

namespace api.Helpers;

public class QueryObject
{
    public string? Simbolo { get; set; } = null;
    public string? NombreCompania { get; set; } = null;

    public string? OrdenarPor { get; set; } = null;
    public bool IsDescending { get; set; } = false;
}
