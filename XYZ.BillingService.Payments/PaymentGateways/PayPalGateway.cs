using XYZ.Billing.BusinessLogic.Errors;
using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.Models;

namespace XYZ.BillingService.Payments.PaymentGateways
{
    public class PayPalGateway : IPaymentGateway
    {
        public Task<Receipt> ProcessPayment(Order orderToProcess)
        {
            if (orderToProcess == null)
            {
                throw new ArgumentNullException(nameof(orderToProcess));
            }

            if (orderToProcess.Amount == 0)
            {
                throw new PaymentNotProcessedException();
            }

            return Task.FromResult<Receipt>(new Receipt()
            {
                OrderNumber = orderToProcess.OrderNumber,
                Amount = orderToProcess.Amount,
                Id = new Guid(),
                PaymentDate = DateTime.Now,
                UserId = orderToProcess.UserId,
                Description = orderToProcess.Description,
            });
        }
    }
}
