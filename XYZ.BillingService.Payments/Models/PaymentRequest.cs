namespace XYZ.BillingService.Payments.Models
{
    public class PaymentRequest
    {
        public required string OrderNumber { get; set; } 
        public string? Description { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
