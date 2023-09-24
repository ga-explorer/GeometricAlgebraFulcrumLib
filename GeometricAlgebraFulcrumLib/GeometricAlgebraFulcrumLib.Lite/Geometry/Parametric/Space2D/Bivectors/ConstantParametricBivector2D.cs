using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Bivectors;

public class ConstantParametricBivector2D :
    IParametricBivector2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector2D Create(Float64Bivector2D point)
    {
        return new ConstantParametricBivector2D(
            point, 
            Float64Bivector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector2D Create(Float64Bivector2D point, Float64Bivector2D tangent)
    {
        return new ConstantParametricBivector2D(
            point, 
            tangent
        );
    }


    public Float64Bivector2D Point { get; }
    
    public Float64Bivector2D Tangent { get; }

    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricBivector2D(Float64Bivector2D point, Float64Bivector2D tangent)
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
    public Float64Bivector2D GetBivector(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D GetDerivative1Bivector(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IParametricScalar GetDualScalarCurve()
    {
        return ConstantParametricScalar.Create(
            Point.Dual2D().Scalar.Value
        );
    }
}