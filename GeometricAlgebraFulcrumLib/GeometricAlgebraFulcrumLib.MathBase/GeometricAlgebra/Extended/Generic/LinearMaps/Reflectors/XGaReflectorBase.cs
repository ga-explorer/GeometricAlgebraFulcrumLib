using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Reflectors;

public abstract class XGaReflectorBase<T> : 
    XGaVersorBase<T>, 
    IXGaReflector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaReflectorBase(XGaProcessor<T> processor)
        : base(processor)
    {
    }


    public abstract IXGaReflector<T> GetReflectorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaVersor<T> GetVersorInverse()
    {
        return GetReflectorInverse();
    }
}