using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

public abstract class XGaScalingRotorBase<T> : 
    XGaVersorBase<T>, 
    IXGaScalingRotor<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaScalingRotorBase(XGaProcessor<T> processor)
        : base(processor)
    {
    }


    public abstract Scalar<T> GetScalingFactor();

    public abstract IXGaScalingRotor<T> GetScalingRotorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaVersor<T> GetVersorInverse()
    {
        return GetScalingRotorInverse();
    }
}