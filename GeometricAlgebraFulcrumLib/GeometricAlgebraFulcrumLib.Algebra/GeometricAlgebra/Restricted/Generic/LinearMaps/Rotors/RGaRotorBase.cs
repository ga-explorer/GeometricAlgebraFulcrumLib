using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public abstract class RGaRotorBase<T> : 
    RGaScaledRotorBase<T>, 
    IRGaRotor<T>
{
    protected RGaRotorBase(RGaProcessor<T> processor)
        : base(processor)
    {
    }


    public abstract IRGaRotor<T> GetRotorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaVersor<T> GetVersorInverse()
    {
        return GetRotorInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaScaledRotor<T> GetScaledRotorInverse()
    {
        return GetRotorInverse();
    }
}