using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64OpnsFlatBladeDecoder :
    CGaFloat64BladeDecoderBase
{
    internal CGaFloat64OpnsFlatBladeDecoder(CGaFloat64Blade blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Line2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        return HyperPlane(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Line2D(LinFloat64Vector2D egaProbePoint)
    {
        return HyperPlane(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Plane3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        return HyperPlane(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Plane3D(LinFloat64Vector3D egaProbePoint)
    {
        return HyperPlane(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat HyperPlane()
    {
        return HyperPlane(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat HyperPlane(CGaFloat64Blade egaProbePoint)
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Element(LinFloat64Vector2D egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Element(LinFloat64Vector3D egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Element(LinFloat64Vector egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    public CGaFloat64Flat Element(CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Lcp(Blade)
                .Gp(Blade.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(Blade.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei.Lcp(Blade.Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new CGaFloat64Flat(
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
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlaneVGaDirection()
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneVGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaDirection()
    {
        return Blade.GeometricSpace.Ei
            .Lcp(Blade)
            .GetVGaPart(true)
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlaneVGaNormalDirection()
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneVGaNormalDirection();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D VGaNormalVector2D()
    {
        return VGaDirection().VGaNormal().Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D VGaNormalVector3D()
    {
        return VGaDirection().VGaNormal().Decode.VGaDirection.Vector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D VGaNormalBivector3D()
    {
        return VGaDirection().VGaNormal().Decode.VGaDirection.Bivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaNormalDirection()
    {
        return VGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineVGaPosition2D(LinFloat64Vector2D egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PlaneVGaPosition3D(LinFloat64Vector3D egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlaneVGaPosition(LinFloat64Vector egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlaneVGaPosition()
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlaneVGaPosition(CGaFloat64Blade egaProbePoint)
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition()
    {
        return VGaPosition(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D VGaPosition2D()
    {
        return VGaPosition().Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D VGaPosition2D(LinFloat64Vector2D egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        ).Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D VGaPosition3D()
    {
        return VGaPosition().Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D VGaPosition3D(LinFloat64Vector3D egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        ).Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(LinFloat64Vector2D egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(LinFloat64Vector3D egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D VGaPosition(LinFloat64Vector egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        ).Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(CGaFloat64Blade egaProbePointBlade)
    {
        return egaProbePointBlade
            .VGaVectorToIpnsPoint()
            .Lcp(Blade)
            .Gp(Blade.Inverse())
            .GetVectorPart((int i) => i >= 2)
            .ToConformalBlade(Blade.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double HyperPlaneWeight()
    {
        return Blade.CGaDual().Decode.IpnsFlat.HyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight2D(LinFloat64Vector2D egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight3D(LinFloat64Vector3D egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(CGaFloat64Blade egaProbePoint)
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