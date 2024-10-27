using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public sealed class CGaOpnsFlatBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    internal CGaOpnsFlatBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Line2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        return HyperPlane(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Line2D(LinVector2D<T> egaProbePoint)
    {
        return HyperPlane(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Plane3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        return HyperPlane(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Plane3D(LinVector3D<T> egaProbePoint)
    {
        return HyperPlane(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> HyperPlane()
    {
        return HyperPlane(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> HyperPlane(CGaBlade<T> egaProbePoint)
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Element(LinVector2D<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Element(LinVector3D<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Element(LinVector<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    public CGaFlat<T> Element(CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Lcp(Blade)
                .Gp(Blade.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(Blade.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei.Lcp(Blade.Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            position,
            directionOpEi.GetVGaPart(true)
        );

        Debug.Assert(
            flat.PositionToIpnsPoint().Op(Blade).IsNearZero()
        );

        return flat;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperPlaneVGaDirection()
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneVGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaDirection()
    {
        return Blade.GeometricSpace.Ei
            .Lcp(Blade)
            .GetVGaPart(true)
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperPlaneVGaNormalDirection()
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneVGaNormalDirection();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> VGaNormalAsVector2D()
    {
        return VGaDirection().VGaNormal().Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> VGaNormalAsVector3D()
    {
        return VGaDirection().VGaNormal().Decode.VGaDirection.Vector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> VGaNormalAsBivector3D()
    {
        return VGaDirection().VGaNormal().Decode.VGaDirection.Bivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaNormalDirection()
    {
        return VGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> LineVGaPosition2D(LinVector2D<T> egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PlaneVGaPosition3D(LinVector3D<T> egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperPlaneVGaPosition(LinVector<T> egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperPlaneVGaPosition()
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperPlaneVGaPosition(CGaBlade<T> egaProbePoint)
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition()
    {
        return VGaPosition(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> VGaPositionAsVector2D()
    {
        return VGaPosition().Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> VGaPositionAsVector2D(LinVector2D<T> egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        ).Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> VGaPositionAsVector3D()
    {
        return VGaPosition().Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> VGaPositionAsVector3D(LinVector3D<T> egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        ).Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(LinVector2D<T> egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(LinVector3D<T> egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> VGaPositionAsVector3D(LinVector<T> egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        ).Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(CGaBlade<T> egaProbePointBlade)
    {
        return egaProbePointBlade
            .VGaVectorToIpnsPoint()
            .Lcp(Blade)
            .Gp(Blade.Inverse())
            .GetVectorPart()
            .GetVectorPart(i => i >= 2)
            .ToConformalBlade(Blade.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> HyperPlaneWeight()
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight2D(LinVector2D<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight3D(LinVector3D<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(LinVector<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(CGaBlade<T> egaProbePoint)
    {
        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var directionOpEi =
            Blade.GeometricSpace.Ei.Lcp(Blade.Negative());

        return ipnsProbePoint
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


}