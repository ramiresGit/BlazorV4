using ImageMagick;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorV4.Shared
{
    public class GalleryImageModel
    {


        /// <summary>
        /// Название изображения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь к оригинальному изображению
        /// </summary>
        public string PathToFileOriginal { get; set; }

        /// <summary>
        /// Thumbnail в формате base64
        /// </summary>
        public string ThumbnailBase64 { get; set; }

        /// <summary>
        /// Размер изображения
        /// </summary>
        public double FileSizeKb { get; set; }

        /// <summary>
        /// Ширина изображения в пикселях
        /// </summary>
        public int PixelWidth { get; set; }

        /// <summary>
        /// Высота изображения в пикселях
        /// </summary>
        public int PixelHeight { get; set; }

        private static HttpClient client = new HttpClient();

        public GalleryImageModel(int Id)
        {
            GetEntity(Id).Wait();

        }

        public GalleryImageModel()
        {

        }

        public GalleryImageModel(MemoryStream ms, string fileName)
        {
            CreateEntity(ms, fileName).Wait();
        }

        private async Task GetEntity(int entityId)
        {

        }

        public async Task CreateEntity(MemoryStream ms, string fileName)
        {
            Image image = Image.Load(ms);
        }

    }
}
