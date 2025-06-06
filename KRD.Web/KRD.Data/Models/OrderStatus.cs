using KRD.Data.Enum;

namespace KRD.Data.Models;

public class OrderStatus
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public DateTime Date { get; set; }
}