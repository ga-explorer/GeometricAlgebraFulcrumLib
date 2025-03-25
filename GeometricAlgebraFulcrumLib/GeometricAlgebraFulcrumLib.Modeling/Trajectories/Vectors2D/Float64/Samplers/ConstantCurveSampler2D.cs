using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Samplers;

public class ConstantCurveSampler2D :
    IParametricCurveSampler2D
{
    public Float64ConstantPath2D ConstantCurve { get; private set; }

    public Float64Path2D Curve
        => ConstantCurve;

    public LinFloat64Vector2D Point
        => ConstantCurve.Point;

    public LinFloat64Vector2D Tangent
        => ConstantCurve.Tangent;

    public Float64ScalarRange ParameterRange { get; private set; }

    public bool IsPeriodic
        => true;

    public int Count
        => 2;

    public Float64Path2DLocalFrame this[int index]
    {
        get
        {
            var t =
                index % 2 == 0
                    ? ParameterRange.MinValue
                    : ParameterRange.MaxValue;

            return ConstantCurve.GetFrame(t);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler2D(ILinFloat64Vector2D point, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath2D.Finite(point, LinFloat64Vector2D.E1);
        ParameterRange = timeRange;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler2D(ILinFloat64Vector2D point, ILinFloat64Vector2D tangent, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath2D.Finite(point, tangent);
        ParameterRange = timeRange;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               ConstantCurve.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler2D SetCurve(ILinFloat64Vector2D point, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath2D.Finite(point, LinFloat64Vector2D.E1);
        ParameterRange = timeRange;

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler2D SetCurve(ILinFloat64Vector2D point, ILinFloat64Vector2D tangent, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath2D.Finite(point, tangent);
        ParameterRange = timeRange;

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetParameterValues()
    {
        yield return ParameterRange.MinValue;
        yield return ParameterRange.MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64ScalarRange> GetParameterSections()
    {
        yield return ParameterRange;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector2D> GetPoints()
    {
        yield return Point;
        yield return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector2D> GetTangents()
    {
        yield return Tangent;
        yield return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Path2DLocalFrame> GetFrames()
    {
        yield return ConstantCurve.GetFrame(ParameterRange.MinValue);
        yield return ConstantCurve.GetFrame(ParameterRange.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Path2DLocalFrame> GetEnumerator()
    {
        return GetFrames().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}