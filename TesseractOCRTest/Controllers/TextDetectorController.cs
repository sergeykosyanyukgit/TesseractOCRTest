using Microsoft.AspNetCore.Mvc;
using TesseractOCRTest.Interfaces;
using TesseractOCRTest.Requests;

namespace TesseractOCRTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextDetectorController : ControllerBase
    {
        private readonly ITextDetector _textDetector;
        public TextDetectorController(ITextDetector textDetector)
        {
            _textDetector = textDetector;
        }

        [HttpPost("Detect/{lang}")]
        public async Task<JsonResult> Detect(IFormFile formFile, string lang)
        {
            var result = await _textDetector.DetectImage(new DetectRequest
            {
                formFile = formFile,
                lang = lang
            });
            return new JsonResult(result);
        }
    }
}
