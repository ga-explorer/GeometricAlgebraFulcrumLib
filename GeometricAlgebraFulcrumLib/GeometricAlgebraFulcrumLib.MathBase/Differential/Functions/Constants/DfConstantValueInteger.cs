using System.Runtime.CompilerServices;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Constants
{
    public sealed class DfConstantValueInteger :
        DfConstantValue
    {
        public override bool IsZero 
            => IntegerValue.IsZero;

        public override bool IsOne 
            => (IntegerValue - EInteger.One).IsZero;

        public EInteger IntegerValue { get; }

        public override double Float64Value
            => IntegerValue.ToInt64Checked();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal DfConstantValueInteger(EInteger integerValue)
        {
            IntegerValue = integerValue;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DfConstantValue Simplify()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IntegerValue.ToString();
        }
    }
}