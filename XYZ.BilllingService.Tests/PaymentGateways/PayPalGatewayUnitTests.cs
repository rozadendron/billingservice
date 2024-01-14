using XYZ.Billing.BusinessLogic.Errors;
using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.PaymentGateways;

namespace XYZ.BilllingService.Tests.PaymentGateways
{
    public class PayPalGatewayUnitTests
    {
        [Fact]
        public async void ProcessPaymentEmptyParam()
        {
            IPaymentGateway paymentGatewayService = new PayPalGateway();
            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => paymentGatewayService.ProcessPayment(null));

        }

        [Fact]
        public async void ProcessPaymentZeroAmmountError()
        {
            IPaymentGateway paymentGatewayService = new PayPalGateway();
            await Assert.ThrowsAnyAsync<PaymentNotProcessedException>(async () => paymentGatewayService.ProcessPayment(new BillingService.Payments.Models.Order()
            {
                OrderNumber = "1",
                UserId = "1",
                PaymentGatewayId = 1,
                Amount = 0
            }));
        }

        [Fact]
        public async void ProcessPaymentSucessResult()
        {
            IPaymentGateway paymentGatewayService = new PayPalGateway();
            var testResult = await paymentGatewayService.ProcessPayment(new BillingService.Payments.Models.Order()
            {
                OrderNumber = "1",
                UserId = "1",
                PaymentGatewayId = 1,
                Amount = 1,
                Description = "test"
            });

            Assert.NotNull(testResult);
            Assert.Equal("1", testResult.OrderNumber);
            Assert.Equal("1", testResult.UserId);
            Assert.Equal("test", testResult.Description);
            Assert.NotNull(testResult.PaymentDate);
        }
    }
}