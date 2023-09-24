using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeOpnsCircle2D(this RGaConformalBlade opnsRound)
    {
        Debug.Assert(opnsRound.ConformalSpace.Is4D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeOpnsSphere3D(this RGaConformalBlade opnsRound)
    {
        Debug.Assert(opnsRound.ConformalSpace.Is5D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeOpnsHyperSphere(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeOpnsRound(this RGaConformalBlade opnsRound)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeOpnsRound(this RGaConformalBlade opnsRound, Float64Vector egaProbePoint)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    public static RGaConformalRound DecodeOpnsRound(this RGaConformalBlade opnsRound, RGaConformalBlade egaProbePoint)
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

        var round = new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            position,
            directionOpEi.RemoveEi()
        );

        return round;
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsRound(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsRound(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsRound(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsRound(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static RGaConformalBlade DecodeOpnsHyperSphereEGaCenter(this RGaFloat64Vector vector, RGaConformalSpace conformalSpace)
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
    //internal static RGaConformalBlade DecodeOpnsHyperSphereEGaCenter(this RGaFloat64Multivector vector, RGaConformalSpace conformalSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereEGaCenter(
    //        conformalSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D DecodeOpnsCircleEGaCenter2D(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleEGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D DecodeOpnsCircleEGaCenter3D(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleEGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsHyperSphereEGaCenter(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereEGaCenter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsRoundEGaCenter(this RGaConformalBlade opnsRound)
    {
        var conformalSpace = opnsRound.ConformalSpace;

        var eiX =
            conformalSpace.Ei.Lcp(opnsRound);

        return opnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    //internal static Tuple<double, RGaConformalBlade> DecodeOpnsHyperSphereWeightEGaCenter(this RGaFloat64Vector vector, RGaConformalSpace conformalSpace)
    //{
    //    var weight = vector[0] + vector[1];

    //    if (weight.IsNearZero())
    //        return new Tuple<double, RGaConformalBlade>(
    //            0, 
    //            conformalSpace.EGaZeroVectorBlade
    //        );

    //    return weight.IsNearZero()
    //        ? throw new InvalidOperationException()
    //        : new Tuple<double, RGaConformalBlade>(
    //            weight,
    //            vector
    //                .GetVectorPart(i => i >= 2)
    //                .Divide(weight)
    //                .ToConformalBlade(conformalSpace)
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static Tuple<double, RGaConformalBlade> DecodeOpnsHyperSphereWeightEGaCenter(this RGaFloat64Multivector vector, RGaConformalSpace conformalSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereWeightEGaCenter(
    //        conformalSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector2D> DecodeOpnsCircleWeightEGaCenter2D(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleWeightEGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector3D> DecodeOpnsSphereWeightEGaCenter3D(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsSphereWeightEGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, RGaConformalBlade> DecodeOpnsHyperSphereWeightEGaCenter(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeightEGaCenter();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsPointPairEGaPoint1(this RGaConformalBlade opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.ConformalSpace.ZeroVectorBlade;

        return new RGaConformalBlade(
            opnsRound.ConformalSpace,
            pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsPointPairEGaPoint2(this RGaConformalBlade opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.ConformalSpace.ZeroVectorBlade;

        return new RGaConformalBlade(
            opnsRound.ConformalSpace,
            pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );
    }

    public static Pair<RGaConformalBlade> DecodeOpnsPointPairEGaPoints(this RGaConformalBlade opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<RGaConformalBlade>(
                opnsRound.ConformalSpace.ZeroVectorBlade,
                opnsRound.ConformalSpace.ZeroVectorBlade
            );

        var point1 = new RGaConformalBlade(
            opnsRound.ConformalSpace,
            pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );

        var point2 = new RGaConformalBlade(
            opnsRound.ConformalSpace,
            pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );

        return new Pair<RGaConformalBlade>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsRoundEGaDirection(this RGaConformalBlade opnsRound)
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
    public static RGaConformalBlade DecodeOpnsRoundEGaNormalDirection(this RGaConformalBlade opnsRound)
    {
        return opnsRound.DecodeOpnsRoundEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundRadius(this RGaConformalBlade opnsRound)
    {
        return opnsRound.DecodeOpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundRadiusSquared(this RGaConformalBlade opnsRound)
    {
        var conformalSpace = opnsRound.ConformalSpace;

        var eiX2 =
            conformalSpace.Ei.Lcp(opnsRound).SpSquared();

        return opnsRound.Sp(opnsRound.GradeInvolution()) / eiX2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsHyperSphereWeight(this RGaConformalBlade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight(this RGaConformalBlade opnsRound)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight2D(this RGaConformalBlade opnsRound, Float64Vector2D egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight3D(this RGaConformalBlade opnsRound, Float64Vector3D egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight(this RGaConformalBlade opnsRound, Float64Vector egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight(this RGaConformalBlade opnsRound, RGaConformalBlade egaProbePoint)
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