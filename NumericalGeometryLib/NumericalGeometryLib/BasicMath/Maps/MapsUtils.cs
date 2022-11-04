using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps
{
    public static class MapsUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64DenseTuple CreateDenseTuple(this double[] itemArray)
        {
            return new Float64DenseTuple(itemArray);
        }
    }
}
