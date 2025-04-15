namespace SpaceInvaders
{
    public interface ISprite
    {
        Rectangle SourceRect { get; }
        Bitmap Image { get; }
        int Width { get; }
        int Height { get; }
        void Update(double dt);
        void MakeTransparent(Color color);
    }
}