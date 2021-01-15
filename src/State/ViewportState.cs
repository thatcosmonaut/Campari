using RefreshCS;

namespace Campari
{
    public struct ViewportState
    {
        public Viewport[] Viewports;
        public uint ViewportCount;
        public Refresh.Rect[] Scissors;
        public uint ScissorCount;
    }
}
