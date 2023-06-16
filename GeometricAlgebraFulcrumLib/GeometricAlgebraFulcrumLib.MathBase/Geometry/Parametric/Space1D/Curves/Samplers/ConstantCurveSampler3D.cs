using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves.Samplers;

public class ConstantCurveSampler1D :
    IParametricCurveSampler1D
{
    public ConstantParametricCurve1D ConstantCurve { get; private set; }

    public IParametricCurve1D Curve 
        => ConstantCurve;

    public double Point 
        => ConstantCurve.Point;

    public double Tangent 
        => ConstantCurve.Tangent;

    public Float64Range1D ParameterRange { get; private set; }

    public bool IsPeriodic 
        => true;
    
    public int Count 
        => 2;

    public ParametricCurveLocalFrame1D this[int index]
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
    public ConstantCurveSampler1D(double point, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve1D.Create(point, 1);
        ParameterRange = parameterRange;
        
        Debug.Assert(IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler1D(double point, double tangent, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve1D.Create(point, tangent);
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
    public ConstantCurveSampler1D SetCurve(double point, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve1D.Create(point, 1);
        ParameterRange = parameterRange;
        
        Debug.Assert(IsValid());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstantCurveSampler1D SetCurve(double point, double tangent, Float64Range1D parameterRange)
    {
        ConstantCurve = ConstantParametricCurve1D.Create(point, tangent);
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
    public IEnumerable<ParametricCurveLocalFrame1D> GetFrames()
    {
        yield return ConstantCurve.GetFrame(ParameterRange.MinValue);
        yield return ConstantCurve.GetFrame(ParameterRange.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<ParametricCurveLocalFrame1D> GetEnumerator()
    {
        return GetFrames().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}