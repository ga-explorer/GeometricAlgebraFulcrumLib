using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;

public class ParameterListCurveSampler3D :
    IParametricCurveSampler3D
{
    public Float64Path3D Curve { get; private set; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Create(
            ParameterValueSet[0],
            ParameterValueSet[^1]
        );

    public bool IsPeriodic { get; private set; }

    public ImmutableSortedSet<Float64Scalar> ParameterValueSet { get; private set; }

    public int Count
        => ParameterValueSet.Count;

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
                ParameterValueSet[index]
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterListCurveSampler3D(Float64Path3D curve, ImmutableSortedSet<Float64Scalar> parameterValueSet, bool isPeriodic = false)
    {
        IsPeriodic = isPeriodic;
        Curve = curve;
        ParameterValueSet = parameterValueSet;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               Curve.IsValid() &&
               ParameterValueSet.Count > 0 &&
               ParameterValueSet[0] >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterListCurveSampler3D SetCurve(Float64Path3D curve, ImmutableSortedSet<Float64Scalar> parameterValueSet, bool isPeriodic)
    {
        IsPeriodic = isPeriodic;
        Curve = curve;
        ParameterValueSet = parameterValueSet;

        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetParameterValues()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => ParameterValueSet[i].ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64ScalarRange> GetParameterSections()
    {
        if (IsPeriodic)
            return Enumerable
                .Range(0, Count)
                .Select(i =>
                    Float64ScalarRange.Create(
                        ParameterValueSet[i],
                        ParameterValueSet[(i + 1).Mod(Count)]
                    )
                );

        return Enumerable
            .Range(0, Count - 1)
            .Select(i =>
                Float64ScalarRange.Create(
                    ParameterValueSet[i],
                    ParameterValueSet[i + 1]
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetPoints()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => Curve.GetValue(ParameterValueSet[i]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector3D> GetTangents()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => Curve.GetTangent(ParameterValueSet[i]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Path3DLocalFrame> GetFrames()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => Curve.GetFrame(ParameterValueSet[i]));
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