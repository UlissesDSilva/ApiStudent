using StudentAdminPortal.API.Data.IRepository;

namespace StudentAdminPortal.API.Data.Repository
{
    public class LocalStorageImageRepository : IRepositoryImage
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);

            return GetRelativePath(fileName);
        }

        public string GetRelativePath(string fileName) {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}