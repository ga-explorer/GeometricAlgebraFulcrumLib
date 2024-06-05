using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DecodeOpnsCircle2D<T>(this XGaConformalBlade<T> opnsRound)
    {
        Debug.Assert(opnsRound.ConformalSpace.Is4D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DecodeOpnsSphere3D<T>(this XGaConformalBlade<T> opnsRound)
    {
        Debug.Assert(opnsRound.ConformalSpace.Is5D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DecodeOpnsHyperSphere<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DecodeOpnsRound<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DecodeOpnsRound<T>(this XGaConformalBlade<T> opnsRound, LinVector<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    public static XGaConformalRound<T> DecodeOpnsRound<T>(this XGaConformalBlade<T> opnsRound, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = opnsRound.ConformalSpace;

        var eiX = conformalSpace.Ei.Lcp(opnsRound);
        var eiX2 = eiX.SpSquared();

        var position =
            opnsRound
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var radiusSquared =
            (opnsRound.Sp(opnsRound.GradeInvolution()) / eiX2);

        //var position =
        //    OpnsSphereToEGaCenter(
        //        -0.5 / eiX2 * opnsRound.Gp(Ei).Gp(opnsRound).GetVectorPart()
        //    );

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(opnsRound)
                .Op(conformalSpace.Ei)
                .Negative();

        var weight =
            egaProbePoint
                .EGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var round = new XGaConformalRound<T>(
            conformalSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static XGaConformalBlade<T> DecodeOpnsHyperSphereEGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> conformalSpace)
    //{
    //    var weight = vector[0] + vector[1];

    //    if (weight.IsNearZero())
    //        return conformalSpace.EGaZeroVectorBlade;

    //    return vector
    //        .GetVectorPart(i => i >= 2)
    //        .Divide(weight)
    //        .ToConformalBlade(conformalSpace);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static XGaConformalBlade<T> DecodeOpnsHyperSphereEGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> conformalSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereEGaCenter(
    //        conformalSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeOpnsCircleEGaCenter2D<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleEGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsCircleEGaCenter3D<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleEGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsHyperSphereEGaCenter<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereEGaCenter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsRoundEGaCenter<T>(this XGaConformalBlade<T> opnsRound)
    {
        var conformalSpace = opnsRound.ConformalSpace;

        var eiX =
            conformalSpace.Ei.Lcp(opnsRound);

        return opnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    //internal static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeOpnsHyperSphereWeightEGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> conformalSpace)
    //{
    //    var weight = vector[0] + vector[1];

    //    if (weight.IsNearZero())
    //        return new Tuple<Scalar<T>, XGaConformalBlade<T>>(
    //            0, 
    //            conformalSpace.EGaZeroVectorBlade
    //        );

    //    return weight.IsNearZero()
    //        ? throw new InvalidOperationException()
    //        : new Tuple<Scalar<T>, XGaConformalBlade<T>>(
    //            weight,
    //            vector
    //                .GetVectorPart(i => i >= 2)
    //                .Divide(weight)
    //                .ToConformalBlade(conformalSpace)
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeOpnsHyperSphereWeightEGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> conformalSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereWeightEGaCenter(
    //        conformalSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector2D<T>> DecodeOpnsCircleWeightEGaCenter2D<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleWeightEGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector3D<T>> DecodeOpnsSphereWeightEGaCenter3D<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsSphereWeightEGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeOpnsHyperSphereWeightEGaCenter<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeightEGaCenter();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsPointPairEGaPoint1<T>(this XGaConformalBlade<T> opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.ConformalSpace.ZeroVectorBlade;

        return new XGaConformalBlade<T>(
            opnsRound.ConformalSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsPointPairEGaPoint2<T>(this XGaConformalBlade<T> opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.ConformalSpace.ZeroVectorBlade;

        return new XGaConformalBlade<T>(
            opnsRound.ConformalSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    public static Pair<XGaConformalBlade<T>> DecodeOpnsPointPairEGaPoints<T>(this XGaConformalBlade<T> opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<XGaConformalBlade<T>>(
                opnsRound.ConformalSpace.ZeroVectorBlade,
                opnsRound.ConformalSpace.ZeroVectorBlade
            );

        var point1 = new XGaConformalBlade<T>(
            opnsRound.ConformalSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        var point2 = new XGaConformalBlade<T>(
            opnsRound.ConformalSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        return new Pair<XGaConformalBlade<T>>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsRoundEGaDirection<T>(this XGaConformalBlade<T> opnsRound)
    {
        var conformalSpace = opnsRound.ConformalSpace;

        return conformalSpace.Ei
            .Lcp(opnsRound)
            .Op(conformalSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsRoundEGaNormalDirection<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRoundEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundRadius<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundRadiusSquared<T>(this XGaConformalBlade<T> opnsRound)
    {
        var conformalSpace = opnsRound.ConformalSpace;

        var eiX2 =
            conformalSpace.Ei.Lcp(opnsRound).SpSquared();

        return opnsRound.Sp(opnsRound.GradeInvolution()) / eiX2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsHyperSphereWeight<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight<T>(this XGaConformalBlade<T> opnsRound)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight2D<T>(this XGaConformalBlade<T> opnsRound, LinVector2D<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight3D<T>(this XGaConformalBlade<T> opnsRound, LinVector3D<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight<T>(this XGaConformalBlade<T> opnsRound, LinVector<T> egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsRoundWeight<T>(this XGaConformalBlade<T> opnsRound, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = opnsRound.ConformalSpace;

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(opnsRound)
                .Op(conformalSpace.Ei)
                .Negative();

        return egaProbePoint
            .EGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }

}