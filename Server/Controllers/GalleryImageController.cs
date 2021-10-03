using BlazorV4.Server.DataBase;
using BlazorV4.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace BlazorV4.Server.Controllers
{
    [ApiController]
    [Route("GalleryImage")]
    public class GalleryImageController : ControllerBase
    {
        private static GalleryImageContext context;

        /// <summary>
        /// Получить Model по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetGalleryImageModel")]
        public GalleryImageModel GetGalleryImageModel([FromQuery] int id)
        {
            if (context == null)
                context = new GalleryImageContext();
            GalleryImageEntity entity = context.GalleryImageEntities.Find(id);

            if(entity == null) 
                return null;    

            GalleryImageModel model = new GalleryImageModel()
            {
                Name = entity.ImageName,
                PathToFileOriginal = entity.PathToOriginal,
                ThumbnailBase64 = entity.ThumbnailBase64
            };

            return model;
        }

        [HttpGet("GetGalleryImageModelByHash")]
        public GalleryImageModel GetGalleryImageModelByHash(string hash)
        {

            if (context == null)
                context = new GalleryImageContext();
            GalleryImageEntity entity = context.GalleryImageEntities.Find(hash);

            if (entity == null)
                return null;

            GalleryImageModel model = new GalleryImageModel()
            {
                Hash = hash,
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
        [HttpGet("GetImagesModels")]
        public List<GalleryImageEntity> GetImagesModels()
        {
            try
            {
                if (context == null)
                    context = new GalleryImageContext();

                var result = context.Set<GalleryImageEntity>();

                var toReturn = result == null || result.CountAsync().Result == 0
                    ? null
                    : result.Select(res => res).ToList();

                return toReturn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;

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

                      
                        stream.Position = 0;

                        GalleryImageEntity entity = new GalleryImageEntity(stream, fullPath);
                        if (context == null)
                            context = new GalleryImageContext();

                        context.GalleryImageEntities.Add(entity);
                        context.SaveChanges();
                    
                    return  Ok();
                    }
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
