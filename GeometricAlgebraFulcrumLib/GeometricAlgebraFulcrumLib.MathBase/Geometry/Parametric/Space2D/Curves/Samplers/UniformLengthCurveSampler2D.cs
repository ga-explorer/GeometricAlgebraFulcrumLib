using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Samplers;

public class UniformLengthCurveSampler2D :
    IParametricCurveSampler2D
{
    public IArcLengthCurve2D ArcLengthCurve { get; private set; }

    public IParametricCurve2D Curve 
        => ArcLengthCurve;

    public Float64Range1D ParameterRange { get; private set; }

    public Float64Range1D LengthRange { get; private set; }

    public bool IsPeriodic { get; private set; }

    public double CurveSectionLength { get; private set; }

    public int Count { get; private set; }

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
                ParameterRange.MinValue + index * CurveSectionLength
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UniformLengthCurveSampler2D(IArcLengthCurve2D curve, Float64Range1D parameterRange, int count, bool isPeriodic = false)
    {
        if ((isPeriodic && count < 1) || (!isPeriodic && count < 2))
            throw new ArgumentOutOfRangeException(nameof(count));

        Count = count;
        IsPeriodic = isPeriodic;
        ArcLengthCurve = curve;
        ParameterRange = parameterRange;
        LengthRange = Float64Range1D.Create(
            curve.ParameterToLength(parameterRange.MinValue),
            curve.ParameterToLength(parameterRange.MaxValue)
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
               ((IsPeriodic && Count > 0) || (!IsPeriodic && Count > 1));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UniformLengthCurveSampler2D SetCurve(IArcLengthCurve2D curve, Float64Range1D parameterRange, int count, bool isPeriodic)
    {
        if ((isPeriodic && count < 1) || (!isPeriodic && count < 2))
            throw new ArgumentOutOfRangeException(nameof(count));

        ArcLengthCurve = curve;
        ParameterRange = parameterRange;
        Count = count;
        IsPeriodic = isPeriodic;
        LengthRange = Float64Range1D.Create(
            curve.ParameterToLength(parameterRange.MinValue),
            curve.ParameterToLength(parameterRange.MaxValue)
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
                ArcLengthCurve.LengthToParameter(
                    LengthRange.MinValue + i * CurveSectionLength
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Range1D> GetParameterSections()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => 
                Float64Range1D.Create(
                    ArcLengthCurve.LengthToParameter(LengthRange.MinValue + i * CurveSectionLength),
                    ArcLengthCurve.LengthToParameter(LengthRange.MinValue + (i + 1) * CurveSectionLength)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetPoints()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => 
                Curve.GetPoint(
                    ArcLengthCurve.LengthToParameter(
                        LengthRange.MinValue + i * CurveSectionLength
                    )
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetTangents()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => 
                Curve.GetTangent(
                    ArcLengthCurve.LengthToParameter(
                        LengthRange.MinValue + i * CurveSectionLength
                    )
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ParametricCurveLocalFrame2D> GetFrames()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => 
                Curve.GetFrame(
                    ArcLengthCurve.LengthToParameter(
                        LengthRange.MinValue + i * CurveSectionLength
                    )
                )
            );
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