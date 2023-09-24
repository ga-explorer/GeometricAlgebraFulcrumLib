using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;

public class ConstantParametricCurve3D :
    IParametricCurve3D
{
    public static ConstantParametricCurve3D ZeroPointCurve { get; }
        = new ConstantParametricCurve3D(
            Float64ScalarRange.Infinite,
            Float64Vector3D.Zero, 
            Float64Vector3D.Zero
        );

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve3D Create(double x, double y, double z)
    {
        return new ConstantParametricCurve3D(
            Float64ScalarRange.Infinite,
            Float64Vector3D.Create(x, y, z), 
            Float64Vector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve3D Create(IFloat64Vector3D point)
    {
        return new ConstantParametricCurve3D(
            Float64ScalarRange.Infinite,
            point, 
            Float64Vector3D.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve3D Create(Float64ScalarRange parameterRange, IFloat64Vector3D point)
    {
        return new ConstantParametricCurve3D(
            parameterRange,
            point, 
            Float64Vector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve3D Create(IFloat64Vector3D point, IFloat64Vector3D tangent)
    {
        return new ConstantParametricCurve3D(
            Float64ScalarRange.Infinite,
            point, 
            tangent
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve3D Create(Float64ScalarRange parameterRange, IFloat64Vector3D point, IFloat64Vector3D tangent)
    {
        return new ConstantParametricCurve3D(
            parameterRange,
            point, 
            tangent
        );
    }


    public Float64Vector3D Point { get; }
    
    public Float64Vector3D Tangent { get; }

    public Float64ScalarRange ParameterRange { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricCurve3D(Float64ScalarRange parameterRange, IFloat64Vector3D point, IFloat64Vector3D tangent)
    {
        Point = point.ToVector3D();
        Tangent = tangent.ToVector3D();
        ParameterRange = parameterRange;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               Point.IsValid() && 
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetPoint(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetDerivative1Point(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            Point,
            Tangent
        );
    }
}