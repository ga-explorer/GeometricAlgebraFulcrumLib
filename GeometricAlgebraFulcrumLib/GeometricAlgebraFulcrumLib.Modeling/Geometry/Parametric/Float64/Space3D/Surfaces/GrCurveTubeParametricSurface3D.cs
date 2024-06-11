using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces;

public class GrCurveTubeParametricSurface3D :
    IGraphicsParametricSurface3D
{
    public IParametricCurve3D Curve { get; }

    public double Radius { get; }


    public GrCurveTubeParametricSurface3D(IParametricCurve3D curve, double radius)
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

    public LinFloat64Vector3D GetPoint(double parameterValue1, double parameterValue2)
    {
        var curveFrame = Curve.GetFrame(parameterValue2);

        var angle = parameterValue1 * 2 * Math.PI;

        return
            curveFrame.Point +
            Radius * Math.Cos(angle) * curveFrame.Normal1.ToLinVector3D() -
            Radius * Math.Sin(angle) * curveFrame.Normal2.ToLinVector3D();
    }

    public LinFloat64Vector3D GetNormal(double parameterValue1, double parameterValue2)
    {
        var curveFrame = Curve.GetFrame(parameterValue2);

        var angle = parameterValue1 * 2 * Math.PI;

        return
            Radius * Math.Cos(angle) * curveFrame.Normal1.ToLinVector3D() -
            Radius * Math.Sin(angle) * curveFrame.Normal2.ToLinVector3D();
    }

    public LinFloat64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2)
    {
        return GetNormal(parameterValue1, parameterValue2).ToUnitLinVector3D();
    }

    public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
    {
        var curveFrame = Curve.GetFrame(parameterValue2);

        var angle = parameterValue1 * 2 * Math.PI;

        var normal =
            Radius * Math.Cos(angle) * curveFrame.Normal1.ToLinVector3D() -
            Radius * Math.Sin(angle) * curveFrame.Normal2.ToLinVector3D();

        var point = curveFrame + normal;

        return new GrParametricSurfaceLocalFrame3D(
            parameterValue1,
            parameterValue2,
            point,
            normal.ToUnitLinVector3D()
        );
    }
}