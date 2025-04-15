namespace SpaceInvaders
{
    public class Sprite : ISprite
    {
        public Rectangle SourceRect {  get; private set; }
        public Bitmap Image { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Sprite(string path)
        {
            this.Image = new Bitmap(path);
            this.Width = this.Image.Width;
            this.Height = this.Image.Height;

            this.SourceRect = new Rectangle(0,0,Width, Height);

            this.Image.MakeTransparent(Color.FromArgb(255, 160, 16, 160));
        }

        public void MakeTransparent(Color color)
        {
            this.Image.MakeTransparent(color);
        }

        public void Update(double dt)
        {
        }
    }
}