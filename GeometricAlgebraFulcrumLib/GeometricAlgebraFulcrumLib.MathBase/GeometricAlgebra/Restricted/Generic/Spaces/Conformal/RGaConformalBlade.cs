using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public abstract class RGaConformalBlade<T> :
    IGeometricElement
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
    protected RGaConformalBlade(RGaConformalSpace<T> space)
    {
        Space = space;
    }


    public abstract bool IsValid();
}