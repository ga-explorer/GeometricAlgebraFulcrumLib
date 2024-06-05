using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space4D.Curves;

public class ConstantParametricCurve4D :
    IParametricCurve4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve4D Create(ILinFloat64Vector4D point)
    {
        return new ConstantParametricCurve4D(
            point, 
            LinFloat64Vector4D.UnitSymmetric
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve4D Create(ILinFloat64Vector4D point, ILinFloat64Vector4D tangent)
    {
        return new ConstantParametricCurve4D(
            point, 
            tangent
        );
    }


    public LinFloat64Vector4D Point { get; }
    
    public LinFloat64Vector4D Tangent { get; }

    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricCurve4D(ILinFloat64Vector4D point, ILinFloat64Vector4D tangent)
    {
        Point = LinFloat64Vector4DUtils.ToLinVector4D(point);
        Tangent = LinFloat64Vector4DUtils.ToLinVector4D(tangent);

        Debug.Assert(IsValid());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() && 
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetPoint(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetDerivative1Point(double parameterValue)
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