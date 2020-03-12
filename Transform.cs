using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;



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
            return ("data:image/" + Format + ";base64,"+ Image.ToBase64());
        }
        
        private void AnalyzeImage()
        {
            MagickImageInfo info = new MagickImageInfo();
            info.Read(ImageData);
            Format = info.Format.ToString();
        }
        private void ExInst()
        {
            InstHelper();
            foreach (string method in Inst) {
                if (method.Equals("FlipHorizontal"))
                    Image.Flop();
                if (method.Equals("FlipVertical"))
                    Image.Flip();
                if (method.Equals("Rotate"))
                    Image.Rotate((long)degrees);
                if (method.Equals("RotateLeft"))
                    Image.Rotate(-90.0);
                if (method.Equals("RotateRight"))
                    Image.Rotate(90.0);
                if (method.Equals("GrayScale"))
                    Image.Grayscale();
                if (method.Equals("Resize"))
                    Image.Resize(xInput, yInput);
                if (method.Equals("Thumbnail"))
                    Image.Thumbnail(125, 125);
            }
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
