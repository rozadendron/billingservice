using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.Models;
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

            var testResult = await paymentGatewayService.ProcessPayment(new BillingService.Payments.Models.PaymentRequest() { OrderNumber = "1", PaymentAmount = 0, Description = "test" });

            Assert.NotNull(testResult);
            Assert.Equal(PaymentStatus.Failed, testResult.PaymentCode);
            Assert.NotEqual(Guid.Empty, testResult.PaymentId);
        }

        [Fact]
        public async void ProcessPaymentSucessResult()
        {
            IPaymentGateway paymentGatewayService = new PayPalGateway();
            var testResult = await paymentGatewayService.ProcessPayment(new BillingService.Payments.Models.PaymentRequest() { OrderNumber = "1", PaymentAmount = 10, Description = "test" });
            Assert.NotNull(testResult);
            Assert.Equal(PaymentStatus.Sucessfull, testResult.PaymentCode);
            Assert.NotEqual(Guid.Empty, testResult.PaymentId);
        }
    }
}