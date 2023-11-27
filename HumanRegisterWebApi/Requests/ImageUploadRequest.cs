using HumanRegisterWebApi.Attributes;

namespace HumanRegisterWebApi.Requests
{
    public class ImageUploadRequest
    {
        [MaxFileSizeAttribute(5*1024*1024)]
        [AllowedExtensions(".JPG", ".JPEG", ".PNG", ".GIF", ".TIFF", ".BMP", ".RAW", ".SVG", ".WEBP", ".HEIF", ".PSD")]
        public IFormFile Image {  get; set; }
    }
}
