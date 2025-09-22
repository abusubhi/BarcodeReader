using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace BarcodeReader.Controllers
{
        public class ReportsController : Controller
        {
            private readonly AppDbContext _context;

            public ReportsController(AppDbContext context)
            {
                _context = context;
            }

            // Action لتحميل ملف Excel
            public IActionResult DownloadExcel()
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Barcodes");

                    // Header
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "Input Barcode";
                    worksheet.Cell(1, 3).Value = "Status";

                    // Data
                    var data = _context.BarcodesCheck.ToList();
                    int row = 2;
                    foreach (var item in data)
                    {
                        worksheet.Cell(row, 1).Value = item.Id;
                        worksheet.Cell(row, 2).Value = item.InputBarcode;
                        worksheet.Cell(row, 3).Value = item.Status;
                        row++;
                    }

                    // تحويل الملف إلى MemoryStream
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "BarcodesReport.xlsx"
                        );
                    }
                }
            }
        }
    }

        

