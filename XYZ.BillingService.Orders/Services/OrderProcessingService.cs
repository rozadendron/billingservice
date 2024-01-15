using XYZ.BillingService.Orders.Errors;
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

            var paymentResult = await paymentGateway.ProcessPayment(new PaymentRequest()
            {
                OrderNumber = orderToProcess.OrderNumber,
                PaymentAmount = orderToProcess.Amount,
                Description = orderToProcess.Description,
            }) ?? throw new ArgumentException("Payment result from Payment Gateway is null");

            if (paymentResult.PaymentCode == PaymentStatus.Failed)
            {
                throw new PaymentNotProcessedException();
            }

            return new Receipt()
            {
                OrderNumber = orderToProcess.OrderNumber,
                Amount = orderToProcess.Amount,
                ReceiptId = Guid.NewGuid(),
                PaymentId = paymentResult.PaymentId,
                PaymentGatewayId = orderToProcess.PaymentGatewayId,
                UserId = orderToProcess.UserId,
                Description = orderToProcess.Description,
            };
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

