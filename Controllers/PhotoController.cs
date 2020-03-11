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
        //[HttpGet]
        //public ViewResult Index() => View();

        // POST: api/Photo
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public string UploadImage([FromBody] UploadImage Request)
        {
            if(Request.Image.Contains(',')) Request.Image = Request.Image.Substring(Request.Image.IndexOf(",") + 1, Request.Image.Length - (Request.Image.IndexOf(",") + 1));
            var imageDataByteArray = Convert.FromBase64String(Request.Image);
            Console.WriteLine("In Upload");
            if (ModelState.IsValid)
            {
                Console.WriteLine(Request.ToString());
                Transform photoTransformation = new Transform(Request.Instructions, imageDataByteArray);
                Console.WriteLine(photoTransformation.getFormat());
                string base64ImageRepresentation = Convert.ToBase64String(photoTransformation.getImage());
                return("{data:image/" + photoTransformation.getFormat().ToLower() + ";charset_utf-8;base64, " +base64ImageRepresentation+"}");
            }
            else return "Bad Request";
        }
    }
}
