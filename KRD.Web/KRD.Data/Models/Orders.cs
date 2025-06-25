namespace KRD.Data.Models;

public class Orders
{
    public int Id { get; set; }
    public string BuyerFullName { get; set; }
    public int CarId { get; set; }
    public string BuyerAddress { get; set; }
    public int ContactId { get; set; }
    public int OrderStatusId { get; set; }
    public int PaymentInvoce { get; set; }
}