using RefreshCS;

namespace Campari
{
    public unsafe struct ColorBlendState
    {
        public bool LogicOpEnable;
        public Refresh.LogicOp LogicOp;
        public BlendConstants BlendConstants;
        public uint BlendStateCount;
        public Refresh.ColorTargetBlendState[] ColorTargetBlendStates;
    }
}
