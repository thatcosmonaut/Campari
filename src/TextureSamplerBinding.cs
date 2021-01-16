namespace Campari
{
    public struct TextureSamplerBinding
    {
        public Texture Texture;
        public Sampler Sampler;

        public TextureSamplerBinding(Texture texture, Sampler sampler)
        {
            Texture = texture;
            Sampler = sampler;
        }
    }
}
