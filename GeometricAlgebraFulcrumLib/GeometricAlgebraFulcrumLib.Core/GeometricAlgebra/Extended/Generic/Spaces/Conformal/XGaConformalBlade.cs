using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public abstract class XGaConformalBlade<T> :
    IAlgebraicElement
{
    public XGaConformalSpace<T> Space { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => Space.ScalarProcessor;

    public XGaProcessor<T> Processor 
        => Space.ConformalProcessor;
    
    public XGaConformalProcessor<T> ConformalProcessor 
        => Space.ConformalProcessor;

    public abstract XGaKVector<T> Blade { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaConformalBlade(XGaConformalSpace<T> space)
    {
        Space = space;
    }


    public abstract bool IsValid();
}