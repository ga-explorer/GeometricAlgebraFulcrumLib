using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Samplers;

public class UniformLengthCurveSampler3D :
    IParametricCurveSampler3D
{
    public IArcLengthCurve3D ArcLengthCurve { get; private set; }

    public IParametricCurve3D Curve 
        => ArcLengthCurve;

    public Float64ScalarRange ParameterRange { get; private set; }

    public Float64ScalarRange LengthRange { get; private set; }

    public bool IsPeriodic { get; private set; }

    public double CurveSectionLength { get; private set; }

    public int Count { get; private set; }

    public ParametricCurveLocalFrame3D this[int index]
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
    public UniformLengthCurveSampler3D(IArcLengthCurve3D curve, Float64ScalarRange parameterRange, int count, bool isPeriodic = false)
    {
        if ((isPeriodic && count < 1) || (!isPeriodic && count < 2))
            throw new ArgumentOutOfRangeException(nameof(count));

        Count = count;
        IsPeriodic = isPeriodic;
        ArcLengthCurve = curve;
        ParameterRange = parameterRange;
        LengthRange = Float64ScalarRange.Create(
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
    public UniformLengthCurveSampler3D SetCurve(IArcLengthCurve3D curve, Float64ScalarRange parameterRange, int count, bool isPeriodic)
    {
        if ((isPeriodic && count < 1) || (!isPeriodic && count < 2))
            throw new ArgumentOutOfRangeException(nameof(count));

        ArcLengthCurve = curve;
        ParameterRange = parameterRange;
        Count = count;
        IsPeriodic = isPeriodic;
        LengthRange = Float64ScalarRange.Create(
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
    public IEnumerable<Float64Scalar> GetParameterValues()
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
    public IEnumerable<Float64ScalarRange> GetParameterSections()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => 
                Float64ScalarRange.Create(
                    ArcLengthCurve.LengthToParameter(LengthRange.MinValue + i * CurveSectionLength),
                    ArcLengthCurve.LengthToParameter(LengthRange.MinValue + (i + 1) * CurveSectionLength)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector3D> GetPoints()
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
    public IEnumerable<Float64Vector3D> GetTangents()
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
    public IEnumerable<ParametricCurveLocalFrame3D> GetFrames()
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
    public IEnumerator<ParametricCurveLocalFrame3D> GetEnumerator()
    {
        return GetFrames().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}