namespace TesseractOCRTest.Requests
{
    public record DetectRequest
    {
        public IFormFile formFile { get; set; }
        public string lang { get; set; }
    }
}
