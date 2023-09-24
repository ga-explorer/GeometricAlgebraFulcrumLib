using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space4D.Curves;

public class ConstantParametricCurve4D :
    IParametricCurve4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve4D Create(IFloat64Vector4D point)
    {
        return new ConstantParametricCurve4D(
            point, 
            Float64Vector4D.UnitSymmetric
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve4D Create(IFloat64Vector4D point, IFloat64Vector4D tangent)
    {
        return new ConstantParametricCurve4D(
            point, 
            tangent
        );
    }


    public Float64Vector4D Point { get; }
    
    public Float64Vector4D Tangent { get; }

    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricCurve4D(IFloat64Vector4D point, IFloat64Vector4D tangent)
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