namespace KRD.Data.Models;

public class CarOption
{
    public int CarId { get; set; }
    public int OptionId { get; set; }
    
    public Car Car { get; set; }
    public Option Option { get; set; }
}