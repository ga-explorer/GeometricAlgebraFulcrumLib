using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Circles
{
    public interface IGraphicsParametricCircle3D :
        IParametricC2Curve3D,
        IArcLengthC1Curve3D
    {
        public double Radius { get; }

        public Float64Tuple3D Center { get; }

        public Float64Tuple3D UnitNormal { get; }
    }
}