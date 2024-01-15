using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.Models;
using XYZ.BillingService.Payments.PaymentGateways;

namespace XYZ.BilllingService.Tests.PaymentGateways
{
    public class NotWorkingGatewayUnitTests
    {
        [Fact]
        public async void ProcessPaymentNotImplemented()
        {
            IPaymentGateway paymentGatewayService = new AlwaysNotWorkingGateway();
            var testResult = await paymentGatewayService.ProcessPayment(new BillingService.Payments.Models.PaymentRequest() { OrderNumber = "1", PaymentAmount = 10, Description = "test" });

            Assert.NotNull(testResult);
            Assert.Equal(PaymentStatus.Failed, testResult.PaymentCode);
            Assert.Equal(Guid.Empty, testResult.PaymentId);
        }     
    }
}