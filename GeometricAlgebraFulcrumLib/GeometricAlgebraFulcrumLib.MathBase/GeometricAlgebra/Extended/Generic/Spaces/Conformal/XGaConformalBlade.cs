using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public abstract class XGaConformalBlade<T> :
    IGeometricElement
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