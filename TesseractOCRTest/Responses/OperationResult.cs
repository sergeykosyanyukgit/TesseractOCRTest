using TesseractOCRTest.Enums;

namespace TesseractOCRTest.Responses
{
    public record OperationResult
    {
        public OperationResult(ErrorCode code, string description)
        {
            Code = code;
            Description = description;
        }

        public ErrorCode Code { get; set; }
        public string Description { get; set; }
    }
}
