using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public abstract class CGaEncoderBase<T>
{
    public CGaGeometricSpace<T> GeometricSpace { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => GeometricSpace.ScalarProcessor;

    public XGaConformalProcessor<T> ConformalProcessor 
        => GeometricSpace.ConformalProcessor;


    protected CGaEncoderBase(CGaGeometricSpace<T> geometricSpace)
    {
        GeometricSpace = geometricSpace;
    }
}