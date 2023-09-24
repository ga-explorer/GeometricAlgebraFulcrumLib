using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    public abstract class RGaFloat64ScaledRotorBase : 
        RGaFloat64VersorBase, 
        IRGaFloat64ScaledRotor
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected RGaFloat64ScaledRotorBase(RGaFloat64Processor metric)
            : base(metric)
        {
        }


        public abstract double GetScalingFactor();

        public abstract IRGaFloat64ScaledRotor GetScaledRotorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64Versor GetVersorInverse()
        {
            return GetScaledRotorInverse();
        }
    }
}