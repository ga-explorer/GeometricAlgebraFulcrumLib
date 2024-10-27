using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public class CGaFloat64IpnsDirectionEncoder :
    CGaFloat64EncoderBase
{
    internal CGaFloat64IpnsDirectionEncoder(CGaFloat64GeometricSpace geometricSpace) 
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(LinFloat64Vector2D egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.Encode.VGa.Vector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Bivector(LinFloat64Bivector2D egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.Encode.VGa.Bivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(LinFloat64Vector3D egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.Encode.VGa.Vector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Bivector(LinFloat64Bivector3D egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.Encode.VGa.Bivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Trivector(LinFloat64Trivector3D egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.Encode.VGa.Trivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(LinFloat64Vector egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.Encode.VGa.Vector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Blade(RGaFloat64KVector egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.Encode.VGa.Blade(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Blade(CGaFloat64Blade egaDirectionBlade)
    {
        return -egaDirectionBlade.VGaDual().Op(GeometricSpace.Ei);
    }

}