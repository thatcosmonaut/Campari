using System;
using RefreshCS;

namespace Campari
{
    public class Buffer : GraphicsResource
    {
        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroyBuffer;

        /* FIXME: could we have a special flags struct? */
        public Buffer(RefreshDevice device, uint usageFlags, uint sizeInBytes) : base(device)
        {
            Handle = Refresh.Refresh_CreateBuffer(
                device.Handle,
                usageFlags,
                sizeInBytes
            );
        }

        public unsafe void SetData<T>(
            uint offsetInBytes,
            T[] data,
            uint dataLengthInBytes
        ) where T : unmanaged
        {
            fixed (T* ptr = &data[0])
            {
                Refresh.Refresh_SetBufferData(
                    Device.Handle,
                    Handle,
                    offsetInBytes,
                    (IntPtr) ptr,
                    dataLengthInBytes
                );
            }
        }
    }
}
