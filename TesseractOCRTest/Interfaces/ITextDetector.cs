using TesseractOCRTest.Requests;
using TesseractOCRTest.Responses;

namespace TesseractOCRTest.Interfaces
{
    public interface ITextDetector
    {
        Task<BaseResponse> DetectImage(DetectRequest detectRequest);
    }
}
