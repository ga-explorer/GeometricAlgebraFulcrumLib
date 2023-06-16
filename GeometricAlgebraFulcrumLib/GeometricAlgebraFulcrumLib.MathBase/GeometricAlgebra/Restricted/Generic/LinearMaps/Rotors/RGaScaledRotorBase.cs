using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors
{
    public abstract class RGaScaledRotorBase<T> : 
        RGaVersorBase<T>, 
        IRGaScaledRotor<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected RGaScaledRotorBase(RGaProcessor<T> processor)
            : base(processor)
        {
        }


        public abstract Scalar<T> GetScalingFactor();

        public abstract IRGaScaledRotor<T> GetScaledRotorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaVersor<T> GetVersorInverse()
        {
            return GetScaledRotorInverse();
        }
    }
}