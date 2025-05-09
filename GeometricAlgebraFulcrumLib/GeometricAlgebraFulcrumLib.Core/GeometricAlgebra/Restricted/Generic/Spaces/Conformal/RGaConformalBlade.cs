using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public abstract class CGaFloat64Blade<T> :
    IAlgebraicElement
{
    public RGaConformalSpace<T> Space { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => Space.ScalarProcessor;

    public RGaProcessor<T> Processor 
        => Space.ConformalProcessor;
    
    public RGaConformalProcessor<T> ConformalProcessor 
        => Space.ConformalProcessor;

    public abstract RGaKVector<T> Blade { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected CGaFloat64Blade(RGaConformalSpace<T> space)
    {
        Space = space;
    }


    public abstract bool IsValid();
}