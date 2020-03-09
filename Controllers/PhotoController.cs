using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _5200Final.Models;
using _5200Final;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImageMagick;
using System.Net.Mime;

namespace _5200Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        public ViewResult Index() => View();

        // POST: api/Photo
        [HttpPost]
        [Route("api/[controller]/UploadImage")]
        [Consumes(MediaTypeNames.Application.Json)]
        public FileContentResult UploadImage([FromBody] UploadImage Request)
        { 
            Transform photoTransformation = new Transform(Request.Instructions, Request.Image);
            return File(photoTransformation.getImage(), "image/" + photoTransformation.getFormat());
        }
    }
}
