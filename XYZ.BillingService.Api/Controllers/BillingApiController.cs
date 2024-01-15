using Microsoft.AspNetCore.Mvc;
using XYZ.BillingService.Orders.Errors;
using XYZ.BillingService.Orders.Interfaces;
using XYZ.BillingService.Payments.Models;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillingApiController(ILogger<BillingApiController> logger, IOrderProcessingService orderProcessingService) : ControllerBase
    {
        private const string PaymentNotProcessedErrorMessage = "Payment not processed. OrderNumber:";
        private const string OrderProcessingReturnedEmptyOrderMessage = "Order processing returned empty order. OrderNumber:";
        private const string ErrorOccuredMessage = "Error occuried during Order processing. OrderNumber:";
        private const string RecievedOrderMessage = "Recieved order, starting processing. OrderNumber:";

        [HttpPost("v1/orders/process")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Process(Order order)
        {
            if (order == null)
            {
                logger.Log(LogLevel.Warning, "Recieved empty order");
                throw new ArgumentNullException(nameof(order));
            }

            logger.Log(LogLevel.Information, $"{RecievedOrderMessage}{order.OrderNumber}");

            try
            {
                var receipt = await orderProcessingService.ProcessOrder(order);

                if (receipt == null)
                {
                    logger.Log(LogLevel.Warning, $"{OrderProcessingReturnedEmptyOrderMessage}{order.OrderNumber}");
                    return Problem($"Order processing service returned null {order.OrderNumber}");
                }
                else
                {
                    logger.Log(LogLevel.Warning, "Order processed sucessfully.");
                    return Ok(receipt);
                }
            }
            catch (PaymentNotProcessedException pnpException)
            {
                logger.Log(LogLevel.Error, pnpException, PaymentNotProcessedErrorMessage + order.OrderNumber);
                return Problem("Payment not processed.");
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ErrorOccuredMessage + order.OrderNumber);
                return Problem("Error occured.");
            }
        }
    }
}
