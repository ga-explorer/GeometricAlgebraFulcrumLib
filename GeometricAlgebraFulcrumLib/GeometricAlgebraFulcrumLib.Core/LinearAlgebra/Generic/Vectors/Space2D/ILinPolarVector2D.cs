using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

public interface ILinPolarVector2D<T> :
    ILinVector2D<T>
{
    Scalar<T> R { get; }

    LinPolarAngle<T> Theta { get; }

    bool IsUnitVector();

    bool IsNearUnitVector();
}