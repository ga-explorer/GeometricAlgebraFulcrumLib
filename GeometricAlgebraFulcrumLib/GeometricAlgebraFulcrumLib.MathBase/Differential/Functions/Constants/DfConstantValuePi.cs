using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Constants
{
    public sealed class DfConstantValuePi :
        DfConstantValue
    {
        public static DfConstantValuePi DefaultValue { get; }
            = new DfConstantValuePi();


        public override bool IsZero 
            => false;

        public override bool IsOne 
            => false;

        public override double Float64Value
            => Math.PI;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfConstantValuePi()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DfConstantValue Simplify()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return "Pi";
        }
    }
}