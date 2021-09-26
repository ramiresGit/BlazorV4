using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorV4.Server.Controllers
{
    [ApiController]
    public class GalleryController : ControllerBase
    {
        [HttpGet("GetFiles")]
        public List<string> GetFiles([FromQuery] string path)
        {
            return Directory.EnumerateFiles(path).ToList();
        }
    }
}
