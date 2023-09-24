using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors;

public class ConstantParametricBivector3D :
    IParametricBivector3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector3D Create(Float64Bivector3D point)
    {
        return new ConstantParametricBivector3D(
            point, 
            Float64Bivector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector3D Create(Float64Bivector3D point, Float64Bivector3D tangent)
    {
        return new ConstantParametricBivector3D(
            point, 
            tangent
        );
    }


    public Float64Bivector3D Point { get; }
    
    public Float64Bivector3D Tangent { get; }

    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricBivector3D(Float64Bivector3D point, Float64Bivector3D tangent)
    {
        Point = point;
        Tangent = tangent;

        Debug.Assert(IsValid());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() && 
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector3D GetBivector(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector3D GetDerivative1Bivector(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IParametricCurve3D GetDualVectorCurve()
    {
        return ConstantParametricCurve3D.Create(
            Point.Dual3D(),
            Tangent.Dual3D()
        );
    }
}