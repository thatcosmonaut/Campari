﻿using RefreshCS;
using System;

namespace Campari
{
    public class ComputePipeline : GraphicsResource
    {
        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroyComputePipeline;

        public unsafe ComputePipeline(
            GraphicsDevice device,
            ShaderStageState computeShaderState,
            uint bufferBindingCount,
            uint imageBindingCount
        ) : base(device) {
            var computePipelineLayoutCreateInfo = new Refresh.ComputePipelineLayoutCreateInfo
            {
                bufferBindingCount = bufferBindingCount,
                imageBindingCount = imageBindingCount
            };

            var computePipelineCreateInfo = new Refresh.ComputePipelineCreateInfo
            {
                pipelineLayoutCreateInfo = computePipelineLayoutCreateInfo,
                computeShaderState = new Refresh.ShaderStageState
                {
                    entryPointName = computeShaderState.EntryPointName,
                    shaderModule = computeShaderState.ShaderModule.Handle,
                    uniformBufferSize = computeShaderState.UniformBufferSize
                }
            };

            Handle = Refresh.Refresh_CreateComputePipeline(
                device.Handle,
                ref computePipelineCreateInfo
            );
        }
    }
}
