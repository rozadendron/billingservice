using XYZ.BillingService.Payments.Models;

namespace XYZ.BillingService.Orders.Interfaces
{
    public interface IOrderProcessingService
    {
        public Task<Receipt> ProcessOrder(Order orderToProcess);
    }
}
