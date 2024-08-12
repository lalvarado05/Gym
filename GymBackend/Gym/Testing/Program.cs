
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.CRUD;

using DTOs;

public class InvoiceEmailSenderTest
{
    public static async Task Main(string[] args)
    {

        // Step 1: Create a sample invoice
        var invoice = new Invoice
        {
            UserId = 123,
            Amount = 100.00,
            AmountAfterDiscount = 90.00,
            PaymentMethod = "Credit Card"
        };

        // Step 2: Create a list of sample details
        var details = new List<Detail>
        {
            new Detail
            {
                UserMembershipId = 1,
                PersonalTrainingId = 0,
                Price = 50.00
            },
            new Detail
            {
                UserMembershipId = 0,
                PersonalTrainingId = 2,
                Price = 40.00
            }
        };

        // Step 3: Instantiate the InvoiceEmailSender
        var emailSender = new InvoiceEmailSender();

        // Step 4: Send the invoice email asynchronously
        await emailSender.SendInvoiceEmailAsync(invoice, details);

        // Step 5: Confirm email was sent (you can check the email inbox to verify)
        Console.WriteLine("Invoice email sent. Please check your inbox.");

    }
}
