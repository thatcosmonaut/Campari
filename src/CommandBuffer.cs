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
            T[] uniforms
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
            T[] uniforms
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

        public void BindVertexBuffers(
            uint firstBinding,
            uint bindingCount,
            Buffer[] buffers,
            UInt64[] offsets
        ) {
            var bufferHandle = GCHandle.Alloc(buffers, GCHandleType.Pinned);
            var offsetHandle = GCHandle.Alloc(offsets, GCHandleType.Pinned);

            Refresh.Refresh_BindVertexBuffers(
                Device.Handle,
                Handle,
                firstBinding,
                bindingCount,
                bufferHandle.AddrOfPinnedObject(),
                offsetHandle.AddrOfPinnedObject()
            );

            bufferHandle.Free();
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

        public void BindFragmentSamplers(
            Texture[] textures,
            Sampler[] samplers
        ) {
            var textureHandle = GCHandle.Alloc(textures, GCHandleType.Pinned);
            var samplerHandle = GCHandle.Alloc(samplers, GCHandleType.Pinned);

            Refresh.Refresh_BindFragmentSamplers(
                Device.Handle,
                Handle,
                textureHandle.AddrOfPinnedObject(),
                samplerHandle.AddrOfPinnedObject()
            );

            textureHandle.Free();
            samplerHandle.Free();
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
