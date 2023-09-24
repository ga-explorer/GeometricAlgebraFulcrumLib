using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Constants
{
    public sealed class DfConstantValueE :
        DfConstantValue
    {
        public static DfConstantValueE DefaultValue { get; }
            = new DfConstantValueE();

    
        public override bool IsZero 
            => false;

        public override bool IsOne 
            => false;

        public override double Float64Value
            => Math.PI;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfConstantValueE()
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
            return "E";
        }
    }
}