using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath
{
    public static class ComplexUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this IEnumerable<Complex> numbers)
        {
            return numbers.Aggregate(Complex.Zero, (a, b) => a + b);
        }
    }
}
