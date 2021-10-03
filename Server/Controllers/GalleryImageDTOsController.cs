using BlazorV4.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorV4.Server.Controllers
{
   
    [ApiController]
    [Route("GalleryImageDTOs")]
    public class GalleryImageDTOsController : ControllerBase
    {
        private static GalleryImageContext context;

        /// <summary>
        /// Создать базу данных и выполнить миграцию
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAssureDB")]
        public string GetAssureDB()
        {
            try
            {
                if(context == null)
                    context = new GalleryImageContext();
                return "got";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return "shit";

            } 
        }


    }
}
