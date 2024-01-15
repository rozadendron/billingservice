namespace XYZ.BillingService.Payments.Models
{
    public class Receipt : Order
    {
        public Guid ReceiptId { get; set; }
        public Guid PaymentId { get; set; }       
    }
}
