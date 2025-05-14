using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

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