using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64IpnsFlatBladeDecoder :
    CGaFloat64BladeDecoderBase
{
    internal CGaFloat64IpnsFlatBladeDecoder(CGaFloat64Blade blade) 
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

    public CGaFloat64Flat HyperPlane(CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eoScalar = Blade[0] + Blade[1];

        // This is a point\hypersphere, not a hyperplane
        if (!eoScalar.IsNearZero())
            return new CGaFloat64Flat(
                cgaGeometricSpace,
                0,
                cgaGeometricSpace.ZeroVectorBlade,
                cgaGeometricSpace.ZeroVectorBlade
            );

        var normal = Blade.GetVGaVectorPart();
        var weight = normal.Norm();

        // Hyperplane has zero normal
        if (weight.IsNearZero())
            throw new InvalidOperationException();

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Op(Blade)
                .Gp(Blade.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(Blade.GeometricSpace);
        //.HyperSphereVGaCenter(cgaGeometricSpace);

        var flat = new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            position,
            normal.VGaUnNormal()
        );

        Debug.Assert(
            flat.PositionToIpnsPoint().Lcp(Blade).IsNearZero()
        );

        return flat;
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
    public CGaFloat64Flat Element(RGaFloat64Vector egaProbePoint)
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
                .Op(Blade)
                .Gp(Blade.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(Blade.GeometricSpace);
        //.HyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei.Lcp(Blade.CGaUnDual().Negative());

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
            weight.IsZero() ||
            flat.PositionToIpnsPoint().Lcp(Blade).IsNearZero()
        );

        return flat;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
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
    //    if (!blade.Specs.IsIpnsFlat)
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
    //    if (!blade.Specs.IsIpnsFlat)
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
    //    if (!blade.Specs.IsIpnsFlat)
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
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eoScalar = Blade[0] + Blade[1];

        return eoScalar.IsNearZero()
            ? Blade.GetVGaVectorPart().VGaUnNormal()
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaDirection()
    {
        return Blade.GeometricSpace.Ei
            .Lcp(Blade.CGaUnDual())
            .GetVGaPart(true)
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlaneVGaNormalDirection()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eoScalar = Blade[0] + Blade[1];

        return eoScalar.IsNearZero()
            ? Blade.GetVGaVectorPart()
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaNormalDirection()
    {
        return VGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineVGaPosition(LinFloat64Vector2D egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PlaneVGaPosition(LinFloat64Vector3D egaProbePoint)
    {
        return HyperPlaneVGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlaneVGaPosition(RGaFloat64Vector egaProbePoint)
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
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eoScalar = Blade[0] + Blade[1];

        return eoScalar.IsNearZero()
            ? egaProbePoint
                .VGaVectorToIpnsPoint()
                .Op(Blade)
                .Gp(Blade.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(Blade.GeometricSpace)
            //.HyperSphereVGaCenter(cgaGeometricSpace)
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition()
    {
        return VGaPosition(
            Blade.GeometricSpace.ZeroVectorBlade
        );
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
    public CGaFloat64Blade VGaPosition(LinFloat64Vector egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(RGaFloat64Vector egaProbePoint)
    {
        return VGaPosition(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(CGaFloat64Blade egaProbePointBlade)
    {
        return egaProbePointBlade
            .VGaVectorToIpnsPoint()
            .Op(Blade)
            .Gp(Blade.Inverse())
            .VectorPartToConformalVGaBlade(Blade.GeometricSpace);
        //.HyperSphereVGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double HyperPlaneWeight()
    {
        var eoScalar = Blade[0] + Blade[1];

        return eoScalar.IsNearZero()
            ? Blade.GetVGaVectorPart().Norm()
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector2D egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector3D egaProbePoint)
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
    public double Weight(RGaFloat64Vector egaProbePoint)
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
            Blade.GeometricSpace.Ei.Lcp(Blade.CGaUnDual().Negative());

        return ipnsProbePoint
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


}