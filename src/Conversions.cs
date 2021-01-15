namespace Campari
{
    public static class Conversions
    {
        public static byte BoolToByte(bool b)
        {
            return (byte)(b ? 1 : 0);
        }
    }
}
