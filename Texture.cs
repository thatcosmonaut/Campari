using System;
using RefreshCS;

namespace Campari
{
    public class Texture : IDisposable
    {
        public RefreshDevice Device { get; }
        public IntPtr Handle { get; }
        public uint Height { get; }
        public uint Width { get; }

        public bool IsDisposed { get; private set; }

        public static Texture Load(RefreshDevice device, string path)
        {
            var pixels = Refresh.Refresh_Image_Load(path, out var width, out var height, out var channels);
            IntPtr textureHandle = Refresh.Refresh_CreateTexture2D(
                device.Handle,
                Refresh.ColorFormat.R8G8B8A8,
                (uint) width,
                (uint) height,
                1,
                (uint) Refresh.TextureUsageFlagBits.SamplerBit
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
                (uint) (width * height * 4)
            );

            return new Texture(
                device,
                textureHandle,
                (uint) width,
                (uint) height
            );
        }

        public Texture(RefreshDevice device, IntPtr handle, uint width, uint height)
        {
            Device = device;
            Handle = handle;
            Width = width;
            Height = height;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                Refresh.Refresh_QueueDestroyTexture(Device.Handle, Handle);
                IsDisposed = true;
            }
        }

        ~Texture()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
