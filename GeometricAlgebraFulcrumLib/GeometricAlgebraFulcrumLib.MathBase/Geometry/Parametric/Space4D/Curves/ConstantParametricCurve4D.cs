using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space4D.Curves;

public class ConstantParametricCurve4D :
    IParametricCurve4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve4D Create(IFloat64Tuple4D point)
    {
        return new ConstantParametricCurve4D(
            point, 
            Float64Vector4D.UnitSymmetric
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve4D Create(IFloat64Tuple4D point, IFloat64Tuple4D tangent)
    {
        return new ConstantParametricCurve4D(
            point, 
            tangent
        );
    }


    public Float64Vector4D Point { get; }
    
    public Float64Vector4D Tangent { get; }

    public Float64Range1D ParameterRange 
        => Float64Range1D.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricCurve4D(IFloat64Tuple4D point, IFloat64Tuple4D tangent)
    {
        Point = point.ToTuple4D();
        Tangent = tangent.ToTuple4D();

        Debug.Assert(IsValid());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() && 
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector4D GetPoint(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector4D GetDerivative1Point(double parameterValue)
    {
        return Tangent;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ParametricCurveLocalFrame4D GetFrame(double parameterValue)
    //{
    //    return ParametricCurveLocalFrame4D.Create(
    //        parameterValue,
    //        Point,
    //        Tangent
    //    );
    //}
}