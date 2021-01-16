using System;
using RefreshCS;

namespace Campari
{
    public class Framebuffer : GraphicsResource
    {
        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroyFramebuffer;

        public unsafe Framebuffer(
            GraphicsDevice device,
            uint width,
            uint height,
            RenderPass renderPass,
            DepthStencilTarget depthStencilTarget, /* can be NULL */
            params ColorTarget[] colorTargets
        ) : base(device)
        {
            IntPtr[] colorTargetHandles = new IntPtr[colorTargets.Length];
            for (var i = 0; i < colorTargets.Length; i += 1)
            {
                colorTargetHandles[i] = colorTargets[i].Handle;
            }

            IntPtr depthStencilTargetHandle;
            if (depthStencilTarget == null)
            {
                depthStencilTargetHandle = IntPtr.Zero;
            }
            else
            {
                depthStencilTargetHandle = depthStencilTarget.Handle;
            }

            fixed (IntPtr* colorTargetHandlesPtr = colorTargetHandles)
            {
                Refresh.FramebufferCreateInfo framebufferCreateInfo = new Refresh.FramebufferCreateInfo
                {
                    width = width,
                    height = height,
                    colorTargetCount = (uint) colorTargets.Length,
                    pColorTargets = (IntPtr) colorTargetHandlesPtr,
                    depthStencilTarget = depthStencilTargetHandle,
                    renderPass = renderPass.Handle
                };

                Handle = Refresh.Refresh_CreateFramebuffer(device.Handle, ref framebufferCreateInfo);
            }
        }
    }
}
