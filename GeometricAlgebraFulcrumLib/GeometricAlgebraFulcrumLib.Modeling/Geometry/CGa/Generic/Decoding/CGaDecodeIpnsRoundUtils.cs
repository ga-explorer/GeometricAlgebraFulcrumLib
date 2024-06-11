using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeIpnsSphere2D<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is4D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeIpnsSphere3D<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is5D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    public static CGaRound<T> DecodeIpnsHyperSphere<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        var cgaGeometricSpace = ipnsRound.GeometricSpace;
        var scalarProcessor = ipnsRound.ScalarProcessor;

        var weight =
            ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new CGaRound<T>(
                cgaGeometricSpace,
                scalarProcessor.Zero,
                scalarProcessor.Zero,
                cgaGeometricSpace.ZeroVectorBlade,
                cgaGeometricSpace.Ie
            );

        var position =
            ipnsRound
                .InternalVector
                .GetVectorPart(i => i >= 2)
                .Divide(weight)
                .ToConformalBlade(cgaGeometricSpace);

        var eiScalar =
            0.5 * (ipnsRound[0] - ipnsRound[1]);

        var radiusSquared =
            position.NormSquared() - 2 * eiScalar / weight;

        var round = new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            position,
            cgaGeometricSpace.Ie
        );

        //Debug.Assert(position.Lcp(ipnsRound).IsNearZero());

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DecodeIpnsRound<T>(this CGaBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRound(
            ipnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaRound<T> DecodeIpnsRound<T>(this CGaBlade<T> ipnsRound, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;
        var scalarProcessor = cgaGeometricSpace.ScalarProcessor;

        var eiX = cgaGeometricSpace.Ei.Lcp(ipnsRound);
        var eiX2 = eiX.SpSquared();

        if (eiX2.IsNearZero())
            return new CGaRound<T>(
                cgaGeometricSpace,
                scalarProcessor.Zero,
                scalarProcessor.Zero,
                cgaGeometricSpace.ZeroVectorBlade,
                cgaGeometricSpace.OneScalarBlade
            );

        var position =
            ipnsRound
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var radiusSquared =
            -(ipnsRound.Sp(ipnsRound.GradeInvolution()) / eiX2);

        //var position =
        //    IpnsSphereToVGaCenter(
        //        -0.5 / eiX2 * ipnsRound.Gp(Ei).Gp(ipnsRound).GetVectorPart()
        //    );

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(ipnsRound.CGaUnDual())
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaBlade<T> DecodeIpnsHyperSphereVGaCenter<T>(this XGaVector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return cgaGeometricSpace.ZeroVectorBlade;

        return vector
            .GetVectorPart(i => i >= 2)
            .Divide(weight)
            .ToConformalBlade(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaBlade<T> DecodeIpnsHyperSphereVGaCenter<T>(this XGaMultivector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereVGaCenter(
            cgaGeometricSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeIpnsCircleVGaCenter2D<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is4D);

        var eoScalar = ipnsRound[0] + ipnsRound[1];

        if (eoScalar.IsNearZero())
            return LinVector2D<T>.Zero(ipnsRound.ScalarProcessor);

        return LinVector2D<T>.Create(
            ipnsRound[2] / eoScalar,
            ipnsRound[3] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeIpnsCircleVGaCenter3D<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is5D);

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
    public static CGaBlade<T> DecodeIpnsHyperSphereVGaCenter<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereVGaCenter(
            ipnsRound.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsRoundVGaCenter<T>(this CGaBlade<T> ipnsRound)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        var eiX =
            cgaGeometricSpace.Ei.Lcp(ipnsRound);

        return ipnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    internal static Tuple<Scalar<T>, CGaBlade<T>> DecodeIpnsHyperSphereWeightVGaCenter<T>(this XGaVector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return new Tuple<Scalar<T>, CGaBlade<T>>(
                cgaGeometricSpace.ScalarProcessor.ScalarFromValue(cgaGeometricSpace.ScalarProcessor.OneValue),
                cgaGeometricSpace.ZeroVectorBlade
            );

        return weight.IsNearZero()
            ? throw new InvalidOperationException()
            : new Tuple<Scalar<T>, CGaBlade<T>>(
                weight,
                vector
                    .GetVectorPart(i => i >= 2)
                    .Divide(weight)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Tuple<Scalar<T>, CGaBlade<T>> DecodeIpnsHyperSphereWeightVGaCenter<T>(this XGaMultivector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereWeightVGaCenter(
            cgaGeometricSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector2D<T>> DecodeIpnsCircleWeightVGaCenter2D<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is4D);

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
    public static Tuple<Scalar<T>, LinVector3D<T>> DecodeIpnsSphereWeightVGaCenter3D<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is5D);

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
    public static Tuple<Scalar<T>, CGaBlade<T>> DecodeIpnsHyperSphereWeightVGaCenter<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereWeightVGaCenter(
            ipnsRound.GeometricSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsPointPairVGaPoint1<T>(this CGaBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector())
            .EncodeVGaVectorBlade(ipnsRound.GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsPointPairVGaPoint2<T>(this CGaBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector())
            .EncodeVGaVectorBlade(ipnsRound.GeometricSpace);
    }

    public static Pair<CGaBlade<T>> DecodeIpnsPointPairVGaPoints<T>(this CGaBlade<T> ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaBlade<T>>(
                ipnsRound.GeometricSpace.ZeroVectorBlade,
                ipnsRound.GeometricSpace.ZeroVectorBlade
            );

        var center = pointPair.CenterToXGaVector();
        var direction = pointPair.DirectionToXGaVector();

        var point1 =
            (center - pointPair.RealRadius * direction).EncodeVGaVectorBlade(ipnsRound.GeometricSpace);

        var point2 =
            (center + pointPair.RealRadius * direction).EncodeVGaVectorBlade(ipnsRound.GeometricSpace);

        return new Pair<CGaBlade<T>>(point1, point2);
    }

    public static Pair<LinVector2D<T>> DecodeIpnsPointPairVGaPointsAsVector2D<T>(this CGaBlade<T> ipnsRound)
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

    public static Pair<LinVector3D<T>> DecodeIpnsPointPairVGaPointsAsVector3D<T>(this CGaBlade<T> ipnsRound)
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
    public static CGaBlade<T> DecodeIpnsRoundVGaDirection<T>(this CGaBlade<T> ipnsRound)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(ipnsRound.CGaUnDual())
            .Op(cgaGeometricSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsRoundVGaNormalDirection<T>(this CGaBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundRadius<T>(this CGaBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundRadiusSquared<T>(this CGaBlade<T> ipnsRound)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        var eiX2 =
            cgaGeometricSpace.Ei.Lcp(ipnsRound).SpSquared();

        return eiX2.IsNearZero()
            ? ipnsRound.ScalarProcessor.Zero
            : -(ipnsRound.Sp(ipnsRound.GradeInvolution()) / eiX2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsHyperSphereWeight<T>(this CGaBlade<T> ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound[0] + ipnsRound[1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight<T>(this CGaBlade<T> ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight2D<T>(this CGaBlade<T> ipnsRound, LinVector2D<T> egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight3D<T>(this CGaBlade<T> ipnsRound, LinVector3D<T> egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight<T>(this CGaBlade<T> ipnsRound, LinVector<T> egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsRoundWeight<T>(this CGaBlade<T> ipnsRound, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(ipnsRound.CGaUnDual())
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }

}