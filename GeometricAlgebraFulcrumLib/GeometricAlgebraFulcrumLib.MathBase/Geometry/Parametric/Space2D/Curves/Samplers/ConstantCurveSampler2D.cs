using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Samplers;

public class ConstantCurveSampler2D :
    IParametricCurveSampler2D
{
    public ConstantParametricCurve2D ConstantCurve { get; private set; }

    public IParametricCurve2D Curve 
        => ConstantCurve;

    public Float64Vector2D Point 
        => ConstantCurve.Point;

    public Float64Vector2D Tangent 
        => ConstantCurve.Tangent;

    public Float64Range1D ParameterRange { get; private set; }

    public bool IsPeriodic 
        => true;
    
    public int Count 
        => 2;

    public ParametricCurveLocalFrame2D this[int index]
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
    public ConstantCurveSampler2D(IFloat64Vector2D point, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve2D.Create(point, Float64Vector2D.E1);
        ParameterRange = parameterRange;
        
        Debug.Assert(IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler2D(IFloat64Vector2D point, IFloat64Vector2D tangent, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve2D.Create(point, tangent);
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
    public ConstantCurveSampler2D SetCurve(IFloat64Vector2D point, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve2D.Create(point, Float64Vector2D.E1);
        ParameterRange = parameterRange;
        
        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler2D SetCurve(IFloat64Vector2D point, IFloat64Vector2D tangent, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve2D.Create(point, tangent);
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
    public IEnumerable<Float64Range1D> GetParameterSections()
    {
        yield return ParameterRange;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetPoints()
    {
        yield return Point;
        yield return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Vector2D> GetTangents()
    {
        yield return Tangent;
        yield return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ParametricCurveLocalFrame2D> GetFrames()
    {
        yield return ConstantCurve.GetFrame(ParameterRange.MinValue);
        yield return ConstantCurve.GetFrame(ParameterRange.MaxValue);
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