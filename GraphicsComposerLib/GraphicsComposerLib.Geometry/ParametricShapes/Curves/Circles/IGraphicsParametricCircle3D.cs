using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Circles
{
    public interface IGraphicsParametricCircle3D :
        IGraphicsParametricCurve3D
    {
        public double Radius { get; }

        public Tuple3D Center { get; }

        public Tuple3D UnitNormal { get; }
    }
}