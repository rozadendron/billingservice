﻿using XYZ.BillingService.Payments.Models;

namespace XYZ.BillingService.Payments.Interfaces
{
    public interface IPaymentGateway
    {
        public Task<Receipt> ProcessPayment(Order orderToProcess);
    }
}
