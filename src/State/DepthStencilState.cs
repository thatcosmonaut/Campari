using RefreshCS;

namespace Campari
{
    public struct DepthStencilState
    {
        public bool DepthTestEnable;
        public Refresh.StencilOpState BackStencilState;
        public Refresh.StencilOpState FrontStencilState;
        public Refresh.CompareOp CompareOp;
        public bool DepthBoundsTestEnable;
        public bool DepthWriteEnable;
        public float MinDepthBounds;
        public float MaxDepthBounds;
        public bool StencilTestEnable;
    }
}
