using ImageMagick;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;

namespace BlazorV4.Server.DataBase
{
    public class GalleryImageEntity
    {

        /// <summary>
        /// ID изображения
        /// </summary>
        [Key]
        public int ImageID { get; set; }

        /// <summary>
        /// Название изображения
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Путь к оригинальному файлу
        /// </summary>
        public string PathToOriginal { get; set; }

        /// <summary>
        /// Файл thumbnail в формате base64
        /// </summary>
        public string ThumbnailBase64 { get; set; }

        /// <summary>
        /// Hash сумма файла
        /// </summary>
        public string Hash { get; set; }

        public GalleryImageEntity(string path)
        {
            Image image = Image.Load(path);

            ImageName = Path.GetFileName(path);
            PathToOriginal = path;
            ThumbnailBase64 = image.ToBase64String(JpegFormat.Instance);
        }

        public GalleryImageEntity(FileStream stream, string fullPath)
        {
            stream.Position = 0;
            string hash = string.Concat(Array.ConvertAll(SHA256.Create().ComputeHash(stream), x => x.ToString("X2")));
            stream.Position = 0;
            MagickImage image = new MagickImage(stream);

            Hash = hash;    
            ImageName = Path.GetFileName(fullPath);
            PathToOriginal = fullPath;
            ThumbnailBase64 = $"data:image/jpeg;base64,{image.ToBase64()}";
        }

        public GalleryImageEntity()
        {

        }

    }
}
