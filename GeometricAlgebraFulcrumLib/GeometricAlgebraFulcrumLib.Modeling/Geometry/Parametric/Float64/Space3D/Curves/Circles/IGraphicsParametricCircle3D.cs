using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Circles;

public interface IGraphicsParametricCircle3D :
    IParametricC2Curve3D,
    IArcLengthCurve3D
{
    double Radius { get; }

    LinFloat64Vector3D Center { get; }

    LinFloat64Vector3D UnitNormal { get; }

    int RotationCount { get; }
}