using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinFloat64Multivector4D :
    IFloat64LinearAlgebraElement,
    IReadOnlyList<double>
{
    double Scalar { get; }

    double Scalar1 { get; }

    double Scalar2 { get; }

    double Scalar3 { get; }

    double Scalar4 { get; }

    double Scalar12 { get; }

    double Scalar13 { get; }

    double Scalar23 { get; }

    double Scalar14 { get; }

    double Scalar24 { get; }

    double Scalar34 { get; }

    double Scalar123 { get; }

    double Scalar124 { get; }

    double Scalar134 { get; }

    double Scalar234 { get; }

    double Scalar1234 { get; }

    bool IsZero();

    bool IsNearZero(double zeroEpsilon = 1e-12d);

    double Norm();

    double NormSquared();

    //Float64Multivector4D ToMultivector4D();
}