using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using RefreshCS;

namespace Campari
{
    public class RefreshDevice : IDisposable
    {
        public IntPtr Handle { get; }

        public bool IsDisposed { get; private set; }

        public RefreshDevice(
            Refresh.PresentationParameters presentationParameters,
            bool debugMode
        ) {
            Handle = Refresh.Refresh_CreateDevice(
                ref presentationParameters,
                (byte) (debugMode ? 1 : 0)
            );
        }

        /* FIXME: pool this */
        public CommandBuffer AcquireCommandBuffer()
        {
            var commandBufferHandle = Refresh.Refresh_AcquireCommandBuffer(Handle, 0);
            return new CommandBuffer(this, commandBufferHandle);
        }

        public void Submit(CommandBuffer[] commandBuffers)
        {
            var commandBufferHandle = GCHandle.Alloc(commandBuffers, GCHandleType.Pinned);

            Refresh.Refresh_Submit(
                Handle,
                (uint) commandBuffers.Length,
                commandBufferHandle.AddrOfPinnedObject()
            );

            commandBufferHandle.Free();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                Refresh.Refresh_DestroyDevice(Handle);
                IsDisposed = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~RefreshDevice()
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
