using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DecodeIpnsSphere2D(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is4D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DecodeIpnsSphere3D(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is5D);

        return ipnsRound.DecodeIpnsHyperSphere();
    }

    public static CGaFloat64Round DecodeIpnsHyperSphere(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        var weight =
            ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new CGaFloat64Round(
                cgaGeometricSpace,
                0,
                0,
                cgaGeometricSpace.ZeroVectorBlade,
                cgaGeometricSpace.Ie
            );

        var position =
            ipnsRound
                .InternalVector
                .GetVectorPart((int i) => i >= 2)
                .Divide(weight)
                .ToConformalBlade(cgaGeometricSpace);

        var eiScalar =
            0.5 * (ipnsRound[0] - ipnsRound[1]);

        var radiusSquared =
            position.NormSquared() - 2 * eiScalar / weight;

        var round = new CGaFloat64Round(
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
    public static CGaFloat64Round DecodeIpnsRound(this CGaFloat64Blade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRound(
            ipnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaFloat64Round DecodeIpnsRound(this CGaFloat64Blade ipnsRound, CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        var eiX = cgaGeometricSpace.Ei.Lcp(ipnsRound);
        var eiX2 = eiX.SpSquared();

        if (eiX2.IsNearZero())
            return new CGaFloat64Round(
                cgaGeometricSpace,
                0,
                0,
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

        var round = new CGaFloat64Round(
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaFloat64Blade DecodeIpnsHyperSphereVGaCenter(this RGaFloat64Vector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return cgaGeometricSpace.ZeroVectorBlade;

        return vector
            .GetVectorPart((int i) => i >= 2)
            .Divide(weight)
            .ToConformalBlade(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaFloat64Blade DecodeIpnsHyperSphereVGaCenter(this RGaFloat64Multivector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereVGaCenter(
            cgaGeometricSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D DecodeIpnsCircleVGaCenter2D(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is4D);

        var eoScalar = ipnsRound[0] + ipnsRound[1];

        if (eoScalar.IsNearZero())
            return LinFloat64Vector2D.Zero;

        return LinFloat64Vector2D.Create(
            ipnsRound[2] / eoScalar,
            ipnsRound[3] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D DecodeIpnsCircleVGaCenter3D(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is5D);

        var eoScalar = ipnsRound[0] + ipnsRound[1];

        if (eoScalar.IsNearZero())
            return LinFloat64Vector3D.Zero;

        return LinFloat64Vector3D.Create(
            ipnsRound[2] / eoScalar,
            ipnsRound[3] / eoScalar,
            ipnsRound[4] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperSphereVGaCenter(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereVGaCenter(
            ipnsRound.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsRoundVGaCenter(this CGaFloat64Blade ipnsRound)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        var eiX =
            cgaGeometricSpace.Ei.Lcp(ipnsRound);

        return ipnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    internal static Tuple<double, CGaFloat64Blade> DecodeIpnsHyperSphereWeightVGaCenter(this RGaFloat64Vector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return new Tuple<double, CGaFloat64Blade>(
                0,
                cgaGeometricSpace.ZeroVectorBlade
            );

        return weight.IsNearZero()
            ? throw new InvalidOperationException()
            : new Tuple<double, CGaFloat64Blade>(
                weight,
                vector
                    .GetVectorPart((int i) => i >= 2)
                    .Divide(weight)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Tuple<double, CGaFloat64Blade> DecodeIpnsHyperSphereWeightVGaCenter(this RGaFloat64Multivector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereWeightVGaCenter(
            cgaGeometricSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, LinFloat64Vector2D> DecodeIpnsCircleWeightVGaCenter2D(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is4D);

        var weight = ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new Tuple<double, LinFloat64Vector2D>(
                0,
                LinFloat64Vector2D.Zero
            );

        var egaPoint = LinFloat64Vector2D.Create(
            ipnsRound[2] / weight,
            ipnsRound[3] / weight
        );

        return new Tuple<double, LinFloat64Vector2D>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, LinFloat64Vector3D> DecodeIpnsSphereWeightVGaCenter3D(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.GeometricSpace.Is5D);

        var weight = ipnsRound[0] + ipnsRound[1];

        if (weight.IsNearZero())
            return new Tuple<double, LinFloat64Vector3D>(
                0,
                LinFloat64Vector3D.Zero
            );

        var egaPoint = LinFloat64Vector3D.Create(
            ipnsRound[2] / weight,
            ipnsRound[3] / weight,
            ipnsRound[4] / weight
        );

        return new Tuple<double, LinFloat64Vector3D>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, CGaFloat64Blade> DecodeIpnsHyperSphereWeightVGaCenter(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound.InternalVector.DecodeIpnsHyperSphereWeightVGaCenter(
            ipnsRound.GeometricSpace
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsPointPairVGaPoint1(this CGaFloat64Blade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector())
            .EncodeVGaVectorBlade(ipnsRound.GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsPointPairVGaPoint2(this CGaFloat64Blade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return ipnsRound.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector())
            .EncodeVGaVectorBlade(ipnsRound.GeometricSpace);
    }

    public static Pair<CGaFloat64Blade> DecodeIpnsPointPairVGaPoints(this CGaFloat64Blade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaFloat64Blade>(
                ipnsRound.GeometricSpace.ZeroVectorBlade,
                ipnsRound.GeometricSpace.ZeroVectorBlade
            );

        var center = pointPair.CenterToRGaVector();
        var direction = pointPair.DirectionToRGaVector();

        var point1 =
            (center - pointPair.RealRadius * direction).EncodeVGaVectorBlade(ipnsRound.GeometricSpace);

        var point2 =
            (center + pointPair.RealRadius * direction).EncodeVGaVectorBlade(ipnsRound.GeometricSpace);

        return new Pair<CGaFloat64Blade>(point1, point2);
    }

    public static Pair<LinFloat64Vector2D> DecodeIpnsPointPairVGaPointsAsVector2D(this CGaFloat64Blade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<LinFloat64Vector2D>(
                LinFloat64Vector2D.Zero,
                LinFloat64Vector2D.Zero
            );

        var center = pointPair.CenterToVector2D();
        var direction = pointPair.DirectionToVector2D();

        var point1 =
            center - pointPair.RealRadius * direction;

        var point2 =
            center + pointPair.RealRadius * direction;

        return new Pair<LinFloat64Vector2D>(point1, point2);
    }

    public static Pair<LinFloat64Vector3D> DecodeIpnsPointPairVGaPointsAsVector3D(this CGaFloat64Blade ipnsRound)
    {
        var pointPair =
            ipnsRound.DecodeIpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<LinFloat64Vector3D>(
                LinFloat64Vector3D.Zero,
                LinFloat64Vector3D.Zero
            );

        var center = pointPair.CenterToVector3D();
        var direction = pointPair.DirectionToVector3D();

        var point1 =
            center - pointPair.RealRadius * direction;

        var point2 =
            center + pointPair.RealRadius * direction;

        return new Pair<LinFloat64Vector3D>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsRoundVGaDirection(this CGaFloat64Blade ipnsRound)
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
    public static CGaFloat64Blade DecodeIpnsRoundVGaNormalDirection(this CGaFloat64Blade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundRadius(this CGaFloat64Blade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundRadiusSquared(this CGaFloat64Blade ipnsRound)
    {
        var cgaGeometricSpace = ipnsRound.GeometricSpace;

        var eiX2 =
            cgaGeometricSpace.Ei.Lcp(ipnsRound).SpSquared();

        return eiX2.IsNearZero()
            ? 0d
            : -(ipnsRound.Sp(ipnsRound.GradeInvolution()) / eiX2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsHyperSphereWeight(this CGaFloat64Blade ipnsRound)
    {
        Debug.Assert(ipnsRound.IsVector);

        return ipnsRound[0] + ipnsRound[1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight(this CGaFloat64Blade ipnsRound)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight2D(this CGaFloat64Blade ipnsRound, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight3D(this CGaFloat64Blade ipnsRound, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight(this CGaFloat64Blade ipnsRound, LinFloat64Vector egaProbePoint)
    {
        return ipnsRound.DecodeIpnsRoundWeight(
            ipnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsRoundWeight(this CGaFloat64Blade ipnsRound, CGaFloat64Blade egaProbePoint)
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