using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public sealed class CGaOpnsDirectionEncoder<T> :
    CGaEncoderBase<T>
{
    internal CGaOpnsDirectionEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(LinVector2D<T> egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.EncodeVGa.Vector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(LinBivector2D<T> egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.EncodeVGa.Bivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(LinVector3D<T> egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.EncodeVGa.Vector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(LinBivector3D<T> egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.EncodeVGa.Bivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Trivector(LinTrivector3D<T> egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.EncodeVGa.Trivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(LinVector<T> egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.EncodeVGa.Vector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaKVector<T> egaDirectionBlade)
    {
        return Blade(
            GeometricSpace.EncodeVGa.Blade(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(CGaBlade<T> egaDirectionBlade)
    {
        return egaDirectionBlade.Op(GeometricSpace.Ei);
    }

}