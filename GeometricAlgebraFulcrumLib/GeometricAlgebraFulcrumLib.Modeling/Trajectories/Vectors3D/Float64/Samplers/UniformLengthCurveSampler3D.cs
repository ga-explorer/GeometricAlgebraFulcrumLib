using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

public class UniformLengthCurveSampler3D :
    IParametricCurveSampler3D
{
    public Float64ArcLengthPath3D ArcLengthCurve { get; private set; }

    public Float64Path3D Curve
        => ArcLengthCurve;

    public Float64ScalarRange ParameterRange { get; private set; }

    public Float64ScalarRange LengthRange { get; private set; }

    public bool IsPeriodic { get; private set; }

    public double CurveSectionLength { get; private set; }

    public int Count { get; private set; }

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

            return Curve.GetFrame(
                ParameterRange.MinValue + index * CurveSectionLength
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UniformLengthCurveSampler3D(Float64ArcLengthPath3D curve, Float64ScalarRange parameterRange, int count, bool isPeriodic = false)
    {
        if (isPeriodic && count < 1 || !isPeriodic && count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        Count = count;
        IsPeriodic = isPeriodic;
        ArcLengthCurve = curve;
        ParameterRange = parameterRange;
        LengthRange = Float64ScalarRange.Create(
            curve.TimeToLength(parameterRange.MinValue),
            curve.TimeToLength(parameterRange.MaxValue)
        );
        CurveSectionLength = isPeriodic
            ? LengthRange.Length / count
            : LengthRange.Length / (count - 1);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               Curve.IsValid() &&
               (IsPeriodic && Count > 0 || !IsPeriodic && Count > 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UniformLengthCurveSampler3D SetCurve(Float64ArcLengthPath3D curve, Float64ScalarRange parameterRange, int count, bool isPeriodic)
    {
        if (isPeriodic && count < 1 || !isPeriodic && count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        ArcLengthCurve = curve;
        ParameterRange = parameterRange;
        Count = count;
        IsPeriodic = isPeriodic;
        LengthRange = Float64ScalarRange.Create(
            curve.TimeToLength(parameterRange.MinValue),
            curve.TimeToLength(parameterRange.MaxValue)
        );
        CurveSectionLength = isPeriodic
            ? LengthRange.Length / count
            : LengthRange.Length / (count - 1);

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetParameterValues()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i =>
                ArcLengthCurve.LengthToTime(
                    LengthRange.MinValue + i * CurveSectionLength
                ).ScalarValue
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64ScalarRange> GetParameterSections()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i =>
                Float64ScalarRange.Create(
                    ArcLengthCurve.LengthToTime(LengthRange.MinValue + i * CurveSectionLength),
                    ArcLengthCurve.LengthToTime(LengthRange.MinValue + (i + 1) * CurveSectionLength)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetPoints()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i =>
                Curve.GetValue(
                    ArcLengthCurve.LengthToTime(
                        LengthRange.MinValue + i * CurveSectionLength
                    )
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetTangents()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i =>
                Curve.GetTangent(
                    ArcLengthCurve.LengthToTime(
                        LengthRange.MinValue + i * CurveSectionLength
                    )
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Path3DLocalFrame> GetFrames()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i =>
                Curve.GetFrame(
                    ArcLengthCurve.LengthToTime(
                        LengthRange.MinValue + i * CurveSectionLength
                    )
                )
            );
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