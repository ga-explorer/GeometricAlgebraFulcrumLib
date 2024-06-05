using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;

public sealed record ParametricCurveLocalFrame2D :
    IParametricCurveLocalFrame2D
{
    public static ParametricCurveLocalFrame2D Create(double parameterValue, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        return new ParametricCurveLocalFrame2D(
            parameterValue,
            point.ToLinVector2D(),
            tangent.ToLinVector2D()
        );
    }


    /// <summary>
    /// The curve parameter value at the given curve point
    /// </summary>
    public Float64Scalar ParameterValue { get; }

    public int Index { get; internal set; } = -1;

    /// <summary>
    /// A point on the curve
    /// </summary>
    public LinFloat64Vector2D Point { get; }

    public int VSpaceDimensions
        => 2;

    public Float64Scalar Item1
        => Point.X;

    public Float64Scalar Item2
        => Point.Y;

    public Float64Scalar X
        => Point.X;

    public Float64Scalar Y
        => Point.Y;

    public Color Color { get; set; }

    /// <summary>
    /// The tangent unit vector to the curve at the given curve point
    /// </summary>
    public LinFloat64Vector2D Tangent { get; }

    public LinFloat64Vector2D Normal { get; }


    private ParametricCurveLocalFrame2D(double parameterValue, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        ParameterValue = parameterValue;
        Point = point.ToLinVector2D();
        Tangent = tangent.ToLinVector2D();
        Normal = Tangent.GetNormal();

        Debug.Assert(IsValid());
    }


    public bool IsValid()
    {
        var isValid =
            !double.IsNaN(ParameterValue) &&
            Point.IsValid() &&
            Tangent.IsValid() &&
            Tangent.VectorENormSquared().IsNearEqual(1);

        return isValid;
    }

    public LinFloat64PolarAngle GetMaxDirectionAngleWithFrame(ParametricCurveLocalFrame2D frame2)
    {
        //TODO: Is this correct?
        var maxAngle = LinFloat64PolarAngle.Angle0;

        var angle = Tangent.GetAngle(frame2.Tangent);
        if (maxAngle.RadiansValue < angle.RadiansValue) maxAngle = angle;

        return maxAngle;
    }


    public ParametricCurveLocalFrame2D TranslateBy(ILinFloat64Vector2D translationVector)
    {
        Debug.Assert(translationVector.IsValid());

        return new ParametricCurveLocalFrame2D(
            ParameterValue,
            LinFloat64Vector2D.Create(Point.X + translationVector.X,
                Point.Y + translationVector.Y),
            Tangent
        );
    }
}