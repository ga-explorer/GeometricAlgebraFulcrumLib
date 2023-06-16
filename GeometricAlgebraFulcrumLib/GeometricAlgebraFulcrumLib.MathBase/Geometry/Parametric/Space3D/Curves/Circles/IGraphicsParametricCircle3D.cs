using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Circles
{
    public interface IGraphicsParametricCircle3D :
        IParametricC2Curve3D,
        IArcLengthCurve3D
    {
        double Radius { get; }

        Float64Vector3D Center { get; }

        Float64Vector3D UnitNormal { get; }

        int RotationCount { get; }
    }
}