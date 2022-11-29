using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace CoreCourse.Efbasics.Web.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> StoreFileOnDisk(IFormFile file, string subFolder)
        {
            //store the file and return unique filename
            //create unique filename
            var fileName = $"{Guid.NewGuid()}" +
                $"_{file.FileName}";
            //create the path to store the file
            var pathToFile = Path
                .Combine(_webHostEnvironment.WebRootPath, subFolder, fileName);
            //create a filestream and copy file
            using (var fileStream = new FileStream(pathToFile
                , FileMode.Create))
            {
                //copy file to disk
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public bool DeleteFileOnDisk(string fileName, string subFolder)
        {
            //delete file on disk, true on success
            //delete it
            //delete old image
            var pathToDelete = Path
                .Combine(_webHostEnvironment.WebRootPath,
                subFolder, fileName);
            try
            {
                System.IO.File.Delete(pathToDelete);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Console.WriteLine(fileNotFoundException.Message);
                return false;
            }
            return true;
        }
    }
}
