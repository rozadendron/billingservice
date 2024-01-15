using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.Models;

namespace XYZ.BillingService.Payments.PaymentGateways
{
    public class AlwaysNotWorkingGateway : IPaymentGateway
    {
        public Task<PaymentResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                throw new ArgumentNullException(nameof(paymentRequest));
            }

            var result = new PaymentResult
            {
                PaymentId = new Guid(),
                PaymentCode = PaymentStatus.Failed
            };

            return Task.FromResult(result);
        }
    }
}
