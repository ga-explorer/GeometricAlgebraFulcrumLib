using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

public abstract class XGaRotorBase<T> : 
    XGaScaledRotorBase<T>, 
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
    public override IXGaScaledRotor<T> GetScaledRotorInverse()
    {
        return GetRotorInverse();
    }
}