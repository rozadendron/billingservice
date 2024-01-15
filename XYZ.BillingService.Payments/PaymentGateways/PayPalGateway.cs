using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.Models;

namespace XYZ.BillingService.Payments.PaymentGateways
{
    public class PayPalGateway : IPaymentGateway
    {
        public Task<PaymentResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                throw new ArgumentNullException(nameof(paymentRequest));
            }

            var result = new PaymentResult() { PaymentId = Guid.NewGuid() };

            if (paymentRequest.PaymentAmount == 0)
            {
                result.PaymentCode = PaymentStatus.Failed;
            }
            else
            {
                result.PaymentCode = PaymentStatus.Sucessfull;
            }

            return Task.FromResult(result);
        }
    }
}
