using BlazorV4.Server.DataBase;
using BlazorV4.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace BlazorV4.Server.Controllers
{
    [ApiController]
    [Route("GalleryImage")]
    public class GalleryImageController : ControllerBase
    {
        private static GalleryImageContext context;


        [HttpGet("GetGalleryImageModel")]
        public GalleryImageModel GetGalleryImageModel([FromQuery] int id)
        {
            if (context == null)
                context = new GalleryImageContext();
            GalleryImageEntity entity = context.GalleryImageEntities.Find(id);

            GalleryImageModel model = new GalleryImageModel()
            {
                Name = entity.ImageName,
                PathToFileOriginal = entity.PathToOriginal,
                ThumbnailBase64 = entity.ThumbnailBase64
            };

            return model;
        }

        /// <summary>
        /// Получить все записи из таблицы GalleryImageEntity
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetImagesIds")]
        public List<int> GetImagesIds()
        {
            try
            {
                if (context == null)
                    context = new GalleryImageContext();

                var result = context.Set<GalleryImageEntity>();

                var toReturn = result == null || result.CountAsync().Result == 0
                    ? new List<int>()
                    : result.Select(res => res.ImageID).ToList();

                return toReturn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<int>();

            }
        }

        [HttpPost("AddImage")]
        public IActionResult AddImage()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('\"');

                    var rootDirectory = Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        GalleryImageEntity entity = new GalleryImageEntity(stream, fullPath);
                        if (context == null)
                            context = new GalleryImageContext();

                        context.GalleryImageEntities.Add(entity);
                        context.SaveChanges();
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetImageName")]
        public string GetImageName([FromQuery] string path)
        {
            return new FileInfo(path).Name;
        }

    }
}
