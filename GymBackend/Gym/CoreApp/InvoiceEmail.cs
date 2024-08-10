using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DataAccess.CRUD;
using DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;

public class InvoiceEmailSender
{
    private readonly string _apiKey;

    public InvoiceEmailSender()
    {
        _apiKey = "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";
    }

    public async Task SendInvoiceEmailAsync(Invoice invoice, List<Detail> details)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        // Generate PDF content using QuestPDF
        var pdfContent = GeneratePdf(invoice, details);
        var xmlContent = GenerateXml(invoice, details);

        // Send email with attachments
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("siluetaclubfitness@gmail.com", "Silueta Club Fitness");
        var to = new EmailAddress("lgarmanyv@ucenfotec.ac.cr");
        var subject = "Your Invoice";
        var plainTextContent = "Please find your invoice attached.";
        var htmlContent = "<strong>Please find your invoice attached.</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        // Attach PDF
        var pdfAttachment = new Attachment
        {
            Content = Convert.ToBase64String(pdfContent),
            Type = "application/pdf",
            Filename = "Invoice.pdf",
            Disposition = "attachment"
        };
        msg.AddAttachment(pdfAttachment);

        // Attach XML
        var xmlAttachment = new Attachment
        {
            Content = Convert.ToBase64String(xmlContent),
            Type = "application/xml",
            Filename = "Invoice.xml",
            Disposition = "attachment"
        };
        msg.AddAttachment(xmlAttachment);

        // Send the email
        try
        {
            var response = await client.SendEmailAsync(msg);
            // Handle response if needed
        }
        catch (Exception ex)
        {
            // Handle exceptions (log, notify, etc.)
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

    private byte[] GeneratePdf(Invoice invoice, List<Detail> details)
    {
        var pdfDocument = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Header().Text($"Invoice for User {invoice.UserId}").FontSize(20).Bold().AlignCenter();
                page.Content().Element(content =>
                {
                    BuildTable(invoice, details, content);
                });
                page.Footer().AlignCenter().Text($"Generated on {DateTime.Now:yyyy-MM-dd}");
            });
        });

        using (var memoryStream = new MemoryStream())
        {
             pdfDocument.GeneratePdf(memoryStream);
            return memoryStream.ToArray();
        }
    }


    private void BuildTable(Invoice invoice, List<Detail> details, IContainer container)
    {
        container.Table(table =>
        {
            // Define columns
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(4);
                columns.RelativeColumn(2);
            });

            // Header
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Item");
                header.Cell().Element(CellStyle).AlignRight().Text("Price");
            });

            // Details
            foreach (var detail in details)
            {
                if (detail.UserMembershipId != 0) {

                    var mCrud = new MembershipCrudFactory();

                    var Membership = mCrud.RetrieveById <Membership> (detail.UserMembershipId);

                    table.Cell().Element(CellStyle).Text("Tipo de Membresia:" + $"{Membership.Type}");
                }

                else
                {
                    var ptCrud = new PersonalTrainingCrudFactory();
                    var pTraining = ptCrud.RetrieveById<PersonalTraining>(detail.PersonalTrainingId);
                    table.Cell().Element(CellStyle).Text("Entrenamiento Personal con Entrenador:" + $"{pTraining.EmployeeName}" + "el dia" + $"{pTraining.ProgrammedDate}");
                }
                
                table.Cell().Element(CellStyle).AlignRight().Text($"{detail.Price:C}");
            }

            // Totals
            table.Cell().Element(CellStyle).Text("Total (Before Discount)");
            table.Cell().Element(CellStyle).AlignRight().Text($"{invoice.Amount:C}");

            table.Cell().Element(CellStyle).Text("Total (After Discount)");
            table.Cell().Element(CellStyle).AlignRight().Text($"{invoice.AmountAfterDiscount:C}");

            table.Cell().Element(CellStyle).Text("Payment Method");
            table.Cell().Element(CellStyle).AlignRight().Text(invoice.PaymentMethod);
        });
    }


    private IContainer CellStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .Padding(5)
            .Background(Colors.White)
            .AlignMiddle()
            .DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
    }

    private byte[] GenerateXml(Invoice invoice, List<Detail> details)
    {
        using (var memoryStream = new MemoryStream())
        {
            var serializer = new XmlSerializer(typeof(Invoice));
            serializer.Serialize(memoryStream, invoice);

            foreach (var detail in details)
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                serializer = new XmlSerializer(typeof(Detail));
                serializer.Serialize(memoryStream, detail);
            }

            return memoryStream.ToArray();
        }
    }
}
