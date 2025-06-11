using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64Multivector2D :
    IFloat64LinearAlgebraElement,
    IReadOnlyList<double>
{
    double Scalar { get; }

    double Scalar1 { get; }

    double Scalar2 { get; }

    double Scalar12 { get; }

    bool IsZero();

    bool IsNearZero(double zeroEpsilon = 1e-12d);

    double Norm();

    double NormSquared();

    LinFloat64Multivector2D ToMultivector2D();
}