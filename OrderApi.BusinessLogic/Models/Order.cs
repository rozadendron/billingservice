namespace XYZ.BillingService.Payments.Models
{
    public class Order
    {
        public required string OrderNumber { get; set; }
        public required string UserId { get; set; }
        public decimal Amount { get; set; }            
        public int PaymentGatewayId { get; set; }
        public string? Description { get; set; }        
    }
}
