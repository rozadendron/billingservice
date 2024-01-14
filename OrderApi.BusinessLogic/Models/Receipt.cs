namespace XYZ.BillingService.Payments.Models
{
    public class Receipt : Order
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
