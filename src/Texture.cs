using System;
using System.IO;
using RefreshCS;

namespace Campari
{
    public class Texture : GraphicsResource
    {
        public uint Height { get; }
        public uint Width { get; }

        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroyTexture;

        public Texture(RefreshDevice device, FileInfo fileInfo) : base(device) 
        {
            var pixels = Refresh.Refresh_Image_Load(
                fileInfo.FullName, 
                out var width, 
                out var height, 
                out var channels
            );
            
            IntPtr textureHandle = Refresh.Refresh_CreateTexture2D(
                device.Handle,
                Refresh.ColorFormat.R8G8B8A8,
                (uint)width,
                (uint)height,
                1,
                (uint)Refresh.TextureUsageFlagBits.SamplerBit
            );

            Refresh.TextureSlice textureSlice;
            textureSlice.texture = textureHandle;
            textureSlice.rectangle.x = 0;
            textureSlice.rectangle.y = 0;
            textureSlice.rectangle.w = width;
            textureSlice.rectangle.h = height;
            textureSlice.level = 0;
            textureSlice.layer = 0;
            textureSlice.depth = 0;

            Refresh.Refresh_SetTextureData(
                device.Handle,
                ref textureSlice,
                pixels,
                (uint)(width * height * 4)
            );

            Refresh.Refresh_Image_Free(pixels);

            Width = (uint) width;
            Height = (uint) height;
        }
    }
}
