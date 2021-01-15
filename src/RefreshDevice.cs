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
                Conversions.BoolToByte(debugMode)
            );
        }

        /* FIXME: pool this */
        public CommandBuffer AcquireCommandBuffer()
        {
            var commandBufferHandle = Refresh.Refresh_AcquireCommandBuffer(Handle, 0);
            return new CommandBuffer(this, commandBufferHandle);
        }

        public unsafe void Submit(params CommandBuffer[] commandBuffers)
        {
            var commandBufferPtrs = stackalloc IntPtr[commandBuffers.Length];

            for (var i = 0; i < commandBuffers.Length; i += 1)
            {
                commandBufferPtrs[i] = commandBuffers[i].Handle;
            }

            Refresh.Refresh_Submit(
                Handle,
                (uint) commandBuffers.Length,
                (IntPtr) commandBufferPtrs
            );
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
