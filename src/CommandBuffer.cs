using System;
using System.Runtime.InteropServices;
using RefreshCS;

namespace Campari
{
    public struct CommandBuffer
    {
        public RefreshDevice Device { get; }
        public IntPtr Handle { get; }

        // called from RefreshDevice
        internal CommandBuffer(RefreshDevice device, IntPtr handle)
        {
            Device = device;
            Handle = handle;
        }

        public void BeginRenderPass(
            RenderPass renderPass,
            Framebuffer framebuffer,
            ref Refresh.Rect renderArea,
            Refresh.Color[] clearColors,
            ref Refresh.DepthStencilValue depthStencilClearValue
        ) {
            var clearColorHandle = GCHandle.Alloc(clearColors, GCHandleType.Pinned);

            Refresh.Refresh_BeginRenderPass(
                Device.Handle,
                Handle,
                renderPass.Handle,
                framebuffer.Handle,
                ref renderArea,
                clearColorHandle.AddrOfPinnedObject(),
                (uint) clearColors.Length,
                ref depthStencilClearValue
            );

            clearColorHandle.Free();
        }

        public void BeginRenderPass(
            RenderPass renderPass,
            Framebuffer framebuffer,
            ref Refresh.Rect renderArea,
            Refresh.Color[] clearColors
        ) {
            var clearColorHandle = GCHandle.Alloc(clearColors, GCHandleType.Pinned);

            Refresh.Refresh_BeginRenderPass(
                Device.Handle,
                Handle,
                renderPass.Handle,
                framebuffer.Handle,
                ref renderArea,
                clearColorHandle.AddrOfPinnedObject(),
                (uint) clearColors.Length,
                IntPtr.Zero
            );

            clearColorHandle.Free();

        }

        public void BindGraphicsPipeline(
            GraphicsPipeline graphicsPipeline
        ) {
            Refresh.Refresh_BindGraphicsPipeline(
                Device.Handle,
                Handle,
                graphicsPipeline.Handle
            );
        }

        public unsafe uint PushVertexShaderParams<T>(
            params T[] uniforms
        ) where T : unmanaged
        {
            fixed (T* ptr = &uniforms[0])
            {
                return Refresh.Refresh_PushVertexShaderParams(
                    Device.Handle,
                    Handle,
                    (IntPtr) ptr,
                    (uint) uniforms.Length
                );
            }
        }

        public unsafe uint PushFragmentShaderParams<T>(
            params T[] uniforms
        ) where T : unmanaged
        {
            fixed (T* ptr = &uniforms[0])
            {
                return Refresh.Refresh_PushFragmentShaderParams(
                    Device.Handle,
                    Handle,
                    (IntPtr) ptr,
                    (uint) uniforms.Length
                );
            }
        }

        public unsafe void BindVertexBuffers(
            uint firstBinding,
            uint bindingCount,
            Buffer[] buffers,
            UInt64[] offsets
        ) {
            var bufferPtrs = stackalloc IntPtr[buffers.Length];

            for (var i = 0; i < buffers.Length; i += 1)
            {
                bufferPtrs[i] = buffers[i].Handle;
            }
            var offsetHandle = GCHandle.Alloc(offsets, GCHandleType.Pinned);

            Refresh.Refresh_BindVertexBuffers(
                Device.Handle,
                Handle,
                firstBinding,
                bindingCount,
                (IntPtr) bufferPtrs,
                offsetHandle.AddrOfPinnedObject()
            );

            offsetHandle.Free();
        }

        public void BindIndexBuffer(
            Buffer indexBuffer,
            uint offset,
            Refresh.IndexElementSize indexElementSize
        ) {
            Refresh.Refresh_BindIndexBuffer(
                Device.Handle,
                Handle,
                indexBuffer.Handle,
                offset,
                indexElementSize
            );
        }

        public unsafe void BindFragmentSamplers(
            Texture[] textures,
            Sampler[] samplers
        ) {
            var texturePtrs = stackalloc IntPtr[textures.Length];
            var samplerPtrs = stackalloc IntPtr[samplers.Length];

            for (var i = 0; i < textures.Length; i += 1)
            {
                texturePtrs[i] = textures[i].Handle;
            }

            for (var i = 0; i < samplers.Length; i += 1)
            {
                samplerPtrs[i] = samplers[i].Handle;
            }

            Refresh.Refresh_BindFragmentSamplers(
                Device.Handle,
                Handle,
                (IntPtr) texturePtrs,
                (IntPtr) samplerPtrs
            );
        }

        public void DrawPrimitives(
            uint vertexStart,
            uint primitiveCount,
            uint vertexParamOffset,
            uint fragmentParamOffset
        ) {
            Refresh.Refresh_DrawPrimitives(
                Device.Handle,
                Handle,
                vertexStart,
                primitiveCount,
                vertexParamOffset,
                fragmentParamOffset
            );
        }

        public void EndRenderPass()
        {
            Refresh.Refresh_EndRenderPass(
                Device.Handle,
                Handle
            );
        }

        public void QueuePresent(ref TextureSlice textureSlice, ref Refresh.Rect rectangle, Refresh.Filter filter)
        {
            var refreshTextureSlice = textureSlice.ToRefreshTextureSlice();

            Refresh.Refresh_QueuePresent(
                Device.Handle,
                Handle,
                ref refreshTextureSlice,
                ref rectangle,
                filter
            );
        }
    }
}
