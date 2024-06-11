using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public static class CGaFloat64EncodeOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeOpnsDirection(
            cgaGeometricSpace.EncodeVGaVector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector2D egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeOpnsDirection(
            cgaGeometricSpace.EncodeVGaBivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeOpnsDirection(
            cgaGeometricSpace.EncodeVGaVector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector3D egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeOpnsDirection(
            cgaGeometricSpace.EncodeVGaBivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Trivector3D egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeOpnsDirection(
            cgaGeometricSpace.EncodeVGaTrivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeOpnsDirection(
            cgaGeometricSpace.EncodeVGaVector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64KVector egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeOpnsDirection(
            cgaGeometricSpace.EncodeVGaBlade(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64Blade egaDirectionBlade)
    {
        return egaDirectionBlade.Op(cgaGeometricSpace.Ei);
    }

}