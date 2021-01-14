namespace Campari
{
    public struct Rectangle
    {
        public int X { get; }
        public int Y { get; }
        public int W { get; }
        public int H { get; }

        public Rectangle(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }
    }
}
