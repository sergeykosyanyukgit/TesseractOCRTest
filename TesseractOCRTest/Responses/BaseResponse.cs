using TesseractOCRTest.Enums;

namespace TesseractOCRTest.Responses
{
    public class BaseResponse
    {
        public BaseResponse(OperationResult result)
        {
            Result = result;
        }

        public BaseResponse(ErrorCode errorCode)
        {
            if (errorCode != ErrorCode.Success)
            {
                throw new ArgumentException("Invalid error code");
            }

            Result = new OperationResult(ErrorCode.Success, "");
        }

        public OperationResult Result { get; set; }
    }
}
