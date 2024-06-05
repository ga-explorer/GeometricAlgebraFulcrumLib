using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DecodeIpnsSphere2D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is4D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DecodeIpnsSphere3D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is5D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    public static XGaConformalRound<T> DecodeIpnsHyperSphere<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        var conformalSpace = ipnsRound.ConformalSpace;
        var scalarProcessor = ipnsRound.ScalarProcessor;

        var weight =
            ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new XGaConformalRound<T>(
                conformalSpace,
                scalarProcessor.Zero,
                scalarProcessor.Zero,
                conformalSpace.ZeroVectorBlade,
                conformalSpace.Ie
            );

        var position =
            ipnsRound
                .InternalVector
                .GetVectorPart(i => i >= 2)
                .Divide(weight)
                .ToConformalBlade(conformalSpace);

        var eiScalar =
            0.5 * (ipnsRound[0] - ipnsRound[1]);

        var radiusSquared =
            position.NormSquared() - 2 * eiScalar / weight;

        var round = new XGaConformalRound<T>(
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
    public static XGaConformalRound<T> DecodeIpnsRound<T>(this XGaConformalBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRound(
            ipnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    public static XGaConformalRound<T> DecodeIpnsRound<T>(this XGaConformalBlade<T> ipnsRound, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = ipnsRound.ConformalSpace;
        var scalarProcessor = conformalSpace.ScalarProcessor;

        var eiX = conformalSpace.Ei.Lcp(ipnsRound);
        var eiX2 = eiX.SpSquared();

        if (eiX2.IsNearZero())
            return new XGaConformalRound<T>(
                conformalSpace,
                scalarProcessor.Zero,
                scalarProcessor.Zero,
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

        var round = new XGaConformalRound<T>(
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
    //public static XGaConformalParametricElement<T> DecodeIpnsRound<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsRound<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsRound<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsRound<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsRound(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaConformalBlade<T> DecodeIpnsHyperSphereEGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> conformalSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return conformalSpace.ZeroVectorBlade;

        return vector
            .GetVectorPart(i => i >= 2)
            .Divide(weight)
            .ToConformalBlade(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaConformalBlade<T> DecodeIpnsHyperSphereEGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> conformalSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereEGaCenter(
            conformalSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeIpnsCircleEGaCenter2D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is4D);

        var eoScalar = ipnsRound[0] + ipnsRound[1];

        if (eoScalar.IsNearZero())
            return LinVector2D<T>.Zero(ipnsRound.ScalarProcessor);

        return LinVector2D<T>.Create(
            ipnsRound[2] / eoScalar,
            ipnsRound[3] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeIpnsCircleEGaCenter3D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is5D);

        var eoScalar = ipnsRound[0] + ipnsRound[1];

        if (eoScalar.IsNearZero())
            return LinVector3D<T>.Zero(ipnsRound.ScalarProcessor);

        return LinVector3D<T>.Create(
            ipnsRound[2] / eoScalar,
            ipnsRound[3] / eoScalar,
            ipnsRound[4] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsHyperSphereEGaCenter<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereEGaCenter(
            ipnsRound.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsRoundEGaCenter<T>(this XGaConformalBlade<T> ipnsRound)
    {
        var conformalSpace = ipnsRound.ConformalSpace;

        var eiX =
            conformalSpace.Ei.Lcp(ipnsRound);

        return ipnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    internal static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeIpnsHyperSphereWeightEGaCenter<T>(this XGaVector<T> vector, XGaConformalSpace<T> conformalSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return new Tuple<Scalar<T>, XGaConformalBlade<T>>(
                conformalSpace.ScalarProcessor.ScalarFromValue(conformalSpace.ScalarProcessor.OneValue),
                conformalSpace.ZeroVectorBlade
            );

        return weight.IsNearZero()
            ? throw new InvalidOperationException()
            : new Tuple<Scalar<T>, XGaConformalBlade<T>>(
                weight,
                vector
                    .GetVectorPart(i => i >= 2)
                    .Divide(weight)
                    .ToConformalBlade(conformalSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeIpnsHyperSphereWeightEGaCenter<T>(this XGaMultivector<T> vector, XGaConformalSpace<T> conformalSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereWeightEGaCenter(
            conformalSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector2D<T>> DecodeIpnsCircleWeightEGaCenter2D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is4D);

        var weight = ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new Tuple<Scalar<T>, LinVector2D<T>>(
                ipnsRound.ScalarProcessor.Zero,
                LinVector2D<T>.Zero(ipnsRound.ScalarProcessor)
            );

        var egaPoint = LinVector2D<T>.Create(
            ipnsRound[2] / weight,
            ipnsRound[3] / weight
        );

        return new Tuple<Scalar<T>, LinVector2D<T>>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector3D<T>> DecodeIpnsSphereWeightEGaCenter3D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.ConformalSpace.Is5D);

        var weight = ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new Tuple<Scalar<T>, LinVector3D<T>>(
                ipnsRound.ScalarProcessor.Zero,
                LinVector3D<T>.Zero(ipnsRound.ScalarProcessor)
            );

        var egaPoint = LinVector3D<T>.Create(
            ipnsRound[2] / weight,
            ipnsRound[3] / weight,
            ipnsRound[4] / weight
        );

        return new Tuple<Scalar<T>, LinVector3D<T>>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, XGaConformalBlade<T>> DecodeIpnsHyperSphereWeightEGaCenter<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereWeightEGaCenter(
            ipnsRound.ConformalSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsPointPairEGaPoint1<T>(this XGaConformalBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.ConformalSpace.ZeroVectorBlade;

        return (pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector())
            .EncodeEGaVectorBlade(ipnsRound.ConformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsPointPairEGaPoint2<T>(this XGaConformalBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.ConformalSpace.ZeroVectorBlade;

        return (pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector())
            .EncodeEGaVectorBlade(ipnsRound.ConformalSpace);
    }

    public static Pair<XGaConformalBlade<T>> DecodeIpnsPointPairEGaPoints<T>(this XGaConformalBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<XGaConformalBlade<T>>(
                ipnsRound.ConformalSpace.ZeroVectorBlade,
                ipnsRound.ConformalSpace.ZeroVectorBlade
            );

        var center = pointPair.CenterToXGaVector();
        var direction = pointPair.DirectionToXGaVector();

        var point1 = 
            (center - pointPair.RealRadius * direction).EncodeEGaVectorBlade(ipnsRound.ConformalSpace);

        var point2 = 
            (center + pointPair.RealRadius * direction).EncodeEGaVectorBlade(ipnsRound.ConformalSpace);

        return new Pair<XGaConformalBlade<T>>(point1, point2);
    }
    
    public static Pair<LinVector2D<T>> DecodeIpnsPointPairEGaPointsAsVector2D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<LinVector2D<T>>(
                LinVector2D<T>.Zero(ipnsRound.ScalarProcessor),
                LinVector2D<T>.Zero(ipnsRound.ScalarProcessor)
            );

        var center = pointPair.CenterToVector2D();
        var direction = pointPair.DirectionToVector2D();

        var point1 = 
            center - pointPair.RealRadius * direction;

        var point2 = 
            center + pointPair.RealRadius * direction;

        return new Pair<LinVector2D<T>>(point1, point2);
    }
    
    public static Pair<LinVector3D<T>> DecodeIpnsPointPairEGaPointsAsVector3D<T>(this XGaConformalBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<LinVector3D<T>>(
                LinVector3D<T>.Zero(ipnsRound.ScalarProcessor),
                LinVector3D<T>.Zero(ipnsRound.ScalarProcessor)
            );

        var center = pointPair.CenterToVector3D();
        var direction = pointPair.DirectionToVector3D();

        var point1 = 
            center - pointPair.RealRadius * direction;

        var point2 = 
            center + pointPair.RealRadius * direction;

        return new Pair<LinVector3D<T>>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsRoundEGaDirection<T>(this XGaConformalBlade<T> ipnsRound)
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
    public static XGaConformalBlade<T> DecodeIpnsRoundEGaNormalDirection<T>(this XGaConformalBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundRadius<T>(this XGaConformalBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundRadiusSquared<T>(this XGaConformalBlade<T> ipnsRound)
    {
        var conformalSpace = ipnsRound.ConformalSpace;

        var eiX2 =
            conformalSpace.Ei.Lcp(ipnsRound).SpSquared();

        return eiX2.IsNearZero()
            ? ipnsRound.ScalarProcessor.Zero
            : -(ipnsRound.Sp(ipnsRound.GradeInvolution()) / eiX2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsHyperSphereWeight<T>(this XGaConformalBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound[0] + ipnsRound[1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight<T>(this XGaConformalBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight2D<T>(this XGaConformalBlade<T> ipnsRound, LinVector2D<T> egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight3D<T>(this XGaConformalBlade<T> ipnsRound, LinVector3D<T> egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight<T>(this XGaConformalBlade<T> ipnsRound, LinVector<T> egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight<T>(this XGaConformalBlade<T> ipnsRound, XGaConformalBlade<T> egaProbePoint)
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