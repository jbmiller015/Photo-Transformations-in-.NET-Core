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
        private Transform photoTransformation = new Transform();

        // POST: api/Photo
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(400)]
        public IActionResult UploadImage([FromBody] UploadImage Request)
        {
            if (Request.Image != null && Request.Instructions != null)
            {
                var result = (photoTransformation.TransformImage(Request.Instructions, Request.Image));
                if(result == "Bad Image")
                {
                    return BadRequest("BadImage");
                }
                return Ok(result);
                
            }
            else return BadRequest("No Image Detected");
        }
        
    }
}
