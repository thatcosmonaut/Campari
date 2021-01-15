using System;
using System.Runtime.InteropServices;
using RefreshCS;

namespace Campari
{
    public class GraphicsPipeline : GraphicsResource
    {
        protected override Action<IntPtr, IntPtr> QueueDestroyFunction => Refresh.Refresh_QueueDestroyGraphicsPipeline;

        public unsafe GraphicsPipeline(
            RefreshDevice device,
            ColorBlendState colorBlendState,
            DepthStencilState depthStencilState,
            ShaderStageState fragmentShaderState,
            ShaderStageState vertexShaderState,
            MultisampleState multisampleState,
            GraphicsPipelineLayoutCreateInfo pipelineLayoutCreateInfo,
            RasterizerState rasterizerState,
            Refresh.PrimitiveType primitiveType,
            VertexInputState vertexInputState,
            ViewportState viewportState,
            RenderPass renderPass
        ) : base(device)
        {
            var blendStateHandle = GCHandle.Alloc(colorBlendState.ColorTargetBlendStates, GCHandleType.Pinned);
            var vertexAttributesHandle = GCHandle.Alloc(vertexInputState.VertexAttributes, GCHandleType.Pinned);
            var vertexBindingsHandle = GCHandle.Alloc(vertexInputState.VertexBindings, GCHandleType.Pinned);
            var viewportHandle = GCHandle.Alloc(viewportState.Viewports, GCHandleType.Pinned);
            var scissorHandle = GCHandle.Alloc(viewportState.Scissors, GCHandleType.Pinned);

            Refresh.GraphicsPipelineCreateInfo graphicsPipelineCreateInfo;

            graphicsPipelineCreateInfo.colorBlendState.logicOpEnable = Conversions.BoolToByte(colorBlendState.LogicOpEnable);
            graphicsPipelineCreateInfo.colorBlendState.logicOp = colorBlendState.LogicOp;
            graphicsPipelineCreateInfo.colorBlendState.blendStates = blendStateHandle.AddrOfPinnedObject();
            graphicsPipelineCreateInfo.colorBlendState.blendStateCount = colorBlendState.BlendStateCount;
            graphicsPipelineCreateInfo.colorBlendState.blendConstants[0] = colorBlendState.BlendConstants.R;
            graphicsPipelineCreateInfo.colorBlendState.blendConstants[1] = colorBlendState.BlendConstants.G;
            graphicsPipelineCreateInfo.colorBlendState.blendConstants[2] = colorBlendState.BlendConstants.B;
            graphicsPipelineCreateInfo.colorBlendState.blendConstants[3] = colorBlendState.BlendConstants.A;

            graphicsPipelineCreateInfo.depthStencilState.backStencilState = depthStencilState.BackStencilState;
            graphicsPipelineCreateInfo.depthStencilState.compareOp = depthStencilState.CompareOp;
            graphicsPipelineCreateInfo.depthStencilState.depthBoundsTestEnable = Conversions.BoolToByte(depthStencilState.DepthBoundsTestEnable);
            graphicsPipelineCreateInfo.depthStencilState.depthTestEnable = Conversions.BoolToByte(depthStencilState.DepthTestEnable);
            graphicsPipelineCreateInfo.depthStencilState.depthWriteEnable = Conversions.BoolToByte(depthStencilState.DepthWriteEnable);
            graphicsPipelineCreateInfo.depthStencilState.frontStencilState = depthStencilState.FrontStencilState;
            graphicsPipelineCreateInfo.depthStencilState.maxDepthBounds = depthStencilState.MaxDepthBounds;
            graphicsPipelineCreateInfo.depthStencilState.minDepthBounds = depthStencilState.MinDepthBounds;
            graphicsPipelineCreateInfo.depthStencilState.stencilTestEnable = Conversions.BoolToByte(depthStencilState.StencilTestEnable);

            graphicsPipelineCreateInfo.vertexShaderState.entryPointName = vertexShaderState.EntryPointName;
            graphicsPipelineCreateInfo.vertexShaderState.shaderModule = vertexShaderState.ShaderModule.Handle;
            graphicsPipelineCreateInfo.vertexShaderState.uniformBufferSize = vertexShaderState.UniformBufferSize;

            graphicsPipelineCreateInfo.fragmentShaderState.entryPointName = fragmentShaderState.EntryPointName;
            graphicsPipelineCreateInfo.fragmentShaderState.shaderModule = fragmentShaderState.ShaderModule.Handle;
            graphicsPipelineCreateInfo.fragmentShaderState.uniformBufferSize = fragmentShaderState.UniformBufferSize;

            graphicsPipelineCreateInfo.multisampleState.multisampleCount = multisampleState.MultisampleCount;
            graphicsPipelineCreateInfo.multisampleState.sampleMask = multisampleState.SampleMask;

            graphicsPipelineCreateInfo.pipelineLayoutCreateInfo.vertexSamplerBindingCount = pipelineLayoutCreateInfo.VertexSamplerBindingCount;
            graphicsPipelineCreateInfo.pipelineLayoutCreateInfo.fragmentSamplerBindingCount = pipelineLayoutCreateInfo.FragmentSamplerBindingCount;

            graphicsPipelineCreateInfo.rasterizerState.cullMode = rasterizerState.CullMode;
            graphicsPipelineCreateInfo.rasterizerState.depthBiasClamp = rasterizerState.DepthBiasClamp;
            graphicsPipelineCreateInfo.rasterizerState.depthBiasConstantFactor = rasterizerState.DepthBiasConstantFactor;
            graphicsPipelineCreateInfo.rasterizerState.depthBiasEnable = Conversions.BoolToByte(rasterizerState.DepthBiasEnable);
            graphicsPipelineCreateInfo.rasterizerState.depthBiasSlopeFactor = rasterizerState.DepthBiasSlopeFactor;
            graphicsPipelineCreateInfo.rasterizerState.depthClampEnable = Conversions.BoolToByte(rasterizerState.DepthClampEnable);
            graphicsPipelineCreateInfo.rasterizerState.fillMode = rasterizerState.FillMode;
            graphicsPipelineCreateInfo.rasterizerState.frontFace = rasterizerState.FrontFace;
            graphicsPipelineCreateInfo.rasterizerState.lineWidth = rasterizerState.LineWidth;

            graphicsPipelineCreateInfo.vertexInputState.vertexAttributes = vertexAttributesHandle.AddrOfPinnedObject();
            graphicsPipelineCreateInfo.vertexInputState.vertexAttributeCount = vertexInputState.VertexAttributeCount;
            graphicsPipelineCreateInfo.vertexInputState.vertexBindings = vertexBindingsHandle.AddrOfPinnedObject();
            graphicsPipelineCreateInfo.vertexInputState.vertexBindingCount = vertexInputState.VertexBindingCount;

            graphicsPipelineCreateInfo.viewportState.viewports = viewportHandle.AddrOfPinnedObject();
            graphicsPipelineCreateInfo.viewportState.viewportCount = viewportState.ViewportCount;
            graphicsPipelineCreateInfo.viewportState.scissors = scissorHandle.AddrOfPinnedObject();
            graphicsPipelineCreateInfo.viewportState.scissorCount = viewportState.ScissorCount;

            graphicsPipelineCreateInfo.primitiveType = primitiveType;
            graphicsPipelineCreateInfo.renderPass = renderPass.Handle;

            Handle = Refresh.Refresh_CreateGraphicsPipeline(device.Handle, ref graphicsPipelineCreateInfo);

            blendStateHandle.Free();
            vertexAttributesHandle.Free();
            vertexBindingsHandle.Free();
            viewportHandle.Free();
            scissorHandle.Free();
        }
    }
}
