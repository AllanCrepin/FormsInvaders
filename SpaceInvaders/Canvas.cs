using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Canvas
    {
        public Bitmap Image { get; set; }
        public Graphics GraphicsObject { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Canvas(int width, int height, PixelFormat pixelFormat)
        {
            this.Image = new Bitmap(width, height, pixelFormat);
            this.GraphicsObject = Graphics.FromImage(this.Image);

            this.GraphicsObject.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            // Without this, clips part of the left-most pixels when scaling the bmp up.
            // Pixel Offset Mode: You have PixelOffsetMode.None, which means no adjustment is made to
            // pixel positioning. While this should normally be fine, the combination of scaling and
            // possible pixel rounding could still cause an off-by-one-pixel issue. Experimenting with
            // PixelOffsetMode.Half might help by aligning the pixels better when scaling.


            // Set the interpolation mode to smooth the scaling
            this.GraphicsObject.InterpolationMode = InterpolationMode.NearestNeighbor;

            this.Width = width;
            this.Height = height;
        }

        public void Clear(Color color)
        {
            GraphicsObject.Clear(color);
        }

        public void Draw(Sprite sprite, float x, float y)
        {
            GraphicsObject.DrawImage(sprite.Image, x, y, sprite.Width, sprite.Height);
        }
    }
}
