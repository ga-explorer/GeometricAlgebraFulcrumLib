using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncoderUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this ILinVector2D<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Vector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this ILinVector3D<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Vector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this LinVector<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Vector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this XGaVector<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Vector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this LinBivector2D<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Bivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this LinBivector3D<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Bivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this XGaBivector<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Bivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaTrivector<T>(this LinTrivector3D<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Trivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBlade<T>(this XGaKVector<T> egaKVector, CGaGeometricSpace<T> geometricSpace)
    {
        return geometricSpace.EncodeVGa.Blade(egaKVector);
    }

}