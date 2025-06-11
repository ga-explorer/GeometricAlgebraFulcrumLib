using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

public abstract class XGaRotorBase<T> : 
    XGaScalingRotorBase<T>, 
    IXGaRotor<T>
{
    protected XGaRotorBase(XGaProcessor<T> processor)
        : base(processor)
    {
    }


    public abstract IXGaRotor<T> GetRotorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaVersor<T> GetVersorInverse()
    {
        return GetRotorInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaScalingRotor<T> GetScalingRotorInverse()
    {
        return GetRotorInverse();
    }
}