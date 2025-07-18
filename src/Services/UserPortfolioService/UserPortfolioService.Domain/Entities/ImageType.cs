using Shared.DataAccess.Common;

namespace UserPortfolioService.Domain.Entities;

public class ImageType
{
    public ImageType(string fileName, string contentType, byte[] data)
    {
        FileName = fileName;
        ContentType = contentType;
        Data = data;
    }

    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public bool IsValidSize => Data?.Length <= 10 * 1024 * 1024;
}