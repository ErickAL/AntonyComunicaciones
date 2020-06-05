using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace App.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder, string fileName);

        string UploadImage(byte[] pictureArray, string folder, string fileName);
    }
}