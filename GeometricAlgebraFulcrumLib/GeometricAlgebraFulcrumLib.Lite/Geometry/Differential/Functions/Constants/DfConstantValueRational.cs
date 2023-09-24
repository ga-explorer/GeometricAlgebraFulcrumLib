using System.Runtime.CompilerServices;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Constants
{
    public sealed class DfConstantValueRational :
        DfConstantValue
    {
        public override bool IsZero 
            => RationalValue.IsZero;

        public override bool IsOne 
            => (RationalValue - ERational.One).IsZero;

        public ERational RationalValue { get; }

        public override double Float64Value
            => RationalValue.ToDouble();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal DfConstantValueRational(ERational rationalValue)
        {
            if (rationalValue.IsNaN() || rationalValue.IsQuietNaN() || rationalValue.IsSignalingNaN() || rationalValue.IsInfinity())
                throw new NotFiniteNumberException(nameof(rationalValue));

            RationalValue = rationalValue.ToLowestTerms();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DfConstantValue Simplify()
        {
            if (RationalValue.IsInteger())
                return new DfConstantValueInteger(
                    RationalValue.ToEIntegerIfExact()
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return RationalValue.ToString();
        }
    }
}