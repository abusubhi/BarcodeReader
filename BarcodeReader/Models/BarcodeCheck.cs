namespace BarcodeReader.Models
{
    public class BarcodeCheck
    {
        public int Id { get; set; }
        public string InputBarcode { get; set; }
        public string Status { get; set; } // "1", "0", or null
    }
}
