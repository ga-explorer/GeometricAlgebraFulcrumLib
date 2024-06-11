using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors;

public abstract class RGaFloat64ReflectorBase : 
    RGaFloat64VersorBase, 
    IRGaFloat64Reflector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaFloat64ReflectorBase(RGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract IRGaFloat64Reflector GetReflectorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64Versor GetVersorInverse()
    {
        return GetReflectorInverse();
    }
}