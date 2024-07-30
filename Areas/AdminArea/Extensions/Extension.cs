namespace FiorelloApp.Areas.AdminArea.Extensions
{
    public static class Extension
    {
        public static bool CheckContentType(this IFormFile file, string fileType)
        {
            return file.ContentType.Contains($"{fileType}/");
        }
        public static bool CheckSize(this IFormFile file, int size)
        {
            return file.Length / 1024 > size;
        }
        public static async Task<string> SaveFile(this IFormFile file, string folderName)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", $"{folderName}", fileName);
            using FileStream fileStream = new(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return fileName;
        }

    }
}
