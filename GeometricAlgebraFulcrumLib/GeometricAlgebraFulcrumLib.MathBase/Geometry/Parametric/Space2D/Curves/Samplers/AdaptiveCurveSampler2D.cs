using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Samplers;

public class AdaptiveCurveSampler2D :
    IParametricCurveSampler2D
{
    public IParametricCurve2D Curve { get; private set; }

    public Float64Range1D ParameterRange { get; private set; }

    public bool IsPeriodic { get; private set; }

    public AdaptiveCurveSamplingOptions2D SamplingOptions { get; private set; }

    public AdaptiveCurve2D SampledCurve { get; private set; }

    public int Count 
        => SampledCurve.Count;

    public ParametricCurveLocalFrame2D this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                if (IsPeriodic)
                    index = index.Mod(Count);

                else
                    throw new IndexOutOfRangeException();
            }

            return SampledCurve[index];
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurveSampler2D(IParametricCurve2D curve, Float64Range1D parameterRange, AdaptiveCurveSamplingOptions2D samplingOptions, bool isPeriodic = false)
    {
        SamplingOptions = samplingOptions;
        IsPeriodic = isPeriodic;
        Curve = curve;
        ParameterRange = parameterRange;
        SampledCurve = curve.CreateAdaptiveCurve2D(parameterRange, samplingOptions);

        Debug.Assert(IsValid());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               Curve.IsValid() &&
               SampledCurve.IsValid() &&
               ((IsPeriodic && Count > 0) || (!IsPeriodic && Count > 1));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurveSampler2D SetCurve(IParametricCurve2D curve, Float64Range1D parameterRange, AdaptiveCurveSamplingOptions2D samplingOptions, bool isPeriodic)
    {
        Curve = curve;
        ParameterRange = parameterRange;
        SamplingOptions = samplingOptions;
        IsPeriodic = isPeriodic;
        SampledCurve = curve.CreateAdaptiveCurve2D(parameterRange, samplingOptions);

        Debug.Assert(IsValid());

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetParameterValues()
    {
        return SampledCurve.GetParameterValues();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Range1D> GetParameterSections()
    {
        var tValues = SampledCurve.GetParameterValues().ToImmutableArray();

        var t1 = tValues[0];
        for (var i = 1; i < tValues.Length; i++)
        {
            var t2 = tValues[i];

            yield return Float64Range1D.Create(t1, t2);

            t1 = t2;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetPoints()
    {
        return SampledCurve.GetPoints();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetTangents()
    {
        return SampledCurve.GetTangents();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ParametricCurveLocalFrame2D> GetFrames()
    {
        return SampledCurve;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<ParametricCurveLocalFrame2D> GetEnumerator()
    {
        return SampledCurve.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}