using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors
{
    public abstract class XGaScaledRotorBase<T> : 
        XGaVersorBase<T>, 
        IXGaScaledRotor<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected XGaScaledRotorBase(XGaProcessor<T> processor)
            : base(processor)
        {
        }


        public abstract Scalar<T> GetScalingFactor();

        public abstract IXGaScaledRotor<T> GetScaledRotorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaVersor<T> GetVersorInverse()
        {
            return GetScaledRotorInverse();
        }
    }
}