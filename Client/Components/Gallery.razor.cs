using BlazorV4.Shared;
using BlazorV4.Shared.Project;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorV4.Client.Components
{
    public partial class Gallery : ComponentBase
    {
        public List<int> FilesCount { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            string result = await Http.GetStringAsync("GalleryImageDTOs/GetAssureDB");
            FilesCount = await Http.GetFromJsonAsync<List<int>>("GalleryImage/GetImagesIds?");
            
        }

        protected async Task<GalleryImageModel> GetModelFromDb(int id)
        {
            GalleryImageModel model = await Http.GetFromJsonAsync<GalleryImageModel>($"GalleryImage/GetGalleryImageModel?={id}");
            return model;
        }
        /// <summary>
        /// Загрузить изображение
        /// </summary>
        /// <param name="e"></param>
        private async void UploadFiles(InputFileChangeEventArgs e)
        {
            var entries = e.GetMultipleFiles();
            if (entries == null || entries.Count == 0)
                return;

            foreach (var entry in entries)
            {
                string name = entry.Name;
                string pathToSave = $@"{BaseProjectSettings.ProjectPath}\{Guid.NewGuid()}\{name}";

                var resizedFile = await entry.RequestImageFileAsync("image/png", 300, 500);

                using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                {
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), pathToSave, name);
                    await Http.PostAsync("GalleryImage/AddImage", content);

                }
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopRight;
                Snackbar.Add($"{entries.FirstOrDefault().Name} added", Severity.Info);
            }


        }

        private async void CreateDB()
        {
            HttpClient client = new HttpClient();
        }
    }
}
