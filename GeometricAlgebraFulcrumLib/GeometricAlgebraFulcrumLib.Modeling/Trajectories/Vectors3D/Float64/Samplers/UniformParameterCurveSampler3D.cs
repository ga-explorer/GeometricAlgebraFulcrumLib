using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

public class UniformParameterCurveSampler3D :
    IParametricCurveSampler3D
{
    public Float64Path3D Curve { get; private set; }

    public Float64ScalarRange ParameterRange { get; private set; }

    public bool IsPeriodic { get; private set; }

    public double ParameterSectionLength { get; private set; }

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
                ParameterRange.MinValue + index * ParameterSectionLength
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UniformParameterCurveSampler3D(Float64Path3D curve, Float64ScalarRange parameterRange, int count, bool isPeriodic = false)
    {
        if (isPeriodic && count < 1 || !isPeriodic && count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        Count = count;
        IsPeriodic = isPeriodic;
        Curve = curve;
        ParameterRange = parameterRange;
        ParameterSectionLength = isPeriodic
            ? parameterRange.Length / count
            : parameterRange.Length / (count - 1);

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
    public UniformParameterCurveSampler3D SetCurve(Float64Path3D curve, Float64ScalarRange parameterRange, int count, bool isPeriodic)
    {
        if (isPeriodic && count < 1 || !isPeriodic && count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        Curve = curve;
        ParameterRange = parameterRange;
        Count = count;
        IsPeriodic = isPeriodic;
        ParameterSectionLength = isPeriodic
            ? ParameterRange.Length / count
            : ParameterRange.Length / (count - 1);

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetParameterValues()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i =>
                ParameterRange.MinValue + i * ParameterSectionLength
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64ScalarRange> GetParameterSections()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i =>
                Float64ScalarRange.Create(
                    ParameterRange.MinValue + i * ParameterSectionLength,
                    ParameterRange.MinValue + (i + 1) * ParameterSectionLength
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
                    ParameterRange.MinValue + i * ParameterSectionLength
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
                    ParameterRange.MinValue + i * ParameterSectionLength
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
                    ParameterRange.MinValue + i * ParameterSectionLength
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