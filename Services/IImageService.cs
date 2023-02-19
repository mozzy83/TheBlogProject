﻿namespace TheBlogProject.Services
{
    public interface IImageService
    {
        Task<byte[]> EncodeImageAsync(IFormFile file);
        Task<byte[]> EncodeImageAsync(string fileName);
        string DecodeImageAsync(byte[] data, string type);
        string ContentType(IFormFile file);
        int Size(IFormFile file);
    }
}
