using System;
using System.Diagnostics.CodeAnalysis;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces
{
    public class GrCurveTubeParametricSurface3D :
        IGraphicsParametricSurface3D
    {
        public IGraphicsC1ParametricCurve3D Curve { get; }

        public double Radius { get; }


        public GrCurveTubeParametricSurface3D([NotNull] IGraphicsC1ParametricCurve3D curve, double radius)
        {
            if (radius < 0)
                throw new ArgumentException(nameof(radius));

            Curve = curve;
            Radius = radius;
        }


        public bool IsValid()
        {
            return Curve.IsValid() && Radius.IsValid();
        }

        public Tuple3D GetPoint(double parameterValue1, double parameterValue2)
        {
            var curveFrame = Curve.GetFrame(parameterValue2);

            var angle = parameterValue1 * 2 * Math.PI;

            return 
                curveFrame.Point +
                Radius * Math.Cos(angle) * curveFrame.Normal1.ToTuple3D() - 
                Radius * Math.Sin(angle) * curveFrame.Normal2.ToTuple3D();
        }

        public Tuple3D GetNormal(double parameterValue1, double parameterValue2)
        {
            var curveFrame = Curve.GetFrame(parameterValue2);

            var angle = parameterValue1 * 2 * Math.PI;

            return 
                Radius * Math.Cos(angle) * curveFrame.Normal1.ToTuple3D() - 
                Radius * Math.Sin(angle) * curveFrame.Normal2.ToTuple3D();
        }

        public Tuple3D GetUnitNormal(double parameterValue1, double parameterValue2)
        {
            return GetNormal(parameterValue1, parameterValue2).ToUnitVector();
        }

        public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
        {
            var curveFrame = Curve.GetFrame(parameterValue2);

            var angle = parameterValue1 * 2 * Math.PI;

            var normal = 
                Radius * Math.Cos(angle) * curveFrame.Normal1.ToTuple3D() - 
                Radius * Math.Sin(angle) * curveFrame.Normal2.ToTuple3D();

            var point = curveFrame + normal;

            return new GrParametricSurfaceLocalFrame3D(
                parameterValue1,
                parameterValue2,
                point,
                normal.ToUnitVector()
            );
        }
    }
}