namespace DataStructuresLib.ODS
{
    public static class BitHacks
    {
        private static readonly int[] MultiplyDeBruijnBitPosition2 = { 0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9 };

        public static int Power2Msb(uint v)
        {
            System.Diagnostics.Debug.Assert(v == RoundToPower(v));
            return MultiplyDeBruijnBitPosition2[(v * 0x077CB531U) >> 27];
        }

        public static uint RoundToPower(uint v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            return ++v;
        }

        public static uint MaxValue(int width)
        {
            return ((1u << (width-1)) << 1) - 1;
        }
    }
}
