using System;
using RefreshCS;

namespace Campari
{
    public class Sampler : GraphicsResource
    {
        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroySampler;

        public Sampler(
            RefreshDevice device,
            ref Refresh.SamplerStateCreateInfo samplerStateCreateInfo
        ) : base(device)
        {
            Handle = Refresh.Refresh_CreateSampler(device.Handle, ref samplerStateCreateInfo);
        }
    }
}
