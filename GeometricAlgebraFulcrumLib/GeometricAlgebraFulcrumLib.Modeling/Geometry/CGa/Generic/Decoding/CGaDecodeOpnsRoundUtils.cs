using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeOpnsCircle2D<T>(this CGaBlade<T> opnsRound)
    {
        Debug.Assert(opnsRound.GeometricSpace.Is4D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeOpnsSphere3D<T>(this CGaBlade<T> opnsRound)
    {
        Debug.Assert(opnsRound.GeometricSpace.Is5D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeOpnsHyperSphere<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeOpnsRound<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeOpnsRound<T>(this CGaBlade<T> opnsRound, LinVector<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    public static CGaRound<T> DecodeOpnsRound<T>(this CGaBlade<T> opnsRound, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = opnsRound.GeometricSpace;

        var eiX = cgaGeometricSpace.Ei.Lcp(opnsRound);
        var eiX2 = eiX.SpSquared();

        var position =
            opnsRound
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var radiusSquared =
            opnsRound.Sp(opnsRound.GradeInvolution()) / eiX2;

        //var position =
        //    OpnsSphereToVGaCenter(
        //        -0.5 / eiX2 * opnsRound.Gp(Ei).Gp(opnsRound).GetVectorPart()
        //    );

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(opnsRound)
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
            directionOpEi.RemoveEi()
        );

        return round;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsRound<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsRound<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsRound<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsRound<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static XGaConformalBlade<T> DecodeOpnsHyperSphereVGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
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
    //internal static XGaConformalBlade<T> DecodeOpnsHyperSphereVGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeOpnsCircleVGaCenter2D<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsCircleVGaCenter3D<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsHyperSphereVGaCenter<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereVGaCenter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsRoundVGaCenter<T>(this CGaBlade<T> opnsRound)
    {
        var cgaGeometricSpace = opnsRound.GeometricSpace;

        var eiX =
            cgaGeometricSpace.Ei.Lcp(opnsRound);

        return opnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    //internal static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeOpnsHyperSphereWeightVGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
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
    //internal static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeOpnsHyperSphereWeightVGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereWeightVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector2D<T>> DecodeOpnsCircleWeightVGaCenter2D<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleWeightVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector3D<T>> DecodeOpnsSphereWeightVGaCenter3D<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsSphereWeightVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, CGaBlade<T>> DecodeOpnsHyperSphereWeightVGaCenter<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeightVGaCenter();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsPointPairVGaPoint1<T>(this CGaBlade<T> opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.GeometricSpace.ZeroVectorBlade;

        return new CGaBlade<T>(
            opnsRound.GeometricSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsPointPairVGaPoint2<T>(this CGaBlade<T> opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.GeometricSpace.ZeroVectorBlade;

        return new CGaBlade<T>(
            opnsRound.GeometricSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    public static Pair<CGaBlade<T>> DecodeOpnsPointPairVGaPoints<T>(this CGaBlade<T> opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaBlade<T>>(
                opnsRound.GeometricSpace.ZeroVectorBlade,
                opnsRound.GeometricSpace.ZeroVectorBlade
            );

        var point1 = new CGaBlade<T>(
            opnsRound.GeometricSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        var point2 = new CGaBlade<T>(
            opnsRound.GeometricSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        return new Pair<CGaBlade<T>>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsRoundVGaDirection<T>(this CGaBlade<T> opnsRound)
    {
        var cgaGeometricSpace = opnsRound.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(opnsRound)
            .Op(cgaGeometricSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsRoundVGaNormalDirection<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRoundVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundRadius<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundRadiusSquared<T>(this CGaBlade<T> opnsRound)
    {
        var cgaGeometricSpace = opnsRound.GeometricSpace;

        var eiX2 =
            cgaGeometricSpace.Ei.Lcp(opnsRound).SpSquared();

        return opnsRound.Sp(opnsRound.GradeInvolution()) / eiX2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsHyperSphereWeight<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight<T>(this CGaBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight2D<T>(this CGaBlade<T> opnsRound, LinVector2D<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight3D<T>(this CGaBlade<T> opnsRound, LinVector3D<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight<T>(this CGaBlade<T> opnsRound, LinVector<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight<T>(this CGaBlade<T> opnsRound, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = opnsRound.GeometricSpace;

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(opnsRound)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }

}