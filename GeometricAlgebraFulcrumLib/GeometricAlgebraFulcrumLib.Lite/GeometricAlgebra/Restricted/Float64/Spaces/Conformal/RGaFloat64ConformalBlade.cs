using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public abstract class RGaFloat64ConformalBlade :
    IGeometricElement
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