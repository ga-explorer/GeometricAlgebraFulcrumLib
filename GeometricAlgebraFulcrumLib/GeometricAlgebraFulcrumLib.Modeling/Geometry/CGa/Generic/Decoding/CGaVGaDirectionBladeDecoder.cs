using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public sealed class CGaVGaDirectionBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    internal CGaVGaDirectionBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }
    

    public CGaDirection<T> Direction()
    {
        Debug.Assert(Blade.IsVGaBlade());

        var weight = Blade.Norm();

        if (weight.IsZero())
            return new CGaDirection<T>(
                Blade.GeometricSpace,
                Blade.GeometricSpace.ScalarProcessor.Zero,
                Blade.GeometricSpace.OneScalarBlade
            );

        return new CGaDirection<T>(
            Blade.GeometricSpace,
            weight,
            Blade.Divide(weight)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Vector2D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is4D &&
        //    Blade.IsVGaVector()
        //);

        return LinVector2D<T>.Create(
            Blade.InternalKVector[2],
            Blade.InternalKVector[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> Vector3D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is5D &&
        //    Blade.IsVGaVector()
        //);

        return LinVector3D<T>.Create(
            Blade.InternalKVector[2],
            Blade.InternalKVector[3],
            Blade.InternalKVector[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> Vector4D()
    {
        Debug.Assert(
            Blade.VSpaceDimensions == 6 &&
            Blade.IsVGaVector()
        );

        return LinVector4D<T>.Create(
            Blade.InternalKVector[2],
            Blade.InternalKVector[3],
            Blade.InternalKVector[4],
            Blade.InternalKVector[5]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> VectorND()
    {
        return Blade.InternalVector.DecodeVGaBladeToLinVector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Vector()
    {
        return Blade.InternalVector.DecodeVGaBladeToXGaVector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Bivector2D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is4D &&
        //    Blade.IsVGaBivector()
        //);

        return LinBivector2D<T>.Create(
            Blade.InternalKVector[2, 3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Bivector3D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is5D &&
        //    Blade.IsVGaBivector()
        //);

        return LinBivector3D<T>.Create(
            Blade.InternalKVector[2, 3],
            Blade.InternalKVector[2, 4],
            Blade.InternalKVector[3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Bivector()
    {
        return Blade.InternalBivector.DecodeVGaBladeToXGaBivector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> Trivector3D()
    {
        //Debug.Assert(
        //    Blade.ConformalSpace.Is5D &&
        //    Blade.IsVGaTrivector()
        //);

        return LinTrivector3D<T>.Create(
            Blade.InternalKVector[2, 3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> KVector()
    {
        return Blade.InternalKVector.DecodeVGaBladeToXGaKVector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> BladeToVectors2D()
    {
        return BladeToVectors().Select(
            v => LinVector2D<T>.Create(v[0], v[1])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> BladeToVectors3D()
    {
        return BladeToVectors().Select(
            v => LinVector3D<T>.Create(v[0], v[1], v[2])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector<T>> BladeToVectorsND()
    {
        return KVector()
            .BladeToVectors()
            .SelectToImmutableArray(v => v.ToLinVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> BladeToVectors()
    {
        return KVector().BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> BladeToVectorVGaBlades()
    {
        return BladeToVectors().SelectToImmutableArray(Blade.GeometricSpace.EncodeVGa.Vector);
    }

}