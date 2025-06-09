using KRD.Data.Enum;

namespace KRD.Data.Models;

public class Option
{
    public int Id { get; set; }
    public OptionType OptionType { get; set; }
    public bool OptionStatus { get; set; }
    
    public ICollection<CarOption> CarOptions { get; set; }
}