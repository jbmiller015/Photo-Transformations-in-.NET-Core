using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _5200Final
{
    public class Transform
    {
        private string[] Inst;
        private byte[] ImageData;
        private string Format;
        MagickImage Image;
        public Transform(string[] Inst, byte[] ImageData) {
            this.Inst = Inst;
            //Not sure if this is needed
            this.ImageData = ImageData;
            this.Image = new MagickImage(ImageData);
            AnalyzeImage();
            ExInst();
        }
        public byte[] getImage()
        {
            return Image.ToByteArray();
        }

        public string getFormat()
        {
            return Format;
        }
        private void AnalyzeImage()
        {
            MagickImageInfo info = new MagickImageInfo();
            info.Read(ImageData);
            Format = info.Format.ToString();
        }
        private void ExInst()
        {
            if (Inst.Contains<string>("FlipHorizontal") || Inst.Contains<string>("FlipVertical"))
                Flip();
            if (Inst.Contains<string>("Rotate"))
                Rotate();
            if (Inst.Contains<string>("GrayScale"))
                GrayScale();
            if (Inst.Contains<string>("Resize"))
                Resize();
            if (Inst.Contains<string>("Thumbnail"))
                Thumbnail();
        }

        private void Thumbnail()
        {
            Image.Thumbnail(200,200);
        }

        private void Resize()
        {
            throw new NotImplementedException();
        }

        private void GrayScale()
        {
            Image.Grayscale();
        }

        private void Rotate()
        {
            if(Inst.Contains<string>("Left"))
                Image.Rotate(-90.0);
            if (Inst.Contains<string>("Right"))
                Image.Rotate(90.0);
            //else if (Inst.Contains<string>("Rotate {0}", dgr)
                    Image.Rotate(-90.0);
            //throw new NotImplementedException();
        }

        private void Flip()
        {
           if(Inst.Contains<string>("FlipVertical"))
                Image.Flip();
           if(Inst.Contains<string>("FlipHorizontal"))
                Image.Flop();
        }
    }
}
