using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public sealed class Float64BezierNPath2D :
    Float64Path2D
{
    public List<LinFloat64Vector2D> ControlPoints { get; }
        = new List<LinFloat64Vector2D>();

    public int Degree
        => ControlPoints.Count - 1;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64BezierNPath2D(Float64ScalarRange timeRange, bool isPeriodic)
        : base(timeRange, isPeriodic)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return ControlPoints.All(p => p.IsValid());
    }

    public Float64BezierNPath2D GetDerivativeCurve()
    {
        var result = new Float64BezierNPath2D(TimeRange, IsPeriodic);

        for (var n = 0; n < Degree; n++)
            result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return t.DeCasteljau(ControlPoints.ToArray());
    }

    public override Float64Path2D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path2D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        throw new NotImplementedException();
    }

}