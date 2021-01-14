using System;
using System.Collections.Generic;
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
