using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _5200Final.Models;
using _5200Final;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace _5200Final.Controllers
{
    [Route("api/Photo")]
    [ApiController]
    public class PhotoController : Controller
    {
        [HttpGet]
        public ViewResult Index() => View();

        // POST: api/Photo
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public IActionResult UploadImage([FromBody] UploadImage Request)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(Request.ToString());
                Transform photoTransformation = new Transform(Request.Instructions, Request.Image);
                return File(photoTransformation.getImage(), "image/" + photoTransformation.getFormat());
            }
            else return BadRequest();
        }
    }
}
