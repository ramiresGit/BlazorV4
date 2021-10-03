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
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlazorV4.Client.Components
{
    public partial class Gallery : ComponentBase
    {
        public static List<GalleryImageModel> FilesCount { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            string result = await Http.GetStringAsync("GalleryImageDTOs/GetAssureDB");
            FilesCount = await Http.GetFromJsonAsync<List<GalleryImageModel>>("GalleryImage/GetImagesModels?");
            
        }

        protected async Task<GalleryImageModel> GetModelFromDb(int id)
        {
            return await Http.GetFromJsonAsync<GalleryImageModel>($"GalleryImage/GetGalleryImageModel?id={id}");
           
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
                string pathToSave = $@"{BaseProjectSettings.ProjectPath}{Guid.NewGuid()}\{name}";

                var resizedFile = await entry.RequestImageFileAsync("image/png", 10000, 10000);

                using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                {
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), pathToSave, name);
                    var hashBytes =  await Http.PostAsync("GalleryImage/AddImage", content);
                }
                FilesCount = await Http.GetFromJsonAsync<List<GalleryImageModel>>("GalleryImage/GetImagesModels?");
                StateHasChanged();
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopRight;
                Snackbar.Add($"{entries.FirstOrDefault().Name} added", Severity.Info);

                
                
                StateHasChanged();
            }


        }

        private async void CreateDB()
        {
            HttpClient client = new HttpClient();
        }
    }
}
