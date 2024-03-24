using Tesseract;
using TesseractOCRTest.Enums;
using TesseractOCRTest.Interfaces;
using TesseractOCRTest.Requests;
using TesseractOCRTest.Responses;
using TesseractOCRTest.Helpers;

namespace TesseractOCRTest.Services
{
    public class TextDetectorService : ITextDetector
    {

        private readonly string tessDataPath;
        private readonly List<string> tessSupportedLang;

        public TextDetectorService()
        {
            tessDataPath = Path.Combine(Environment.CurrentDirectory, ServiceHelper.TrainDataFolderName);
            tessSupportedLang = new();
            if (!Directory.Exists(tessDataPath))
            {
                return;
            }
            tessSupportedLang = Directory
                .GetFiles(tessDataPath)
                .Select(path => 
                    Path.GetFileName(path)
                    .Split(".")
                    .First())
                .ToList();
        }

        public async Task<BaseResponse> DetectImage(DetectRequest detectRequest)
        {
            try
            {
                if (detectRequest.formFile is null)
                {
                    return new BaseResponse(new OperationResult(ErrorCode.NoFileError, "File Error"));
                }
                if (!tessSupportedLang.Contains(detectRequest.lang))
                {
                    return new BaseResponse(new OperationResult(ErrorCode.EnterLangError, "Lang Error"));
                }

                var ms = new MemoryStream();
                await detectRequest.formFile.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var engine = new TesseractEngine(tessDataPath, detectRequest.lang);
                var image = Pix.LoadFromMemory(ms.ToArray());
                var text = engine.Process(image).GetText();

                engine.Dispose();
                image.Dispose();
                ms.Dispose();
                return new BaseResponse(new OperationResult(ErrorCode.Success, text));
            }
            catch (Exception ex)
            {
                return new BaseResponse(new OperationResult(ErrorCode.UnsupportedError, ex.Message));
            }
        }
    }
}
