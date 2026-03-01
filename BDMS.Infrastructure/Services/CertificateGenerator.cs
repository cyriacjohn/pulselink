using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BDMS.Infrastructure.Services
{
    public class CertificateGenerator
    {
        public byte[] Generate(string donorName, string hospitalName, string certificateNumber)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);

                    page.Content().Column(column =>
                    {
                        column.Spacing(20);
                        column.Item().Text("Blood Donation Certificate")
                                                                        .FontSize(24)
                                                                        .Bold()
                                                                        .AlignCenter();
                        column.Item().Text($"Certificate No: {certificateNumber}")
                                                                        .FontSize(14);
                        column.Item().Text($"This certifies that {donorName}")
                                                                        .FontSize(16);
                        column.Item().Text($"has successfully donated blood at {hospitalName}")
                                                                         .FontSize(16);
                        column.Item().Text($"Date: {DateTime.Now:dd MM yyyy}")
                                                                         .FontSize(14);
                        column.Item().Text("Thank you for saving lives.")
                                                                         .FontSize(14)
                                                                         .AlignCenter();
                    });
                });
            }).GeneratePdf();
        }
    }
}
