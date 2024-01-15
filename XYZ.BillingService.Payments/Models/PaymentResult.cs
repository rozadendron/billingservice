namespace XYZ.BillingService.Payments.Models
{
    public class PaymentResult
    {
        public Guid PaymentId { get; set; }
        public PaymentStatus PaymentCode { get; set; }
    }
}
