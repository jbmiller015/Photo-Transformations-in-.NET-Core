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

namespace _5200Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // POST: api/Photo
        [HttpPost]
        public FileContentResult UploadImage([FromBody] UploadImage Request)
        { 
            Transform photoTransformation = new Transform(Request.Instructions, Request.Image);
            return File(photoTransformation.getImage(), "image/" + photoTransformation.getFormat());
        }

        // PUT: api/Photo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
