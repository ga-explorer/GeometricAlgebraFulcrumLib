using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public sealed record Axis(int Dimensions, int Index, bool IsNegative = false)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSame(Axis axis)
        {
            return Index == axis.Index && 
                   IsNegative == axis.IsNegative;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsOpposite(Axis axis)
        {
            return Index == axis.Index && 
                   IsNegative != axis.IsNegative;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple ToTuple()
        {
            return Float64Tuple.CreateScaledBasis(
                Dimensions,
                Index,
                IsNegative ? -1d : 1d
            );
        }
    }
}
