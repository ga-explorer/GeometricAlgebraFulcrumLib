using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Bivectors;

public class ConstantParametricBivector2D :
    IParametricBivector2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector2D Create(LinFloat64Bivector2D point)
    {
        return new ConstantParametricBivector2D(
            point, 
            LinFloat64Bivector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector2D Create(LinFloat64Bivector2D point, LinFloat64Bivector2D tangent)
    {
        return new ConstantParametricBivector2D(
            point, 
            tangent
        );
    }


    public LinFloat64Bivector2D Point { get; }
    
    public LinFloat64Bivector2D Tangent { get; }

    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricBivector2D(LinFloat64Bivector2D point, LinFloat64Bivector2D tangent)
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
    public LinFloat64Bivector2D GetBivector(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GetDerivative1Bivector(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64ParametricScalar GetDualScalarCurve()
    {
        return ConstantParametricScalar.Create(
            Point.Dual2D().Scalar.ScalarValue
        );
    }
}