﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5200Final.Models
{
    public class UploadImage
    {
        public string[] Instructions { get; set; }

     
        public string Image { get; set; }
    }
}
