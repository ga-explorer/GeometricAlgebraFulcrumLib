using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DecodeOpnsCircle2D(this CGaFloat64Blade opnsRound)
    {
        Debug.Assert(opnsRound.GeometricSpace.Is4D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DecodeOpnsSphere3D(this CGaFloat64Blade opnsRound)
    {
        Debug.Assert(opnsRound.GeometricSpace.Is5D);

        return opnsRound.DecodeOpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DecodeOpnsHyperSphere(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DecodeOpnsRound(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DecodeOpnsRound(this CGaFloat64Blade opnsRound, LinFloat64Vector egaProbePoint)
    {
        return opnsRound.DecodeOpnsRound(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    public static CGaFloat64Round DecodeOpnsRound(this CGaFloat64Blade opnsRound, CGaFloat64Blade egaProbePoint)
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

        var round = new CGaFloat64Round(
            cgaGeometricSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static CGaFloat64Blade DecodeOpnsHyperSphereVGaCenter(this RGaFloat64Vector vector, RGaConformalSpace cgaGeometricSpace)
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
    //internal static CGaFloat64Blade DecodeOpnsHyperSphereVGaCenter(this RGaFloat64Multivector vector, RGaConformalSpace cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D DecodeOpnsCircleVGaCenter2D(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D DecodeOpnsCircleVGaCenter3D(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsHyperSphereVGaCenter(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereVGaCenter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsRoundVGaCenter(this CGaFloat64Blade opnsRound)
    {
        var cgaGeometricSpace = opnsRound.GeometricSpace;

        var eiX =
            cgaGeometricSpace.Ei.Lcp(opnsRound);

        return opnsRound
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    //internal static Tuple<double, CGaFloat64Blade> DecodeOpnsHyperSphereWeightVGaCenter(this RGaFloat64Vector vector, RGaConformalSpace cgaGeometricSpace)
    //{
    //    var weight = vector[0] + vector[1];

    //    if (weight.IsNearZero())
    //        return new Tuple<double, CGaFloat64Blade>(
    //            0, 
    //            cgaGeometricSpace.VGaZeroVectorBlade
    //        );

    //    return weight.IsNearZero()
    //        ? throw new InvalidOperationException()
    //        : new Tuple<double, CGaFloat64Blade>(
    //            weight,
    //            vector
    //                .GetVectorPart(i => i >= 2)
    //                .Divide(weight)
    //                .ToConformalBlade(cgaGeometricSpace)
    //        );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static Tuple<double, CGaFloat64Blade> DecodeOpnsHyperSphereWeightVGaCenter(this RGaFloat64Multivector vector, RGaConformalSpace cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().DecodeOpnsHyperSphereWeightVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, LinFloat64Vector2D> DecodeOpnsCircleWeightVGaCenter2D(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsCircleWeightVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, LinFloat64Vector3D> DecodeOpnsSphereWeightVGaCenter3D(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsSphereWeightVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, CGaFloat64Blade> DecodeOpnsHyperSphereWeightVGaCenter(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeightVGaCenter();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsPointPairVGaPoint1(this CGaFloat64Blade opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.GeometricSpace.ZeroVectorBlade;

        return new CGaFloat64Blade(
            opnsRound.GeometricSpace,
            pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsPointPairVGaPoint2(this CGaFloat64Blade opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return opnsRound.GeometricSpace.ZeroVectorBlade;

        return new CGaFloat64Blade(
            opnsRound.GeometricSpace,
            pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );
    }

    public static Pair<CGaFloat64Blade> DecodeOpnsPointPairVGaPoints(this CGaFloat64Blade opnsRound)
    {
        var pointPair =
            opnsRound.DecodeOpnsRound();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaFloat64Blade>(
                opnsRound.GeometricSpace.ZeroVectorBlade,
                opnsRound.GeometricSpace.ZeroVectorBlade
            );

        var point1 = new CGaFloat64Blade(
            opnsRound.GeometricSpace,
            pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );

        var point2 = new CGaFloat64Blade(
            opnsRound.GeometricSpace,
            pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );

        return new Pair<CGaFloat64Blade>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsRoundVGaDirection(this CGaFloat64Blade opnsRound)
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
    public static CGaFloat64Blade DecodeOpnsRoundVGaNormalDirection(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.DecodeOpnsRoundVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundRadius(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.DecodeOpnsRoundRadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundRadiusSquared(this CGaFloat64Blade opnsRound)
    {
        var cgaGeometricSpace = opnsRound.GeometricSpace;

        var eiX2 =
            cgaGeometricSpace.Ei.Lcp(opnsRound).SpSquared();

        return opnsRound.Sp(opnsRound.GradeInvolution()) / eiX2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsHyperSphereWeight(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.CGaDual().DecodeIpnsHyperSphereWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight(this CGaFloat64Blade opnsRound)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight2D(this CGaFloat64Blade opnsRound, LinFloat64Vector2D egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight3D(this CGaFloat64Blade opnsRound, LinFloat64Vector3D egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight(this CGaFloat64Blade opnsRound, LinFloat64Vector egaProbePoint)
    {
        return opnsRound.DecodeOpnsRoundWeight(
            opnsRound.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsRoundWeight(this CGaFloat64Blade opnsRound, CGaFloat64Blade egaProbePoint)
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