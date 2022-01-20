namespace StoreSolution.Data.Common
{
    public interface IStorageService
    {
        string GetFileUrl(string filePath);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);
    }
}
