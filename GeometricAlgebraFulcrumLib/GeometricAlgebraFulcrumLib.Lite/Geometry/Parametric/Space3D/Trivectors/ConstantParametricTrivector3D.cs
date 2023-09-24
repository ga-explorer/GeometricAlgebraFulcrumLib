using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Trivectors;

public class ConstantParametricTrivector3D :
    IParametricTrivector3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricTrivector3D Create(Float64Trivector3D point)
    {
        return new ConstantParametricTrivector3D(
            point, 
            Float64Trivector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricTrivector3D Create(Float64Trivector3D point, Float64Trivector3D tangent)
    {
        return new ConstantParametricTrivector3D(
            point, 
            tangent
        );
    }


    public Float64Trivector3D Point { get; }
    
    public Float64Trivector3D Tangent { get; }

    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricTrivector3D(Float64Trivector3D point, Float64Trivector3D tangent)
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
    public Float64Trivector3D GetTrivector(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Trivector3D GetDerivative1Trivector(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IParametricScalar GetDualScalarCurve()
    {
        return ConstantParametricScalar.Create(
            Point.Dual3D().Scalar.Value
        );
    }
}