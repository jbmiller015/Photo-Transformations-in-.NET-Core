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

        //Overloaded Consturctor
        //Not used in this version
        /*public Transform(string[] Inst, byte[] ImageData) {
            this.Inst = Inst;
            this.ImageData = ImageData;
            Image = new MagickImage(ImageData);
            AnalyzeImage();
            ExInst();
        }*/

        /**
         * Drives Transformations
         */
        public string TransformImage(string[] Instructions, string imageString){
            Inst = Instructions;
            Image = new MagickImage();
            ImageData = cleanImage(imageString);
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
        
        private byte[] cleanImage(string image)
        {
            if (image.Contains(','))
                image = image.Substring(image.IndexOf(",") + 1, image.Length - (image.IndexOf(",") + 1));
            return Convert.FromBase64String(image);
        }
        /**
         * Get Image Type.
         */
        private void AnalyzeImage()
        {
            MagickImageInfo info = new MagickImageInfo();
            info.Read(ImageData);
            Format = info.Format.ToString().ToLower();
        }
        
        /**
         * Call Methods associated with instructions in list
         */
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

        /**
         * Pulls specifying information out of Rotate and Resize
         */
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
