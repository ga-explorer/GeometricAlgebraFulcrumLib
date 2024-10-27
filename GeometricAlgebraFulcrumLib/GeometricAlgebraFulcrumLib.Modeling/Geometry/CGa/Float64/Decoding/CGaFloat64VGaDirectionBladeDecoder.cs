using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64VGaDirectionBladeDecoder :
    CGaFloat64BladeDecoderBase
{
    internal CGaFloat64VGaDirectionBladeDecoder(CGaFloat64Blade blade) 
        : base(blade)
    {
    }

    
    public CGaFloat64Direction Element()
    {
        Debug.Assert(Blade.IsVGaBlade());

        var weight = Blade.Norm();

        if (weight.IsZero())
            return new CGaFloat64Direction(
                Blade.GeometricSpace,
                0d,
                Blade.GeometricSpace.OneScalarBlade
            );

        return new CGaFloat64Direction(
            Blade.GeometricSpace,
            weight,
            Blade.Divide(weight)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Vector2D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is4D &&
        //    Blade.IsVGaVector()
        //);

        return LinFloat64Vector2D.Create(
            Blade.InternalKVector[2],
            Blade.InternalKVector[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D Vector3D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is5D &&
        //    Blade.IsVGaVector()
        //);

        return LinFloat64Vector3D.Create(
            Blade.InternalKVector[2],
            Blade.InternalKVector[3],
            Blade.InternalKVector[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D Vector4D()
    {
        Debug.Assert(
            Blade.VSpaceDimensions == 6 &&
            Blade.IsVGaVector()
        );

        return LinFloat64Vector4D.Create(
            Blade.InternalKVector[2],
            Blade.InternalKVector[3],
            Blade.InternalKVector[4],
            Blade.InternalKVector[5]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector Vector()
    {
        return Blade.InternalVector.DecodeVGaBladeToLinVector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector RGaVector()
    {
        return Blade.InternalVector.DecodeVGaBladeToRGaVector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D Bivector2D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is4D &&
        //    Blade.IsVGaBivector()
        //);

        return LinFloat64Bivector2D.Create(
            Blade.InternalKVector[2, 3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D Bivector3D()
    {
        //Debug.Assert(
        //    //Blade.ConformalSpace.Is5D &&
        //    Blade.IsVGaBivector()
        //);

        return LinFloat64Bivector3D.Create(
            Blade.InternalKVector[2, 3],
            Blade.InternalKVector[2, 4],
            Blade.InternalKVector[3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Bivector()
    {
        return Blade.InternalBivector.DecodeVGaBladeToRGaBivector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D Trivector3D()
    {
        //Debug.Assert(
        //    Blade.ConformalSpace.Is5D &&
        //    Blade.IsVGaTrivector()
        //);

        return LinFloat64Trivector3D.Create(
            Blade.InternalKVector[2, 3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector KVector()
    {
        return Blade.InternalKVector.DecodeVGaBladeToRGaKVector(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector2D> BladeToVectors2D()
    {
        return BladeToVectors().Select(
            v => LinFloat64Vector2D.Create(v[0], v[1])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector3D> BladeToVectors3D()
    {
        return BladeToVectors().Select(
            v => LinFloat64Vector3D.Create(v[0], v[1], v[2])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector> BladeToVectorsND()
    {
        return KVector()
            .BladeToVectors()
            .SelectToImmutableArray(v => v.ToLinVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> BladeToVectors()
    {
        return KVector()
            .BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> BladeToVectorVGaBlades()
    {
        return BladeToVectors()
            .SelectToImmutableArray(Blade.GeometricSpace.Encode.VGa.Vector);
    }

}