using KRD.Data.Enum;

namespace KRD.Data.Models;

public class Car
{
    public int Id { get; set; } 
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Generation { get; set; }
    public string Config {get;set;}
    public Color Color { get; set; }
}