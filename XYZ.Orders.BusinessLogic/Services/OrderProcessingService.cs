using XYZ.BillingService.Orders.Interfaces;
using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.Models;
using XYZ.BillingService.Payments.PaymentGateways;

namespace XYZ.BillingService.Orders.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        public async Task<Receipt> ProcessOrder(Order orderToProcess)
        {
            if (orderToProcess == null)
            {
                throw new ArgumentNullException(nameof(orderToProcess));
            }

            var paymentGateway = GetPaymentGateway(orderToProcess.PaymentGatewayId);
            return await paymentGateway.ProcessPayment(orderToProcess);
        }

        private IPaymentGateway GetPaymentGateway(int gatewayId)
        {
            switch (gatewayId)
            {
                case 1:
                    return new PayPalGateway();
                case 2:
                    return new ApplePayGateway();
                default:
                    return new AlwaysNotWorkingGateway();
            }
        }
    }
}

