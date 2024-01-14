using XYZ.Billing.BusinessLogic.Errors;
using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.Models;

namespace XYZ.BillingService.Payments.PaymentGateways
{
    public class AlwaysNotWorkingGateway : IPaymentGateway
    {
        public async Task<Receipt> ProcessPayment(Order orderToProcess)
        {
            throw new PaymentNotProcessedException();
        }      
    }
}
