using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public abstract class RGaFloat64ConformalBlade :
    IAlgebraicElement
{
    public RGaFloat64ConformalSpace Space { get; }

    public RGaFloat64ConformalProcessor Processor 
        => RGaFloat64Processor.Conformal;

    public abstract RGaFloat64KVector Blade { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaFloat64ConformalBlade(RGaFloat64ConformalSpace space)
    {
        Space = space;
    }


    public abstract bool IsValid();
}