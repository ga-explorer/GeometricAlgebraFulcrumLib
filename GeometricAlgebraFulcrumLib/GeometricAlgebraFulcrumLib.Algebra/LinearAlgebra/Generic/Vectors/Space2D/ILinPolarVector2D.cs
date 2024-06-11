using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public interface ILinPolarVector2D<T> :
    ILinVector2D<T>
{
    Scalar<T> R { get; }

    LinPolarAngle<T> Theta { get; }

    bool IsUnitVector();

    bool IsNearUnitVector();
}