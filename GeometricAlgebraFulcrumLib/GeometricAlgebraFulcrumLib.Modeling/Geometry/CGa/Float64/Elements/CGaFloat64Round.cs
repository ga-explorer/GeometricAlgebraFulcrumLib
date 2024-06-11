using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public class CGaFloat64Round :
    CGaFloat64Element
{
    public override CGaFloat64Blade Position { get; }

    public CGaFloat64Blade Center
        => Position;

    private double _radiusSquared;
    public override double RadiusSquared
    {
        get => _radiusSquared;
        set => _radiusSquared = value.IsValid() && !value.IsInfinite()
            ? value
            : throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Round(CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, CGaFloat64Blade position, CGaFloat64Blade direction)
        : base(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Round,
            weight,
            direction
        )
    {
        Position = position;

        _radiusSquared =
            Direction.IsScalar ||
            Weight.IsNearZero() ||
            radiusSquared.IsNearZero()
                ? 0d : radiusSquared;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return Weight.IsValid() &&
               Weight >= 0 &&
               Direction.IsVGaBlade() &&
               Position.IsVGaVector() &&
               Direction.Norm().IsNearOne() &&
               RadiusSquared.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(CGaFloat64Element element2, bool ignoreWeight = false)
    {
        if (element2 is not CGaFloat64Round round2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqual(element2.Weight))
            return false;

        if (!RadiusSquared.IsNearEqual(round2.RadiusSquared))
            return false;

        if (!Center.IsNearEqual(round2.Center))
            return false;

        if (!Direction.IsNearEqual(round2.Direction))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeOpnsBlade()
    {
        return Weight * (GeometricSpace.Eo + 0.5 * RadiusSquared * GeometricSpace.Ei)
            .Op(Direction)
            .TranslateBy(Position);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeIpnsBlade()
    {
        var direction =
            ((VSpaceDimensions - 2).IsEven() ? Direction : -Direction).VGaDual();

        return Weight * (GeometricSpace.Eo - 0.5 * RadiusSquared * GeometricSpace.Ei)
            .Op(direction)
            .TranslateBy(Position);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector CenterToRGaVector()
    {
        return PositionToRGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D CenterToVector2D()
    {
        return PositionToVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D CenterToVector3D()
    {
        return PositionToVector3D();
    }


    public LinFloat64Vector2D RoundSurfacePointToVector2D(LinFloat64Vector2D egaProbeDirection, double distanceFromSurface)
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

    public LinFloat64Vector3D RoundSurfacePointToVector3D(LinFloat64Vector3D egaProbeDirection, double distanceFromSurface)
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
    public IReadOnlyList<LinFloat64Vector2D> GetSurfacePointVectors2D()
    {
        var directionVectors =
            DirectionToVectors2D().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                LinFloat64Vector2D.Zero,
                (a, b) => a + b
            ).VectorDivideByENorm()
        );

        return directionVectors
            .Select(v => PositionToVector2D() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector3D> GetSurfacePointVectors3D()
    {
        var directionVectors =
            DirectionToVectors3D().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                LinFloat64Vector3D.Zero,
                (a, b) => a + b
            ).VectorDivideByENorm()
        );

        return directionVectors
            .Select(v => PositionToVector3D() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector> GetSurfacePointVectors()
    {
        var directionVectors =
            DirectionToVectors().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                LinFloat64Vector.VectorZero,
                (a, b) => a + b
            ).DivideByENorm()
        );

        return directionVectors
            .Select(v => PositionToVector() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> GetSurfacePointRGaVectors()
    {
        var directionVectors =
            DirectionToRGaVectors().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                GeometricSpace.EuclideanProcessor.VectorZero,
                (a, b) => a + b
            ).DivideByNorm()
        );

        return directionVectors
            .Select(v => PositionToRGaVector() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointVGaVectorBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(GeometricSpace.EncodeVGaVector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointPGaVectorBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(GeometricSpace.EncodePGaPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(GeometricSpace.EncodeIpnsRoundPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(GeometricSpace.EncodeOpnsFlatPoint)
            .ToImmutableArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsVGaPoint2D(LinFloat64Vector2D egaPoint)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsVGaPoint3D(LinFloat64Vector3D egaPoint)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsVGaPoint(RGaFloat64Vector egaPoint)
    {
        var v = egaPoint - PositionToRGaVector();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsVGaPoint2D(LinFloat64Vector2D egaPoint, double epsilon = 1e-12)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionNearParallelTo(v, epsilon) &&
               (v.Norm() - RealRadius).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsVGaPoint3D(LinFloat64Vector3D egaPoint, double epsilon = 1e-12)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionNearParallelTo(v, epsilon) &&
               (v.Norm() - RealRadius).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsVGaPoint(RGaFloat64Vector egaPoint, double epsilon = 1e-12)
    {
        var v = egaPoint - PositionToRGaVector();

        return IsDirectionNearParallelTo(v, epsilon) &&
               (v.Norm() - RealRadius).IsNearZero(epsilon);
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