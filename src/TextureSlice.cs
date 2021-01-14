namespace Campari
{
    public struct TextureSlice
    {
        public Texture Texture { get; }
        public Rectangle Rectangle { get;  }
        public uint Depth { get; }
        public uint Layer { get; }
        public uint Level { get; }

        public TextureSlice(Texture texture)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, (int) texture.Width, (int) texture.Height);
            Depth = 0;
            Layer = 0;
            Level = 0;
        }

        public TextureSlice(Texture texture, Rectangle rectangle, uint depth = 0, uint layer = 0, uint level = 0)
        {
            Texture = texture;
            Rectangle = rectangle;
            Depth = depth;
            Layer = layer;
            Level = level;
        }

        public RefreshCS.Refresh.TextureSlice ToRefreshTextureSlice()
        {
            RefreshCS.Refresh.TextureSlice textureSlice = new RefreshCS.Refresh.TextureSlice
            {
                texture = Texture.Handle,
                rectangle = new RefreshCS.Refresh.Rect
                {
                    x = Rectangle.X, 
                    y = Rectangle.Y, 
                    w = Rectangle.W,
                    h = Rectangle.H
                },
                depth = Depth,
                layer = Layer,
                level = Level
            };

            return textureSlice;
        }
    }
}
