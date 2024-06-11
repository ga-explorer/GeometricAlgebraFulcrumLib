using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars.Samplers;

public class ConstantParametricScalarSampler :
    IParametricScalarSampler
{
    public ConstantParametricScalar ConstantCurve { get; private set; }

    public IFloat64ParametricScalar Curve
        => ConstantCurve;

    public double Point
        => ConstantCurve.Point;

    public double Tangent
        => ConstantCurve.Tangent;

    public Float64ScalarRange ParameterRange { get; private set; }

    public bool IsPeriodic
        => true;

    public int Count
        => 2;

    public ParametricScalarLocalFrame this[int index]
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
    public ConstantParametricScalarSampler(double point, Float64ScalarRange parameterRange)
    {
        ConstantCurve = ConstantParametricScalar.Create(point, 1);
        ParameterRange = parameterRange;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantParametricScalarSampler(double point, double tangent, Float64ScalarRange parameterRange)
    {
        ConstantCurve = ConstantParametricScalar.Create(point, tangent);
        ParameterRange = parameterRange;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               ConstantCurve.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantParametricScalarSampler SetCurve(double point, Float64ScalarRange parameterRange)
    {
        ConstantCurve = ConstantParametricScalar.Create(point, 1);
        ParameterRange = parameterRange;

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantParametricScalarSampler SetCurve(double point, double tangent, Float64ScalarRange parameterRange)
    {
        ConstantCurve = ConstantParametricScalar.Create(point, tangent);
        ParameterRange = parameterRange;

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
    public IEnumerable<double> GetPoints()
    {
        yield return Point;
        yield return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetTangents()
    {
        yield return Tangent;
        yield return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ParametricScalarLocalFrame> GetFrames()
    {
        yield return ConstantCurve.GetFrame(ParameterRange.MinValue);
        yield return ConstantCurve.GetFrame(ParameterRange.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<ParametricScalarLocalFrame> GetEnumerator()
    {
        return GetFrames().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}