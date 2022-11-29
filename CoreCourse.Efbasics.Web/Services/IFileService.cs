namespace CoreCourse.Efbasics.Web.Services
{
    public interface IFileService
    {
        bool DeleteFileOnDisk(string fileName, string subFolder);
        Task<string> StoreFileOnDisk(IFormFile file, string subFolder);
    }
}