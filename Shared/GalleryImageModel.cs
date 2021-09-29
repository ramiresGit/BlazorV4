using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorV4.Shared
{
    public class GalleryImageModel
    {
        private Image _thumbnail;

        /// <summary>
        /// Название изображения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь к оригинальному изображению
        /// </summary>
        public string PathToFileOriginal { get; set; }

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

    }
}
