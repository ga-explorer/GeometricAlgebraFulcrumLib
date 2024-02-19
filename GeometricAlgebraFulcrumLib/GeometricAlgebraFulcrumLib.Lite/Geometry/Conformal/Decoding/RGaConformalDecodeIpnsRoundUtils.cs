using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeIpnsSphere2D(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is4D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeIpnsSphere3D(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is5D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    public static RGaConformalRound DecodeIpnsHyperSphere(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        var conformalSpace = ipnsRound.ConformalSpace;

        var weight =
            ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new RGaConformalRound(
                conformalSpace,
                0,
                0,
                conformalSpace.ZeroVectorBlade,
                conformalSpace.Ie
            );

        var position =
            ipnsRound
                .InternalVector
                .GetVectorPart((int i) => i >= 2)
                .Divide(weight)
                .ToConformalBlade(conformalSpace);

        var eiScalar =
            0.5 * (ipnsRound[0] - ipnsRound[1]);

        var radiusSquared =
            position.NormSquared() - 2 * eiScalar / weight;

        var round = new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            position,
            conformalSpace.Ie
        );

        //Debug.Assert(position.Lcp(ipnsRound).IsNearZero());

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DecodeIpnsRound(this RGaConformalBlade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRound(
            ipnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    public static RGaConformalRound DecodeIpnsRound(this RGaConformalBlade ipnsRound, RGaConformalBlade egaProbePoint)
    {
        var conformalSpace = ipnsRound.ConformalSpace;

        var eiX = conformalSpace.Ei.Lcp(ipnsRound);
        var eiX2 = eiX.SpSquared();

        if (eiX2.IsNearZero())
            return new RGaConformalRound(
                conformalSpace,
                0,
                0,
                conformalSpace.ZeroVectorBlade,
                conformalSpace.OneScalarBlade
            );

        var position =
            ipnsRound
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var radiusSquared =
            -(ipnsRound.Sp(ipnsRound.GradeInvolution()) / eiX2);

        //var position =
        //    IpnsSphereToEGaCenter(
        //        -0.5 / eiX2 * ipnsRound.Gp(Ei).Gp(ipnsRound).GetVectorPart()
        //    );

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(ipnsRound.CGaUnDual())
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

        //Debug.Assert(position.Lcp(ipnsRound).IsNearZero());

        return round;
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsRound(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsRound(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsRound(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsRound(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaConformalBlade DecodeIpnsHyperSphereEGaCenter(this RGaFloat64Vector vector, RGaConformalSpace conformalSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return conformalSpace.ZeroVectorBlade;

        return vector
            .GetVectorPart((int i) => i >= 2)
            .Divide(weight)
            .ToConformalBlade(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaConformalBlade DecodeIpnsHyperSphereEGaCenter(this RGaFloat64Multivector vector, RGaConformalSpace conformalSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereEGaCenter(
            conformalSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D DecodeIpnsCircleEGaCenter2D(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is4D);

        var eoScalar = ipnsRound[0] + ipnsRound[1];

        if (eoScalar.IsNearZero())
            return Float64Vector2D.Zero;

        return Float64Vector2D.Create(
            ipnsRound[2] / eoScalar,
            ipnsRound[3] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D DecodeIpnsCircleEGaCenter3D(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is5D);

        var eoScalar = ipnsRound[0] + ipnsRound[1];

        if (eoScalar.IsNearZero())
            return Float64Vector3D.Zero;

        return Float64Vector3D.Create(
            ipnsRound[2] / eoScalar,
            ipnsRound[3] / eoScalar,
            ipnsRound[4] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsHyperSphereEGaCenter(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereEGaCenter(
            ipnsRound.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsRoundEGaCenter(this RGaConformalBlade ipnsRound)
    {
        var conformalSpace = ipnsRound.ConformalSpace;

        var eiX =
            conformalSpace.Ei.Lcp(ipnsRound);

        return ipnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    internal static Tuple<double, RGaConformalBlade> DecodeIpnsHyperSphereWeightEGaCenter(this RGaFloat64Vector vector, RGaConformalSpace conformalSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return new Tuple<double, RGaConformalBlade>(
                0,
                conformalSpace.ZeroVectorBlade
            );

        return weight.IsNearZero()
            ? throw new InvalidOperationException()
            : new Tuple<double, RGaConformalBlade>(
                weight,
                vector
                    .GetVectorPart((int i) => i >= 2)
                    .Divide(weight)
                    .ToConformalBlade(conformalSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Tuple<double, RGaConformalBlade> DecodeIpnsHyperSphereWeightEGaCenter(this RGaFloat64Multivector vector, RGaConformalSpace conformalSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereWeightEGaCenter(
            conformalSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector2D> DecodeIpnsCircleWeightEGaCenter2D(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is4D);

        var weight = ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new Tuple<double, Float64Vector2D>(
                0,
                Float64Vector2D.Zero
            );

        var egaPoint = Float64Vector2D.Create(
            ipnsRound[2] / weight,
            ipnsRound[3] / weight
        );

        return new Tuple<double, Float64Vector2D>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, Float64Vector3D> DecodeIpnsSphereWeightEGaCenter3D(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is5D);

        var weight = ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new Tuple<double, Float64Vector3D>(
                0,
                Float64Vector3D.Zero
            );

        var egaPoint = Float64Vector3D.Create(
            ipnsRound[2] / weight,
            ipnsRound[3] / weight,
            ipnsRound[4] / weight
        );

        return new Tuple<double, Float64Vector3D>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, RGaConformalBlade> DecodeIpnsHyperSphereWeightEGaCenter(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereWeightEGaCenter(
            ipnsRound.ConformalSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsPointPairEGaPoint1(this RGaConformalBlade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.ConformalSpace.ZeroVectorBlade;

        return (pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector())
            .EncodeEGaVectorBlade(ipnsRound.ConformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsPointPairEGaPoint2(this RGaConformalBlade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.ConformalSpace.ZeroVectorBlade;

        return (pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector())
            .EncodeEGaVectorBlade(ipnsRound.ConformalSpace);
    }

    public static Pair<RGaConformalBlade> DecodeIpnsPointPairEGaPoints(this RGaConformalBlade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<RGaConformalBlade>(
                ipnsRound.ConformalSpace.ZeroVectorBlade,
                ipnsRound.ConformalSpace.ZeroVectorBlade
            );

        var center = pointPair.CenterToRGaVector();
        var direction = pointPair.DirectionToRGaVector();

        var point1 = 
            (center - pointPair.RealRadius * direction).EncodeEGaVectorBlade(ipnsRound.ConformalSpace);

        var point2 = 
            (center + pointPair.RealRadius * direction).EncodeEGaVectorBlade(ipnsRound.ConformalSpace);

        return new Pair<RGaConformalBlade>(point1, point2);
    }
    
    public static Pair<Float64Vector2D> DecodeIpnsPointPairEGaPointsAsVector2D(this RGaConformalBlade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<Float64Vector2D>(
                Float64Vector2D.Zero,
                Float64Vector2D.Zero
            );

        var center = pointPair.CenterToVector2D();
        var direction = pointPair.DirectionToVector2D();

        var point1 = 
            center - pointPair.RealRadius * direction;

        var point2 = 
            center + pointPair.RealRadius * direction;

        return new Pair<Float64Vector2D>(point1, point2);
    }
    
    public static Pair<Float64Vector3D> DecodeIpnsPointPairEGaPointsAsVector3D(this RGaConformalBlade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<Float64Vector3D>(
                Float64Vector3D.Zero,
                Float64Vector3D.Zero
            );

        var center = pointPair.CenterToVector3D();
        var direction = pointPair.DirectionToVector3D();

        var point1 = 
            center - pointPair.RealRadius * direction;

        var point2 = 
            center + pointPair.RealRadius * direction;

        return new Pair<Float64Vector3D>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsRoundEGaDirection(this RGaConformalBlade ipnsRound)
    {
        var conformalSpace = ipnsRound.ConformalSpace;

        return conformalSpace.Ei
            .Lcp(ipnsRound.CGaUnDual())
            .Op(conformalSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsRoundEGaNormalDirection(this RGaConformalBlade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundRadius(this RGaConformalBlade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundRadiusSquared(this RGaConformalBlade ipnsRound)
    {
        var conformalSpace = ipnsRound.ConformalSpace;

        var eiX2 =
            conformalSpace.Ei.Lcp(ipnsRound).SpSquared();

        return eiX2.IsNearZero()
            ? 0d
            : -(ipnsRound.Sp(ipnsRound.GradeInvolution()) / eiX2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsHyperSphereWeight(this RGaConformalBlade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound[0] + ipnsRound[1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight(this RGaConformalBlade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight2D(this RGaConformalBlade ipnsRound, Float64Vector2D egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight3D(this RGaConformalBlade ipnsRound, Float64Vector3D egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight(this RGaConformalBlade ipnsRound, Float64Vector egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight(this RGaConformalBlade ipnsRound, RGaConformalBlade egaProbePoint)
    {
        var conformalSpace = ipnsRound.ConformalSpace;

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(ipnsRound.CGaUnDual())
                .Op(conformalSpace.Ei)
                .Negative();

        return egaProbePoint
            .EGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }

}