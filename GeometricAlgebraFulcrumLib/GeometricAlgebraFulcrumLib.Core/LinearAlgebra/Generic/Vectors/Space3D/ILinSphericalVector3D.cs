using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinSphericalVector3D<T> :
    ILinVector3D<T>
{
    Scalar<T> R { get; }

    LinAngle<T> Theta { get; }

    LinAngle<T> Phi { get; }

    bool IsUnitVector();

    bool IsNearUnitVector();
}