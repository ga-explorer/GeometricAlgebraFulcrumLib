using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles
{
    public interface IGraphicsParametricCircle3D :
        IGraphicsC2ParametricCurve3D,
        IGraphicsC1ArcLengthCurve3D
    {
        public double Radius { get; }

        public Float64Tuple3D Center { get; }

        public Float64Tuple3D UnitNormal { get; }
    }
}