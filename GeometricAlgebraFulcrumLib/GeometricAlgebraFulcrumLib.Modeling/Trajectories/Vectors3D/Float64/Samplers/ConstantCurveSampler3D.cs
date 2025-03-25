using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

public class ConstantCurveSampler3D :
    IParametricCurveSampler3D
{
    public Float64ConstantPath3D ConstantCurve { get; private set; }

    public Float64Path3D Curve
        => ConstantCurve;

    public LinFloat64Vector3D Point
        => ConstantCurve.Point;

    public LinFloat64Vector3D Tangent
        => ConstantCurve.Tangent;

    public Float64ScalarRange ParameterRange { get; private set; }

    public bool IsPeriodic
        => true;

    public int Count
        => 2;

    public Float64Path3DLocalFrame this[int index]
    {
        get
        {
            var parameterValue =
                index % 2 == 0
                    ? ParameterRange.MinValue
                    : ParameterRange.MaxValue;

            return ConstantCurve.GetFrame(parameterValue);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler3D(ILinFloat64Vector3D point, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath3D.Finite(point, LinFloat64Vector3D.E1);
        ParameterRange = timeRange;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler3D(ILinFloat64Vector3D point, ILinFloat64Vector3D tangent, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath3D.Finite(point, tangent);
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
    public ConstantCurveSampler3D SetCurve(ILinFloat64Vector3D point, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath3D.Finite(point, LinFloat64Vector3D.E1);
        ParameterRange = timeRange;

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler3D SetCurve(ILinFloat64Vector3D point, ILinFloat64Vector3D tangent, Float64ScalarRange timeRange)
    {
        ConstantCurve = Float64ConstantPath3D.Finite(point, tangent);
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
    public IEnumerable<LinFloat64Vector3D> GetPoints()
    {
        yield return Point;
        yield return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetTangents()
    {
        yield return Tangent;
        yield return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Path3DLocalFrame> GetFrames()
    {
        yield return ConstantCurve.GetFrame(ParameterRange.MinValue);
        yield return ConstantCurve.GetFrame(ParameterRange.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Path3DLocalFrame> GetEnumerator()
    {
        return GetFrames().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}