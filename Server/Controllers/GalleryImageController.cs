using BlazorV4.Shared;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorV4.Server.Controllers
{
    [ApiController]
    [Route("GalleryImage")]
    public class GalleryImageController : ControllerBase
    {
        [HttpGet("GetGalleryImage")]
        public ActionResult GetGalleryImage([FromQuery] string path)
        {
            byte[] bytes;
            MemoryStream ms = new MemoryStream();

            using (Image image = Image.Load(path))
            {
                image.SaveAsJpeg(ms);
                ms.Position = 0;
                bytes = ms.ToArray();
            }

            return File(bytes, "image/jpeg");
        }

        [HttpGet("GetImageName")]
        public string GetImageName([FromQuery] string path)
        {
            return new FileInfo(path).Name;
        }

    }
}
