namespace StudentAdminPortal.API.Data.IRepository
{
    public interface IRepositoryImage
    {
        Task<string> Upload(IFormFile image, string fileName);
    }
}