using Microsoft.Extensions.Hosting.Internal;

namespace MuseumManagementSystem.Web.ExtensionMethods
{
    public static class FileHelper
    {
        //public static async Task<string> UploadImage(this IFormFile file, string folderPath, string fileName, IWebHostEnvironment hostingEnvironment)
        //{
        //    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
        //    string uniqueFileName = $"{fileName}_{DateTime.Now.ToString("yymmssfff")}{Path.GetExtension(file.FileName)}";
        //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //    string envPath = $"/{folderPath}/" + uniqueFileName;
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //    return envPath;
        //}
        public static async Task<string> UploadImage(this IFormFile file, string folderPath, string fileName, IWebHostEnvironment hostingEnvironment, string? existingRelativePath = null)
        {
            // Remove existing image if provided
            if (!string.IsNullOrEmpty(existingRelativePath))
            {
                string existingPath = Path.Combine(hostingEnvironment.WebRootPath, existingRelativePath);
                if (File.Exists(existingPath))
                {
                    File.Delete(existingPath);
                }
            }

            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
            string uniqueFileName = $"{fileName}_{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Get the relative path from the web root
            string relativePath = Path.Combine(folderPath, uniqueFileName);

            return relativePath;
        }

        public static bool IsFileOpen(this FileInfo file)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
            return false;
        }

        public static bool DeleteImage(string existingRelativePath, IWebHostEnvironment hostingEnvironment)
        {
            string existingPath = Path.Combine(hostingEnvironment.WebRootPath, existingRelativePath.Replace('/', '\\'));
            if (File.Exists(existingPath))
            {
                File.Delete(existingPath);
                return true;
            }
            return false;
        }
    }
}
