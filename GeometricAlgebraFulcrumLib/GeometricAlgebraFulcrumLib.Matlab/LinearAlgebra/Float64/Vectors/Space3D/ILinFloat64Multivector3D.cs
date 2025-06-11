using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinFloat64Multivector3D :
    IFloat64LinearAlgebraElement,
    IReadOnlyList<double>
{
    double Scalar { get; }

    double Scalar1 { get; }

    double Scalar2 { get; }

    double Scalar3 { get; }

    double Scalar12 { get; }

    double Scalar13 { get; }

    double Scalar23 { get; }

    double Scalar123 { get; }

    bool IsZero();

    bool IsNearZero(double zeroEpsilon = 1e-12d);

    double Norm();

    double NormSquared();

    LinFloat64Multivector3D ToMultivector3D();
}