using System;
using RefreshCS;

namespace Campari
{
    public class ColorTarget : GraphicsResource
    {
        public uint Width { get; }
        public uint Height { get; }

        public Texture Texture { get; }
        public Refresh.ColorFormat Format => Texture.Format;

        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroyColorTarget;

        public ColorTarget(GraphicsDevice device, Refresh.SampleCount sampleCount, ref TextureSlice textureSlice) : base(device)
        {
            var refreshTextureSlice = textureSlice.ToRefreshTextureSlice();
            Handle = Refresh.Refresh_CreateColorTarget(device.Handle, sampleCount, ref refreshTextureSlice);
        }
    }
}
