using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeIpnsDirection(
            cgaGeometricSpace.EncodeVGaVector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector2D<T> egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeIpnsDirection(
            cgaGeometricSpace.EncodeVGaBivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeIpnsDirection(
            cgaGeometricSpace.EncodeVGaVector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector3D<T> egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeIpnsDirection(
            cgaGeometricSpace.EncodeVGaBivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinTrivector3D<T> egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeIpnsDirection(
            cgaGeometricSpace.EncodeVGaTrivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeIpnsDirection(
            cgaGeometricSpace.EncodeVGaVector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaKVector<T> egaDirectionBlade)
    {
        return cgaGeometricSpace.EncodeIpnsDirection(
            cgaGeometricSpace.EncodeVGaBlade(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, CGaBlade<T> egaDirectionBlade)
    {
        return -egaDirectionBlade.VGaDual().Op(cgaGeometricSpace.Ei);
    }

}