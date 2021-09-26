﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorV4.Client.Components
{
    public partial class GalleryComponent
    {
        public List<string> Files { get; set; }

        protected override async Task OnInitializedAsync()
        {   
            Files = await Http.GetFromJsonAsync<List<string>>("Gallery/GetFiles");
        }
    }
}