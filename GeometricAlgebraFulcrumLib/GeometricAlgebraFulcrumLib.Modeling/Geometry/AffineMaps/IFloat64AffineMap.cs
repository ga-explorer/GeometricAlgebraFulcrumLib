using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps;

public interface IFloat64AffineMap :
    IAlgebraicElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance);

    double[,] GetArray2D();


}