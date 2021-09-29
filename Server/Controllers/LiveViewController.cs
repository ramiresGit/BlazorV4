using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorV4.Server.Controllers
{
    [ApiController]
    [Route("LiveView")]
    public class LiveViewController : Controller
    {
        private static int _id = 0;

        [HttpGet("GetLiveView")]
        public ActionResult GetLiveView([FromQuery] string path)
        {
            byte[] bytes;
            MemoryStream ms = new MemoryStream();

            using (Image image = Image.Load($@"D:\Blazor\Gif\{_id}.gif"))
            {
                image.SaveAsJpeg(ms);
                ms.Position = 0;
                bytes = ms.ToArray();

            }
            _id = (_id + 1) % 105;

            return File(bytes, "image/jpeg");
        }
    }
}
