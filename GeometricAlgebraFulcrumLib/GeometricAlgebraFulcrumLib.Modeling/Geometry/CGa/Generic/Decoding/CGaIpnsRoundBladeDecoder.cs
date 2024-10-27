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

public sealed class CGaIpnsRoundBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    internal CGaIpnsRoundBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> Sphere2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        return HyperSphere();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> Sphere3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        return HyperSphere();
    }

    public CGaRound<T> HyperSphere()
    {
        Debug.Assert(Blade.IsVector);

        var cgaGeometricSpace = Blade.GeometricSpace;
        var scalarProcessor = Blade.ScalarProcessor;

        var weight =
            Blade[0] + Blade[1];

        if (weight.IsNearZero())
            return new CGaRound<T>(
                cgaGeometricSpace,
                scalarProcessor.Zero,
                scalarProcessor.Zero,
                cgaGeometricSpace.ZeroVectorBlade,
                cgaGeometricSpace.Ie
            );

        var position =
            Blade
                .InternalVector
                .GetVectorPart(i => i >= 2)
                .Divide(weight)
                .ToConformalBlade(cgaGeometricSpace);

        var eiScalar =
            0.5 * (Blade[0] - Blade[1]);

        var radiusSquared =
            position.NormSquared() - 2 * eiScalar / weight;

        var round = new CGaRound<T>(
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
    public CGaRound<T> Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    public CGaRound<T> Element(CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;
        var scalarProcessor = cgaGeometricSpace.ScalarProcessor;

        var eiX = cgaGeometricSpace.Ei.Lcp(Blade);
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
            Blade
                .Gp(-eiX.Inverse())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var radiusSquared =
            -Blade.Sp(Blade.GradeInvolution()).Divide(eiX2);

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

        var round = new CGaRound<T>(
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
    //public XGaConformalParametricElement<T> <T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> <T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> <T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> <T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsRound)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> CircleVGaCenter2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        var eoScalar = Blade[0] + Blade[1];

        if (eoScalar.IsNearZero())
            return LinVector2D<T>.Zero(Blade.ScalarProcessor);

        return LinVector2D<T>.Create(
            Blade[2] / eoScalar,
            Blade[3] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> CircleVGaCenter3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        var eoScalar = Blade[0] + Blade[1];

        if (eoScalar.IsNearZero())
            return LinVector3D<T>.Zero(Blade.ScalarProcessor);

        return LinVector3D<T>.Create(
            Blade[2] / eoScalar,
            Blade[3] / eoScalar,
            Blade[4] / eoScalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperSphereVGaCenter()
    {
        Debug.Assert(Blade.IsVector);

        return Blade.InternalVector.DecodeIpnsHyperSphereVGaCenter(
            Blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaCenter()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX =
            cgaGeometricSpace.Ei.Lcp(Blade);

        return Blade
            .Gp(eiX.Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<Scalar<T>, LinVector2D<T>> CircleWeightVGaCenter2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        var weight = Blade[0] + Blade[1];

        if (weight.IsNearZero())
            return new Tuple<Scalar<T>, LinVector2D<T>>(
                Blade.ScalarProcessor.Zero,
                LinVector2D<T>.Zero(Blade.ScalarProcessor)
            );

        var egaPoint = LinVector2D<T>.Create(
            Blade[2] / weight,
            Blade[3] / weight
        );

        return new Tuple<Scalar<T>, LinVector2D<T>>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<Scalar<T>, LinVector3D<T>> SphereWeightVGaCenter3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        var weight = Blade[0] + Blade[1];

        if (weight.IsNearZero())
            return new Tuple<Scalar<T>, LinVector3D<T>>(
                Blade.ScalarProcessor.Zero,
                LinVector3D<T>.Zero(Blade.ScalarProcessor)
            );

        var egaPoint = LinVector3D<T>.Create(
            Blade[2] / weight,
            Blade[3] / weight,
            Blade[4] / weight
        );

        return new Tuple<Scalar<T>, LinVector3D<T>>(weight, egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<Scalar<T>, CGaBlade<T>> HyperSphereWeightVGaCenter()
    {
        Debug.Assert(Blade.IsVector);

        return Blade.InternalVector.DecodeIpnsHyperSphereWeightVGaCenter(
            Blade.GeometricSpace
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPairIpnsPoint1()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return Blade.GeometricSpace.EncodeIpnsRound.Point(
            pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPairIpnsPoint2()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return Blade.GeometricSpace.EncodeIpnsRound.Point(
            pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector()
        );
    }
    
    public Pair<CGaBlade<T>> PointPairIpnsPoints()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaBlade<T>>(
                Blade.GeometricSpace.ZeroVectorBlade,
                Blade.GeometricSpace.ZeroVectorBlade
            );

        var center = pointPair.CenterToXGaVector();
        var direction = pointPair.DirectionToXGaVector();

        var point1 =
            Blade.GeometricSpace.EncodeIpnsRound.Point(center - pointPair.RealRadius * direction);

        var point2 =
            Blade.GeometricSpace.EncodeIpnsRound.Point(center + pointPair.RealRadius * direction);

        return new Pair<CGaBlade<T>>(point1, point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPairVGaPoint1()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToXGaVector() - pointPair.RealRadius * pointPair.DirectionToXGaVector())
            .EncodeVGaVector(Blade.GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPairVGaPoint2()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return Blade.GeometricSpace.ZeroVectorBlade;

        return (pointPair.CenterToXGaVector() + pointPair.RealRadius * pointPair.DirectionToXGaVector())
            .EncodeVGaVector(Blade.GeometricSpace);
    }
    
    public LinVector2D<T> PointPairVGaPoint1AsVector2D()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return LinVector2D<T>.Zero(Blade.ScalarProcessor);

        var center = pointPair.CenterToVector2D();
        var direction = pointPair.DirectionToVector2D();

        return center - pointPair.RealRadius * direction;
    }
    
    public LinVector2D<T> PointPairVGaPoint2AsVector2D()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return LinVector2D<T>.Zero(Blade.ScalarProcessor);

        var center = pointPair.CenterToVector2D();
        var direction = pointPair.DirectionToVector2D();

        return center + pointPair.RealRadius * direction;
    }

    public LinVector3D<T> PointPairVGaPoint1AsVector3D()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return LinVector3D<T>.Zero(Blade.ScalarProcessor);

        var center = pointPair.CenterToVector3D();
        var direction = pointPair.DirectionToVector3D();

        return center - pointPair.RealRadius * direction;
    }
    
    public LinVector3D<T> PointPairVGaPoint2AsVector3D()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return LinVector3D<T>.Zero(Blade.ScalarProcessor);

        var center = pointPair.CenterToVector3D();
        var direction = pointPair.DirectionToVector3D();

        return center + pointPair.RealRadius * direction;
    }

    public Pair<CGaBlade<T>> PointPairVGaPoints()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return new Pair<CGaBlade<T>>(
                Blade.GeometricSpace.ZeroVectorBlade,
                Blade.GeometricSpace.ZeroVectorBlade
            );

        var center = pointPair.CenterToXGaVector();
        var direction = pointPair.DirectionToXGaVector();

        var point1 =
            (center - pointPair.RealRadius * direction).EncodeVGaVector(Blade.GeometricSpace);

        var point2 =
            (center + pointPair.RealRadius * direction).EncodeVGaVector(Blade.GeometricSpace);

        return new Pair<CGaBlade<T>>(point1, point2);
    }

    public Pair<LinVector2D<T>> PointPairVGaPointsAsVector2D()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return new Pair<LinVector2D<T>>(
                LinVector2D<T>.Zero(Blade.ScalarProcessor),
                LinVector2D<T>.Zero(Blade.ScalarProcessor)
            );

        var center = pointPair.CenterToVector2D();
        var direction = pointPair.DirectionToVector2D();

        var point1 =
            center - pointPair.RealRadius * direction;

        var point2 =
            center + pointPair.RealRadius * direction;

        return new Pair<LinVector2D<T>>(point1, point2);
    }

    public Pair<LinVector3D<T>> PointPairVGaPointsAsVector3D()
    {
        var pointPair =
            Element();

        if (!pointPair.IsRoundPointPair)
            return new Pair<LinVector3D<T>>(
                LinVector3D<T>.Zero(Blade.ScalarProcessor),
                LinVector3D<T>.Zero(Blade.ScalarProcessor)
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
    public CGaBlade<T> VGaDirection()
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
    public CGaBlade<T> VGaNormalDirection()
    {
        return VGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Radius()
    {
        return RadiusSquared().SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> RadiusSquared()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var eiX2 =
            cgaGeometricSpace.Ei.Lcp(Blade).SpSquared();

        return eiX2.IsNearZero()
            ? Blade.ScalarProcessor.Zero
            : -Blade.Sp(Blade.GradeInvolution()).Divide(eiX2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> HyperSphereWeight()
    {
        Debug.Assert(Blade.IsVector);

        return Blade[0] + Blade[1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight2D(LinVector2D<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight3D(LinVector3D<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(LinVector<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(CGaBlade<T> egaProbePoint)
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