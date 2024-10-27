using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public class CGaRound<T> :
    CGaElement<T>
{
    public override CGaBlade<T> Position { get; }

    public CGaBlade<T> Center
        => Position;

    public override Scalar<T> RadiusSquared { get; set; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaRound(CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> weight, IScalar<T> radiusSquared, CGaBlade<T> position, CGaBlade<T> direction)
        : base(
            cgaGeometricSpace,
            CGaElementKind.Round,
            weight,
            direction
        )
    {
        Position = position;

        RadiusSquared =
            Direction.IsScalar ||
            Weight.IsNearZero() ||
            radiusSquared.IsNearZero()
                ? ScalarProcessor.Zero : radiusSquared.ToScalar();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return Weight.IsValid() &&
               //Weight >= 0 &&
               Direction.IsVGaBlade() &&
               Position.IsVGaVector() &&
               //Direction.Norm().IsNearOne() &&
               RadiusSquared.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(CGaElement<T> element2, bool ignoreWeight = false)
    {
        if (element2 is not CGaRound<T> round2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqualTo(element2.Weight))
            return false;

        if (!RadiusSquared.IsNearEqualTo(round2.RadiusSquared))
            return false;

        if (!Center.IsNearEqual(round2.Center))
            return false;

        if (!Direction.IsNearEqual(round2.Direction))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaBlade<T> EncodeOpnsBlade()
    {
        return Weight * (GeometricSpace.Eo + RadiusSquared * GeometricSpace.EiByTwo)
            .Op(Direction)
            .TranslateBy(Position);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaBlade<T> EncodeIpnsBlade()
    {
        var direction =
            ((VSpaceDimensions - 2).IsEven() ? Direction : -Direction).VGaDual();

        return Weight * (GeometricSpace.Eo - RadiusSquared * GeometricSpace.EiByTwo)
            .Op(direction)
            .TranslateBy(Position);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> CenterToXGaVector()
    {
        return PositionToXGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> CenterToVector2D()
    {
        return PositionToVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> CenterToVector3D()
    {
        return PositionToVector3D();
    }


    public LinVector2D<T> RoundSurfacePointToVector2D(LinVector2D<T> egaProbeDirection, Scalar<T> distanceFromSurface)
    {
        if (IsPoint)
            return PositionToVector2D() +
                   egaProbeDirection.SetLength(distanceFromSurface);

        if (IsRoundPointPair)
            return PositionToVector2D() +
                   DirectionToVector2D().SetLength(distanceFromSurface + RealRadius);

        if (IsRoundCircle)
            return PositionToVector2D() +
                   egaProbeDirection.SetLength(RealRadius + distanceFromSurface);

        return PositionToVector2D() +
               egaProbeDirection.SetLength(distanceFromSurface);
    }

    public LinVector3D<T> RoundSurfacePointToVector3D(LinVector3D<T> egaProbeDirection, Scalar<T> distanceFromSurface)
    {
        if (IsPoint)
            return PositionToVector3D() +
                   egaProbeDirection.SetLength(distanceFromSurface);

        if (IsRoundPointPair)
            return PositionToVector3D() +
                   DirectionToVector3D().SetLength(RealRadius + distanceFromSurface);

        if (IsRoundCircle)
            return PositionToVector3D() +
                   egaProbeDirection.ProjectOnBivector(DirectionToBivector3D()).SetLength(RealRadius + distanceFromSurface);

        if (IsRoundSphere)
            return PositionToVector3D() +
                   egaProbeDirection.SetLength(RealRadius + distanceFromSurface);

        return PositionToVector3D() +
               egaProbeDirection.SetLength(distanceFromSurface);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> GetSurfacePointVectors2D()
    {
        var directionVectors =
            DirectionToVectors2D().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                LinVector2D<T>.Zero(ScalarProcessor),
                (a, b) => a + b
            ).VectorDivideByNorm()
        );

        return directionVectors
            .Select(v => PositionToVector2D() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> GetSurfacePointVectors3D()
    {
        var directionVectors =
            DirectionToVectors3D().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                LinVector3D<T>.Zero(ScalarProcessor),
                (a, b) => a + b
            ).VectorDivideByENorm()
        );

        return directionVectors
            .Select(v => PositionToVector3D() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector<T>> GetSurfacePointVectors()
    {
        var directionVectors =
            DirectionToVectors().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                LinVector<T>.Zero(ScalarProcessor),
                (a, b) => a + b
            ).DivideByENorm()
        );

        return directionVectors
            .Select(v => PositionToVector() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> GetSurfacePointXGaVectors()
    {
        var directionVectors =
            DirectionToXGaVectors().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                GeometricSpace.EuclideanProcessor.VectorZero,
                (a, b) => a + b
            ).DivideByNorm()
        );

        return directionVectors
            .Select(v => PositionToXGaVector() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointVGaVectorBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(GeometricSpace.EncodeVGa.Vector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointPGaVectorBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(GeometricSpace.EncodePGa.Point)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(GeometricSpace.EncodeIpnsRound.Point)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(GeometricSpace.EncodeOpnsFlat.Point)
            .ToImmutableArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsVGaPoint2D(LinVector2D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsVGaPoint3D(LinVector3D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsVGaPoint(XGaVector<T> egaPoint)
    {
        var v = egaPoint - PositionToXGaVector();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsVGaPoint2D(LinVector2D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionNearParallelTo(v) &&
               (v.Norm() - RealRadius).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsVGaPoint3D(LinVector3D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionNearParallelTo(v) &&
               (v.Norm() - RealRadius).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsVGaPoint(XGaVector<T> egaPoint)
    {
        var v = egaPoint - PositionToXGaVector();

        return IsDirectionNearParallelTo(v) &&
               (v.Norm() - RealRadius).IsNearZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (Weight.IsNearZero())
            return "Zero Conformal Round";

        return new StringBuilder()
            .AppendLine("Conformal Round:")
            .AppendLine($"   Weight: ${BasisSpecs.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   Position: ${Position.ToLaTeX()}$")
            .AppendLine($"   Squared Radius: ${BasisSpecs.ToLaTeX(RadiusSquared)}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}