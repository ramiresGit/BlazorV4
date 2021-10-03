using BlazorV4.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Threading;

namespace BlazorV4.Client.Components
{
    public partial class GalleryImage
    {

        /// <summary>
        /// Путь к файлу изображения
        /// </summary>
        [Parameter]
        public string PathToFile 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Источник изображения
        /// </summary>
        public string ImageSrc => GalleryImageModel?.ThumbnailBase64;

        public string FileName { get; set; }

        [Parameter]
        public GalleryImageModel GalleryImageModel { get; set; }

        public GalleryImage()
        {

           
        }

    }
}
