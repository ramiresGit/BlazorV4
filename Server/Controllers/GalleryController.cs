using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorV4.Server.Controllers
{
    [ApiController]
    [Route("Gallery")]
    public class GalleryController : ControllerBase
    {
        [HttpGet("GetFiles")]
        public List<string> GetFiles()
        {
            List<string> paths = Directory.EnumerateFiles(@"D:\Pics").ToList();
            return paths;
        }
    }
}
