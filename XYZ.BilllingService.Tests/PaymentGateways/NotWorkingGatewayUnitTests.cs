using XYZ.Billing.BusinessLogic.Errors;
using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.PaymentGateways;

namespace XYZ.BilllingService.Tests.PaymentGateways
{
    public class NotWorkingGatewayUnitTests
    {
        [Fact]
        public async void ProcessPaymentNotImplemented()
        {
            IPaymentGateway paymentGatewayService = new AlwaysNotWorkingGateway();
            Assert.ThrowsAnyAsync<PaymentNotProcessedException>(async () => paymentGatewayService.ProcessPayment(null));

        }     

    }
}