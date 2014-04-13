namespace KnowledgeRepresentationReasoning.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    public class Gray
    {
        public static ulong GrayEncode(ulong n)
        {
            return n ^ (n >> 1);
        }

        public static ulong GrayDecode(ulong n)
        {
            ulong i = 1 << 8 * 64 - 2; //long is 64-bit
            ulong p, b = p = n & i;

            while ((i >>= 1) > 0)
                b |= p = n & i ^ p >> 1;
            return b;
        }

        public static List<BitArray> GetGreyCodesWithLengthN(int n)
        {
            var result = new List<BitArray>();
            var start = (ulong)(1 << (n + 1));
            var end = (ulong)(1 << (n + 2));
            for (var i = start; i < end; i++)
            {
                var gray = (int)GrayEncode(i);
                result.Add(new BitArray(new[] { gray }));
            }
            return result;
        }
    }
}
