using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace _5200Final
{
    public class Transform
    {
        private string[] Inst;
        private byte[] ImageData;
        private string Format;
        private int degrees;
        private int xInput;
        private int yInput;
        MagickImage Image;
        public Transform(string[] Inst, byte[] ImageData) {
            this.Inst = Inst;
            this.ImageData = ImageData;
            Image = new MagickImage(ImageData);
            AnalyzeImage();
            ExInst();
        }

        public Transform()
        {
            Image = new MagickImage();
        }

        public string TransformImage(string[] Instructions, byte[] ImageData){
            Inst = Instructions;
            this.ImageData = ImageData;
            try
            {
                Image = new MagickImage(ImageData);
            }
            catch (MagickCorruptImageErrorException)
            {
                return "Bad Image";
            }
            AnalyzeImage();
            ExInst();
            return Image.ToBase64();
        }
        
        public string getFormat()
        {
            return("data:image/" + Format + ";base64,");
        }
        private void AnalyzeImage()
        {
            MagickImageInfo info = new MagickImageInfo();
            info.Read(ImageData);
            Format = info.Format.ToString();
        }
        private void ExInst()
        {
            Console.WriteLine("InEx");
            InstHelper();
            if (Inst.Contains<string>("FlipHorizontal"))
                Image.Flop();
            if (Inst.Contains<string>("FlipVertical"))
                Image.Flip();
            if (Inst.Contains<string>("Rotate"))
                Image.Rotate((long)degrees);
            if (Inst.Contains<string>("RotateLeft"))
                Image.Rotate(-90.0);
            if (Inst.Contains<string>("RotateRight"))
                Image.Rotate(90.0);
            if (Inst.Contains<string>("GrayScale"))
                Image.Grayscale();
            if (Inst.Contains<string>("Resize"))
                Image.Resize(xInput, yInput);
            if (Inst.Contains<string>("Thumbnail"))
                Image.Thumbnail(125, 125);
        }

        private void InstHelper()
        {
           for(int i = 0; i < Inst.Length; i++)
            {
                if (Inst[i].Contains("Rotate "))
                {
                    degrees = Int32.Parse(Inst[i].Substring(Inst[i].IndexOf(":") + 1, Inst[i].Length - (Inst[i].IndexOf(":") + 1)));
                    Inst[i] = "Rotate";
                }
                if (Inst[i].Contains("Resize"))
                {
                    xInput = Int32.Parse(Inst[i].Substring(Inst[i].IndexOf(":") + 1, Inst[i].IndexOf("|") - (Inst[i].IndexOf(":") + 1)));
                    Console.WriteLine(xInput);
                    yInput = Int32.Parse(Inst[i].Substring(Inst[i].IndexOf("|") +1, Inst[i].Length - (Inst[i].IndexOf("|") + 1)));
                    Console.WriteLine(yInput);
                    Inst[i] = "Resize";
                }
            }
        }
    }
}
