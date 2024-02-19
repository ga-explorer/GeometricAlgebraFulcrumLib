using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

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