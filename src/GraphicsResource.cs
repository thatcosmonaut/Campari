﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Campari
{
    public abstract class GraphicsResource : IDisposable
    {
        public RefreshDevice Device { get; }
        public IntPtr Handle { get; protected set; }

        public bool IsDisposed { get; private set; }
        protected abstract Action<IntPtr, IntPtr> QueueDestroyFunction { get; }

        public GraphicsResource(RefreshDevice device)
        {
            Device = device;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                QueueDestroyFunction(Device.Handle, Handle);
                IsDisposed = true;
            }
        }

        ~GraphicsResource()
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
