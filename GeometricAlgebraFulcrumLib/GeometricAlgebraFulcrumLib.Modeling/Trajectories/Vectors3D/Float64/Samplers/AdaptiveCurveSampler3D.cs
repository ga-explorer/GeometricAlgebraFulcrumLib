using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

public class AdaptiveCurveSampler3D :
    IParametricCurveSampler3D
{
    public Float64Path3D Curve { get; private set; }

    public Float64ScalarRange ParameterRange { get; private set; }

    public bool IsPeriodic { get; private set; }

    public Float64AdaptivePath3DSamplingOptions SamplingOptions { get; private set; }

    public Float64AdaptivePath3D SampledCurve { get; private set; }

    public int Count
        => SampledCurve.Count;

    public Float64Path3DLocalFrame this[int index]
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
    public AdaptiveCurveSampler3D(Float64Path3D curve, Float64ScalarRange parameterRange, Float64AdaptivePath3DSamplingOptions samplingOptions, bool isPeriodic = false)
    {
        SamplingOptions = samplingOptions;
        IsPeriodic = isPeriodic;
        Curve = curve;
        ParameterRange = parameterRange;
        SampledCurve = curve.CreateAdaptiveCurve3D(parameterRange, samplingOptions);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               Curve.IsValid() &&
               SampledCurve.IsValid() &&
               (IsPeriodic && Count > 0 || !IsPeriodic && Count > 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AdaptiveCurveSampler3D SetCurve(Float64Path3D curve, Float64ScalarRange parameterRange, Float64AdaptivePath3DSamplingOptions samplingOptions, bool isPeriodic)
    {
        Curve = curve;
        ParameterRange = parameterRange;
        SamplingOptions = samplingOptions;
        IsPeriodic = isPeriodic;
        SampledCurve = curve.CreateAdaptiveCurve3D(parameterRange, samplingOptions);

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetParameterValues()
    {
        return SampledCurve.GetTimeValues();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64ScalarRange> GetParameterSections()
    {
        var tValues = SampledCurve.GetTimeValues().ToImmutableArray();

        var t1 = tValues[0];
        for (var i = 1; i < tValues.Length; i++)
        {
            var t2 = tValues[i];

            yield return Float64ScalarRange.Create(t1, t2);

            t1 = t2;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetPoints()
    {
        return SampledCurve.Curve.GetPoints();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetTangents()
    {
        return SampledCurve.GetTangents();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Path3DLocalFrame> GetFrames()
    {
        return SampledCurve;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Path3DLocalFrame> GetEnumerator()
    {
        return SampledCurve.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}