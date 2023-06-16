using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Samplers;

public class ParameterListCurveSampler2D :
    IParametricCurveSampler2D
{
    public IParametricCurve2D Curve { get; private set; }

    public Float64Range1D ParameterRange
        => Float64Range1D.Create(
            ParameterValueSet[0], 
            ParameterValueSet[^1]
        );

    public bool IsPeriodic { get; private set; }

    public ImmutableSortedSet<double> ParameterValueSet { get; private set; }

    public int Count 
        => ParameterValueSet.Count;

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

            return Curve.GetFrame(
                ParameterValueSet[index]
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterListCurveSampler2D(IParametricCurve2D curve, ImmutableSortedSet<double> parameterValueSet, bool isPeriodic = false)
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
    public ParameterListCurveSampler2D SetCurve(IParametricCurve2D curve, ImmutableSortedSet<double> parameterValueSet, bool isPeriodic)
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
            .Select(i => ParameterValueSet[i]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Range1D> GetParameterSections()
    {
        if (IsPeriodic)
            return Enumerable
                .Range(0, Count)
                .Select(i => 
                    Float64Range1D.Create(
                        ParameterValueSet[i],
                        ParameterValueSet[(i + 1).Mod(Count)]
                    )
                );

        return Enumerable
            .Range(0, Count - 1)
            .Select(i => 
                Float64Range1D.Create(
                    ParameterValueSet[i],
                    ParameterValueSet[i + 1]
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetPoints()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => Curve.GetPoint(ParameterValueSet[i]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetTangents()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => Curve.GetTangent(ParameterValueSet[i]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ParametricCurveLocalFrame2D> GetFrames()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => Curve.GetFrame(ParameterValueSet[i]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<ParametricCurveLocalFrame2D> GetEnumerator()
    {
        return GetFrames().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}