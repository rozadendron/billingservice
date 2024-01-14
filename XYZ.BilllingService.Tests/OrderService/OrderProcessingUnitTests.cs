using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using XYZ.Billing.BusinessLogic.Errors;
using XYZ.BillingService.Orders.Interfaces;
using XYZ.BillingService.Orders.Services;
using XYZ.BillingService.Payments.Interfaces;
using XYZ.BillingService.Payments.PaymentGateways;

namespace XYZ.BilllingService.Tests.PaymentGateways
{
    public class OrderProcessingUnitTests
    {
        public OrderProcessingUnitTests()
        {


        }

        [Fact]
        public async void OrderProcessNullParam()
        {
            OrderProcessingService orderProcessingService = new OrderProcessingService();
            Task result() => orderProcessingService.ProcessOrder(null);
            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Fact]
        public async void OrderProcessApplePayHappyPassResult()
        {
            OrderProcessingService orderProcessingService = new OrderProcessingService();
            var order = new BillingService.Payments.Models.Order()
            {
                OrderNumber = "1",
                UserId = "1",
                PaymentGatewayId = 1,
                Amount = 1,
                Description = "test"
            };


            var reciept = orderProcessingService.ProcessOrder(order);
            Assert.NotNull(reciept);
            Assert.NotNull(reciept.Result);
            Assert.Equal(reciept.Result.OrderNumber, "1");
            Assert.Equal(reciept.Result.UserId, "1");
            Assert.Equal(reciept.Result.Description, "test");
            Assert.NotNull(reciept.Result.PaymentDate);
        }

        [Fact]
        public async void OrderProcessPayPalHappyPassResult()
        {
            OrderProcessingService orderProcessingService = new OrderProcessingService();
            var order = new BillingService.Payments.Models.Order()
            {
                OrderNumber = "1",
                UserId = "1",
                PaymentGatewayId = 2,
                Amount = 1,
                Description = "test"
            };


            var reciept = orderProcessingService.ProcessOrder(order);
            Assert.NotNull(reciept);
            Assert.NotNull(reciept.Result);
            Assert.Equal(reciept.Result.OrderNumber, "1");
            Assert.Equal(reciept.Result.UserId, "1");
            Assert.Equal(reciept.Result.Description, "test");
            Assert.NotNull(reciept.Result.PaymentDate);
        }

        [Fact]
        public async void OrderProcessNotWorkingGatewayResult()
        {
            OrderProcessingService orderProcessingService = new OrderProcessingService();
            var order = new BillingService.Payments.Models.Order()
            {
                OrderNumber = "1",
                UserId = "1",
                PaymentGatewayId = 3,
                Amount = 1,
                Description = "test"
            };


            var reciept = orderProcessingService.ProcessOrder(order);
            Assert.NotNull(reciept);
            Assert.NotNull(reciept.Result);
            Assert.Equal(reciept.Result.OrderNumber, "1");
            Assert.Equal(reciept.Result.UserId, "1");
            Assert.Equal(reciept.Result.Description, "test");
            Assert.NotNull(reciept.Result.PaymentDate);
        }

        //[Fact]
        //public async void ProcessPaymentSucessResult()
        //{
        //    IPaymentGateway paymentGatewayService = new PayPalGateway();
        //    var testResult = await paymentGatewayService.ProcessPayment(new BillingService.Payments.Models.Order()
        //    {
        //        OrderNumber = "1",
        //        UserId = "1",
        //        PaymentGatewayId = 1,
        //        Amount = 1,
        //        Description = "test"
        //    });

        //    Assert.NotNull(testResult);
        //    Assert.Equal(testResult.OrderNumber, "1");
        //    Assert.Equal(testResult.UserId, "1");
        //    Assert.Equal(testResult.Description, "test");
        //    Assert.NotNull(testResult.PaymentDate);
        //}


    }
}