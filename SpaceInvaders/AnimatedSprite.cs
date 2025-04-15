namespace SpaceInvaders
{
    public class AnimatedSprite : ISprite
    {
        public Bitmap Image { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private int CurrentFrame;
        private int TotalFrames;
        private float TimePerFrame;
        private float TimeSinceLastFrame;
        private int FrameWidth;
        private int FrameHeight;

        public Rectangle SourceRect { get; private set; }

        public AnimatedSprite(string path, int frameWidth, float framesPerSecond)
        {
            this.Image = new Bitmap(path);
            this.FrameWidth = frameWidth;
            this.FrameHeight = this.Image.Height;
            this.Width = this.Image.Width;
            this.Height = this.Image.Height;
            this.CurrentFrame = 0;
            this.TimePerFrame = 1f / framesPerSecond;
            this.TimeSinceLastFrame = 0f;

            this.TotalFrames = this.Image.Width / this.FrameWidth;

            this.SourceRect = new Rectangle(0, 0, this.FrameWidth, this.FrameHeight);

            this.Image.MakeTransparent(Color.FromArgb(255, 160, 16, 160));
        }

        public void MakeTransparent(Color color)
        {
            this.Image.MakeTransparent(color);
        }

        public void Update(double dt)
        {
            this.TimeSinceLastFrame += (float)dt;

            if (this.TimeSinceLastFrame >= this.TimePerFrame)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame >= this.TotalFrames)
                {
                    this.CurrentFrame = 0;
                }

                this.SourceRect = new Rectangle(this.CurrentFrame * this.FrameWidth, 0, this.FrameWidth, this.FrameHeight);

                this.TimeSinceLastFrame = 0f;
            }
        }
    }
}