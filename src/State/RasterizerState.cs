using RefreshCS;

namespace Campari
{
    public struct RasterizerState
    {
        public Refresh.CullMode CullMode;
        public float DepthBiasClamp;
        public float DepthBiasConstantFactor;
        public bool DepthBiasEnable;
        public float DepthBiasSlopeFactor;
        public bool DepthClampEnable;
        public Refresh.FillMode FillMode;
        public Refresh.FrontFace FrontFace;
        public float LineWidth;
    }
}
