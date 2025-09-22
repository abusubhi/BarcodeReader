using BarcodeReader.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarcodeReader.Controllers
{
    public class BarcodeCalling : Controller
    {
        private readonly AppDbContext _context;
        public BarcodeCalling(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CheckBarcode()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CheckMultiple()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckBarcode(string inputBarcode)
        {
            if (string.IsNullOrEmpty(inputBarcode))
                return BadRequest("Barcode is required");

            var exists = _context.BarcodesMaster
                                 .Any(b => b.Barcode == inputBarcode);

            var check = new BarcodeCheck
            {
                InputBarcode = inputBarcode,
                Status = exists ? "1" : "0"
            };

            _context.BarcodesCheck.Add(check);
            _context.SaveChanges();

            return Ok(check);
        }
        [HttpPost]
        public IActionResult CheckMultiple(string barcodes)
        {
            var list = barcodes.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var barcode in list)
            {
                var exists = _context.BarcodesMaster.Any(b => b.Barcode == barcode);
                var check = new BarcodeCheck
                {
                    InputBarcode = barcode,
                    Status = exists ? "1" : "0"
                };
                _context.BarcodesCheck.Add(check);
            }

            _context.SaveChanges();

            return RedirectToAction();
        }


    }

}
