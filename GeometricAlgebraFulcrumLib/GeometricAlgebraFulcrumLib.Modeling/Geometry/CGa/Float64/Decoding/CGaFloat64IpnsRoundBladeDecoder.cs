using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64IpnsRoundBladeDecoder :
    CGaFloat64BladeDecoderBase
{
    internal CGaFloat64IpnsRoundBladeDecoder(CGaFloat64Blade blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Sphere2D()
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

    public CGaFloat64Round HyperSphere()
    {
        Debug.Assert(Blade.IsVector);

        var cgaGeometricSpace = Blade.GeometricSpace;

        var weight =
            Blade[0] + Blade[1];

        if (weight.IsNearZero())
            return new CGaFloat64Round(
                cgaGeometricSpace,
                0,
                0,
                cgaGeometricSpace.ZeroVectorBlade,
                cgaGeometricSpace.Ie
            );

        var position =
            Blade
                .InternalVector
                .GetVectorPart((int i) => i >= 2)
                .Divide(weight)
                .ToConformalBlade(cgaGeometricSpace);

        var eiScalar =
            0.5 * (Blade[0] - Blade[1]);

        var radiusSquared =
            position.NormSquared() - 2 * eiScalar / weight;

        var round = new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            position,
            cgaGeometricSpace.Ie
        );

        //Debug.Assert(position.Lcp(Blade).IsNearZero());

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    public CGaFloat64Round Element(CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX = cgaGeometricSpace.Ei.Lcp(Blade);
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
            Blade
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var radiusSquared =
            -(Blade.Sp(Blade.GradeInvolution()) / eiX2);

        //var position =
        //    IpnsSphereToVGaCenter(
        //        -0.5 / eiX2 * Blade.Gp(Ei).Gp(Blade).GetVectorPart()
        //    );

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(Blade.CGaUnDual())
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

        //Debug.Assert(position.Lcp(Blade).IsNearZero());

        return round;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public RGaConformalParametricElement Element(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D CircleVGaCenter2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        var eoScalar = Blade[0] + Blade[1];

        if (eoScalar.IsNearZero())
            return LinFloat64Vector2D.Zero;

        return LinFloat64Vector2D.Create(
            Blade[2] / eoScalar,
            Blade[3] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D CircleVGaCenter3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        var eoScalar = Blade[0] + Blade[1];

        if (eoScalar.IsNearZero())
            return LinFloat64Vector3D.Zero;

        return LinFloat64Vector3D.Create(
            Blade[2] / eoScalar,
            Blade[3] / eoScalar,
            Blade[4] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperSphereVGaCenter()
    {
        Debug.Assert(Blade.IsVector);

        return Blade.InternalVector.DecodeIpnsHyperSphereVGaCenter(
            Blade.GeometricSpace
        );
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

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<double, LinFloat64Vector2D> CircleWeightVGaCenter2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        var weight = Blade[0] + Blade[1];

        if (weight.IsNearZero())
            return new Tuple<double, LinFloat64Vector2D>(
                0,
                LinFloat64Vector2D.Zero
            );

        var egaPoint = LinFloat64Vector2D.Create(
            Blade[2] / weight,
            Blade[3] / weight
        );

        return new Tuple<double, LinFloat64Vector2D>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<double, LinFloat64Vector3D> SphereWeightVGaCenter3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        var weight = Blade[0] + Blade[1];

        if (weight.IsNearZero())
            return new Tuple<double, LinFloat64Vector3D>(
                0,
                LinFloat64Vector3D.Zero
            );

        var egaPoint = LinFloat64Vector3D.Create(
            Blade[2] / weight,
            Blade[3] / weight,
            Blade[4] / weight
        );

        return new Tuple<double, LinFloat64Vector3D>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<double, CGaFloat64Blade> HyperSphereWeightVGaCenter()
    {
        Debug.Assert(Blade.IsVector);

        return Blade.InternalVector.DecodeIpnsHyperSphereWeightVGaCenter(
            Blade.GeometricSpace
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPairIpnsPoint1()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return Blade.GeometricSpace.Encode.IpnsRound.Point( 
            pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPairIpnsPoint2()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return Blade.GeometricSpace.Encode.IpnsRound.Point( 
            pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPairVGaPoint1()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToRGaVector() - pointPair.RealRadius * pointPair.DirectionToRGaVector())
            .EncodeVGaVector(Blade.GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPairVGaPoint2()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToRGaVector() + pointPair.RealRadius * pointPair.DirectionToRGaVector())
            .EncodeVGaVector(Blade.GeometricSpace);
    }
    
    public Pair<CGaFloat64Blade> PointPairIpnsPoints()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaFloat64Blade>(
                Blade.GeometricSpace.ZeroVectorBlade,
                Blade.GeometricSpace.ZeroVectorBlade
            );

        var center = pointPair.CenterToRGaVector();
        var direction = pointPair.DirectionToRGaVector();

        var point1 =
            pointPair.GeometricSpace.Encode.IpnsRound.Point(
                center - pointPair.RealRadius * direction
            );

        var point2 =
            pointPair.GeometricSpace.Encode.IpnsRound.Point(
                center + pointPair.RealRadius * direction
            );

        return new Pair<CGaFloat64Blade>(point1, point2);
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

        var center = pointPair.CenterToRGaVector();
        var direction = pointPair.DirectionToRGaVector();

        var point1 =
            (center - pointPair.RealRadius * direction).EncodeVGaVector(Blade.GeometricSpace);

        var point2 =
            (center + pointPair.RealRadius * direction).EncodeVGaVector(Blade.GeometricSpace);

        return new Pair<CGaFloat64Blade>(point1, point2);
    }

    public Pair<LinFloat64Vector2D> PointPairVGaPointsAsVector2D()
    {
        var pointPair =
            Element();

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

    public Pair<LinFloat64Vector3D> PointPairVGaPointsAsVector3D()
    {
        var pointPair =
            Element();

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
    public CGaFloat64Blade VGaDirection()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(Blade.CGaUnDual())
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

        return eiX2.IsNearZero()
            ? 0d
            : -(Blade.Sp(Blade.GradeInvolution()) / eiX2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double HyperSphereWeight()
    {
        Debug.Assert(Blade.IsVector);

        return Blade[0] + Blade[1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector2D egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector3D egaProbePoint)
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
                .Lcp(Blade.CGaUnDual())
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }
}