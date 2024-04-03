namespace BookinKanAPI.ServicesManage.ImageServiceManage
{
    public interface IImageCarsService
    {

        bool IsUpload(IFormFileCollection formFiles);
        string Validation(IFormFileCollection formFiles);
        Task<List<string>> UploadImages(IFormFileCollection formFiles);
        Task DeleteFileImages(List<string> files);
        Task<List<string>> UploadImageswithPath(IFormFileCollection formFiles, string uploadPath);
    }
}
