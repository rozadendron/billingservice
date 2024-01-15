using XYZ.BillingService.Orders.Errors;
using XYZ.BillingService.Orders.Services;

namespace XYZ.BilllingService.Tests.PaymentGateways
{
    public class OrderProcessingUnitTests
    {   
        [Fact]
        public async void OrderProcessNullParam()
        {
            OrderProcessingService orderProcessingService = new OrderProcessingService();
            Task result() => orderProcessingService.ProcessOrder(null);
            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Fact]
        public void OrderProcessApplePayHappyPassResult()
        {
            OrderProcessingService orderProcessingService = new OrderProcessingService();
            var order = new BillingService.Payments.Models.Order()
            {
                OrderNumber = "1",
                UserId = "1",
                PaymentGatewayId = 1,
                Amount = 1,
                Description = "test",                 
            };

            var reciept = orderProcessingService.ProcessOrder(order);
            
            Assert.NotNull(reciept);
            Assert.NotNull(reciept.Result);
            Assert.NotEqual(Guid.Empty, reciept.Result.PaymentId);
            Assert.NotEqual(Guid.Empty, reciept.Result.ReceiptId);
            
            Assert.Equal("1", reciept.Result.OrderNumber);
            Assert.Equal("1", reciept.Result.UserId);
            Assert.Equal("test", reciept.Result.Description);          
        }

        [Fact]
        public void OrderProcessPayPalHappyPassResult()
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
            Assert.Equal("1", reciept.Result.OrderNumber);
            Assert.Equal("1", reciept.Result.UserId);
            Assert.Equal( "test", reciept.Result.Description);           
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

            Task result() => orderProcessingService.ProcessOrder(order);
            await Assert.ThrowsAsync<PaymentNotProcessedException>(result);
        }

        [Fact]
        public async void OrderProcessWrongAmmountResult()
        {
            OrderProcessingService orderProcessingService = new OrderProcessingService();
            var order = new BillingService.Payments.Models.Order()
            {
                OrderNumber = "1",
                UserId = "1",
                PaymentGatewayId = 3,
                Amount = 0,
                Description = "test"
            };

            Task result() => orderProcessingService.ProcessOrder(order);
            await Assert.ThrowsAsync<PaymentNotProcessedException>(result);
        }
    }
}