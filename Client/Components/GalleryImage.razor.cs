using BlazorV4.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

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
        public string ImageSrc => $"GalleryImage/GetGalleryImage?path={PathToFile}";

        public string FileName { get; set; }

        public GalleryImageModel galleryImageModel;

        protected override async Task OnInitializedAsync()
        {
            FileName = await Http.GetStringAsync($"GalleryImage/GetImageName?path={PathToFile}");
        }

    }
}
