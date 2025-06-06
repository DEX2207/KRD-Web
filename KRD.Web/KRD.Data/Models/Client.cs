namespace KRD.Data.Models;

public class Client
{
    public int Id { get; set; }
    public int OrdersId { get; set; }
    public string FullName { get; set; }
    public int ContactId { get; set; }
    public string Address { get; set; }
    public int CarId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public bool WarrantyActive { get; set; }
}