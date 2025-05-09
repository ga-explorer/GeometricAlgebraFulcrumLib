using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public sealed class CGaOpnsRoundBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    internal CGaOpnsRoundBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> Circle2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        return HyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> Sphere3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        return HyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> HyperSphere()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> Element(LinVector<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    public CGaRound<T> Element(CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX = cgaGeometricSpace.Ei.Lcp(Blade);
        var eiX2 = eiX.SpSquared();

        var position =
            Blade
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var radiusSquared =
            Blade.Sp(Blade.GradeInvolution()).Divide(eiX2);

        //var position =
        //    OpnsSphereToVGaCenter(
        //        -0.5 / eiX2 * Blade.Gp(Ei).Gp(Blade).GetVectorPart()
        //    );

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(Blade)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var round = new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            position,
            directionOpEi.GetVGaPart(true)
        );

        return round;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsRound)
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
    //    if (!blade.Specs.IsOpnsRound)
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
    //    if (!blade.Specs.IsOpnsRound)
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
    //    if (!blade.Specs.IsOpnsRound)
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
    //internal static XGaConformalBlade<T> HyperSphereVGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
    //{
    //    var weight = vector[0] + vector[1];

    //    if (weight.IsNearZero())
    //        return cgaGeometricSpace.VGaZeroVectorBlade;

    //    return vector
    //        .GetVectorPart(i => i >= 2)
    //        .Divide(weight)
    //        .ToConformalBlade(cgaGeometricSpace);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static XGaConformalBlade<T> HyperSphereVGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().HyperSphereVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> CircleVGaCenter2D()
    {
        return Blade.CGaDual().Decode.IpnsRound.CircleVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> CircleVGaCenter3D()
    {
        return Blade.CGaDual().Decode.IpnsRound.CircleVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperSphereVGaCenter()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphereVGaCenter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaCenter()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX =
            cgaGeometricSpace.Ei.Lcp(Blade);

        return Blade
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    //internal static Tuple<Scalar<T>, XGaConformalBlade<T>> HyperSphereWeightVGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
    //{
    //    var weight = vector[0] + vector[1];

    //    if (weight.IsNearZero())
    //        return new Tuple<Scalar<T>, XGaConformalBlade<T>>(
    //            0, 
    //            cgaGeometricSpace.VGaZeroVectorBlade
    //        );

    //    return weight.IsNearZero()
    //        ? throw new InvalidOperationException()
    //        : new Tuple<Scalar<T>, XGaConformalBlade<T>>(
    //            weight,
    //            vector
    //                .GetVectorPart(i => i >= 2)
    //                .Divide(weight)
    //                .ToConformalBlade(cgaGeometricSpace)
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static Tuple<Scalar<T>, XGaConformalBlade<T>> HyperSphereWeightVGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().HyperSphereWeightVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<Scalar<T>, LinVector2D<T>> CircleWeightVGaCenter2D()
    {
        return Blade.CGaDual().Decode.IpnsRound.CircleWeightVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<Scalar<T>, LinVector3D<T>> SphereWeightVGaCenter3D()
    {
        return Blade.CGaDual().Decode.IpnsRound.SphereWeightVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<Scalar<T>, CGaBlade<T>> HyperSphereWeightVGaCenter()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphereWeightVGaCenter();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPairVGaPoint1()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return new CGaBlade<T>(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPairVGaPoint2()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return new CGaBlade<T>(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    public Pair<CGaBlade<T>> PointPairVGaPoints()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaBlade<T>>(
                Blade.GeometricSpace.ZeroVectorBlade,
                Blade.GeometricSpace.ZeroVectorBlade
            );

        var point1 = new CGaBlade<T>(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        var point2 = new CGaBlade<T>(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        return new Pair<CGaBlade<T>>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaDirection()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(Blade)
            .Op(cgaGeometricSpace.Ei)
            .Negative()
            .GetVGaPart(true)
            .DivideByNorm();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaNormalDirection()
    {
        return VGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Radius()
    {
        return RadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> RadiusSquared()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX2 =
            cgaGeometricSpace.Ei.Lcp(Blade).SpSquared();

        return Blade.Sp(Blade.GradeInvolution()).Divide(eiX2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> HyperSphereWeight()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphereWeight();
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
        var cgaGeometricSpace = Blade.GeometricSpace;

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(Blade)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }

}