using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Extensions
{
    public static class SequenceExtensions
    {
        public static int BitwiseXor(this IEnumerable<int> sequence)
        {
            return sequence.Aggregate(0, (acc, value) => acc ^ value);
        }
    }
}