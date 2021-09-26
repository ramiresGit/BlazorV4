using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorV4.Client.Pages
{
    public partial class GalleryImage
    {

        /// <summary>
        /// Путь к файлу изображения
        /// </summary>
        [Parameter]
        public string PathToFile { get; set; }

        public string ImageSrc => $"GalleryImage/GetGalleryImage{PathToFile}";
    }
}
