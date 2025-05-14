using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces.Conformal;

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