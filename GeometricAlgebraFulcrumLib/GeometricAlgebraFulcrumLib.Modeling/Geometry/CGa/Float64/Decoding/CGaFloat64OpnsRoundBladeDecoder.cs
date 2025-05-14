using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64OpnsRoundBladeDecoder :
    CGaFloat64BladeDecoderBase
{
    internal CGaFloat64OpnsRoundBladeDecoder(CGaFloat64Blade blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Circle2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        return HyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Sphere3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        return HyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round HyperSphere()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Element(LinFloat64Vector egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    public CGaFloat64Round Element(CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX = cgaGeometricSpace.Ei.Lcp(Blade);
        var eiX2 = eiX.SpSquared();

        var position =
            Blade
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var radiusSquared =
            Blade.Sp(Blade.GradeInvolution()) / eiX2;

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

        var round = new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            position,
            directionOpEi.GetVGaPart(true)
        );

        return round;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //internal static CGaFloat64Blade HyperSphereVGaCenter(this XGaFloat64Vector vector, XGaConformalSpace cgaGeometricSpace)
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
    //internal static CGaFloat64Blade HyperSphereVGaCenter(this XGaFloat64Multivector vector, XGaConformalSpace cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().HyperSphereVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D CircleVGaCenter2D()
    {
        return Blade.CGaDual().Decode.IpnsRound.CircleVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D CircleVGaCenter3D()
    {
        return Blade.CGaDual().Decode.IpnsRound.CircleVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperSphereVGaCenter()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphereVGaCenter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaCenter()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX =
            cgaGeometricSpace.Ei.Lcp(Blade);

        return Blade
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    //internal static Tuple<double, CGaFloat64Blade> HyperSphereWeightVGaCenter(this XGaFloat64Vector vector, XGaConformalSpace cgaGeometricSpace)
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
    //internal static Tuple<double, CGaFloat64Blade> HyperSphereWeightVGaCenter(this XGaFloat64Multivector vector, XGaConformalSpace cgaGeometricSpace)
    //{
    //    return vector.GetVectorPart().HyperSphereWeightVGaCenter(
    //        cgaGeometricSpace
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<double, LinFloat64Vector2D> CircleWeightVGaCenter2D()
    {
        return Blade.CGaDual().Decode.IpnsRound.CircleWeightVGaCenter2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<double, LinFloat64Vector3D> SphereWeightVGaCenter3D()
    {
        return Blade.CGaDual().Decode.IpnsRound.SphereWeightVGaCenter3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<double, CGaFloat64Blade> HyperSphereWeightVGaCenter()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphereWeightVGaCenter();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPairVGaPoint1()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return new CGaFloat64Blade(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPairVGaPoint2()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return new CGaFloat64Blade(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }

    public Pair<CGaFloat64Blade> PointPairVGaPoints()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaFloat64Blade>(
                Blade.GeometricSpace.ZeroVectorBlade,
                Blade.GeometricSpace.ZeroVectorBlade
            );

        var point1 = new CGaFloat64Blade(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        var point2 = new CGaFloat64Blade(
            Blade.GeometricSpace,
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );

        return new Pair<CGaFloat64Blade>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaDirection()
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
    public CGaFloat64Blade VGaNormalDirection()
    {
        return VGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Radius()
    {
        return RadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double RadiusSquared()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX2 =
            cgaGeometricSpace.Ei.Lcp(Blade).SpSquared();

        return Blade.Sp(Blade.GradeInvolution()) / eiX2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double HyperSphereWeight()
    {
        return Blade.CGaDual().Decode.IpnsRound.HyperSphereWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight2D(LinFloat64Vector2D egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight3D(LinFloat64Vector3D egaProbePoint)
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
    public double Weight(CGaFloat64Blade egaProbePoint)
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