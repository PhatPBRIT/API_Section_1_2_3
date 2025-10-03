using System.Collections.Generic;
using BookStoreApi.Models.Domain;

public interface IImageRepository
{
    Image Upload(Image image);

    List<Image> GetAllInfoImages();

    (byte[], string, string ) DownloadFile(int Id);
}
