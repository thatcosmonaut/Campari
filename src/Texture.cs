using System;
using System.IO;
using RefreshCS;

namespace Campari
{
    public class Texture : GraphicsResource
    {
        public uint Height { get; }
        public uint Width { get; }
        public Refresh.ColorFormat Format { get; }

        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroyTexture;

        public static Texture LoadPNG(RefreshDevice device, FileInfo fileInfo)
        {
            var pixels = Refresh.Refresh_Image_Load(
                fileInfo.FullName,
                out var width,
                out var height,
                out var channels
            );

            Refresh.TextureCreateInfo textureCreateInfo;
            textureCreateInfo.width = (uint) width;
            textureCreateInfo.height = (uint) height;
            textureCreateInfo.depth = 1;
            textureCreateInfo.format = Refresh.ColorFormat.R8G8B8A8;
            textureCreateInfo.isCube = 0;
            textureCreateInfo.levelCount = 1;
            textureCreateInfo.sampleCount = Refresh.SampleCount.One;
            textureCreateInfo.usageFlags = (uint) Refresh.TextureUsageFlagBits.SamplerBit;

            var texture = new Texture(device, ref textureCreateInfo);

            texture.SetData(pixels, (uint) (width * height * 4));

            Refresh.Refresh_Image_Free(pixels);
            return texture;
        }

        public Texture(RefreshDevice device, ref Refresh.TextureCreateInfo textureCreateInfo) : base(device)
        {
            Handle = Refresh.Refresh_CreateTexture(
                device.Handle,
                ref textureCreateInfo
            );

            Format = textureCreateInfo.format;
        }

        public void SetData(IntPtr data, uint dataLengthInBytes)
        {
            Refresh.TextureSlice textureSlice;
            textureSlice.texture = Handle;
            textureSlice.rectangle.x = 0;
            textureSlice.rectangle.y = 0;
            textureSlice.rectangle.w = (int) Width;
            textureSlice.rectangle.h = (int) Height;
            textureSlice.level = 0;
            textureSlice.layer = 0;
            textureSlice.depth = 0;

            Refresh.Refresh_SetTextureData(
                Device.Handle,
                ref textureSlice,
                data,
                dataLengthInBytes
            );
        }
    }
}
