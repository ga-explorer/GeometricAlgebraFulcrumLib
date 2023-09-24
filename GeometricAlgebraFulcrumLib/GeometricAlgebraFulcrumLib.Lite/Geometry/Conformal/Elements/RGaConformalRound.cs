using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public class RGaConformalRound :
    RGaConformalElement
{
    public RGaConformalBlade Center
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
    internal RGaConformalRound(RGaConformalSpace conformalSpace, double weight, double radiusSquared, RGaConformalBlade position, RGaConformalBlade direction)
        : base(
            conformalSpace, 
            RGaConformalElementKind.Round, 
            weight, 
            position, 
            direction
        )
    {
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
               Direction.IsEGaBlade() &&
               Direction.Norm().IsNearOne() &&
               Center.IsEGaBlade() &&
               RadiusSquared.IsValid();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(RGaConformalElement element2, bool ignoreWeight = false)
    {
        if (element2 is not RGaConformalRound round2)
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
    public override RGaConformalBlade EncodeOpnsBlade()
    {
        return Weight * (ConformalSpace.Eo + 0.5 * RadiusSquared * ConformalSpace.Ei)
            .Op(Direction)
            .TranslateBy(Position);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaConformalBlade EncodeIpnsBlade()
    {
        var direction = 
            ((VSpaceDimensions - 2).IsEven() ? Direction : -Direction).EGaDual();

        return Weight * (ConformalSpace.Eo - 0.5 * RadiusSquared * ConformalSpace.Ei)
            .Op(direction)
            .TranslateBy(Position);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector CenterToRGaVector()
    {
        return PositionToRGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D CenterToVector2D()
    {
        return PositionToVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D CenterToVector3D()
    {
        return PositionToVector3D();
    }

    
    public Float64Vector2D RoundSurfacePointToVector2D(Float64Vector2D egaProbeDirection, double distanceFromSurface)
    {
        if (IsPoint)
            return PositionToVector2D() + 
                   egaProbeDirection.SetLength(distanceFromSurface);

        if (IsRoundPointPair)
            return PositionToVector2D() + 
                   egaProbeDirection
                       .ProjectOnVector(DirectionToVector2D())
                       .SetLength(distanceFromSurface + RealRadius);

        if (IsRoundCircle)
            return PositionToVector2D() + 
                   egaProbeDirection.SetLength(RealRadius + distanceFromSurface);

        return PositionToVector2D() + 
               egaProbeDirection.SetLength(distanceFromSurface);
    }
    
    public Float64Vector3D RoundSurfacePointToVector3D(Float64Vector3D egaProbeDirection, double distanceFromSurface)
    {
        if (IsPoint)
            return PositionToVector3D() + 
                   egaProbeDirection.SetLength(distanceFromSurface);

        if (IsRoundPointPair)
            return PositionToVector3D() + 
                   egaProbeDirection.ProjectOnVector(DirectionToVector3D()).SetLength(RealRadius + distanceFromSurface);

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
    public IReadOnlyList<Float64Vector2D> GetSurfacePointVectors2D()
    {
        var directionVectors =
            DirectionToVectors2D().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                Float64Vector2D.Zero,
                (a, b) => a + b
            ).DivideByNorm()
        );

        return directionVectors
            .Select(v => PositionToVector2D() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector3D> GetSurfacePointVectors3D()
    {
        var directionVectors =
            DirectionToVectors3D().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                Float64Vector3D.Zero,
                (a, b) => a + b
            ).DivideByNorm()
        );

        return directionVectors
            .Select(v => PositionToVector3D() + v * RealRadius)
            .ToImmutableArray();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector> GetSurfacePointVectors()
    {
        var directionVectors =
            DirectionToVectors().ToList();

        directionVectors.Add(
            -directionVectors.Aggregate(
                Float64Vector.ZeroVector,
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
                ConformalSpace.EuclideanProcessor.CreateZeroVector(),
                (a, b) => a + b
            ).DivideByNorm()
        );

        return directionVectors
            .Select(v => PositionToRGaVector() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointEGaVectorBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(ConformalSpace.EncodeEGaVector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointPGaVectorBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(ConformalSpace.EncodePGaPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(ConformalSpace.EncodeIpnsRoundPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointRGaVectors()
            .Select(ConformalSpace.EncodeOpnsFlatPoint)
            .ToImmutableArray();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsEGaPoint2D(Float64Vector2D egaPoint)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsEGaPoint3D(Float64Vector3D egaPoint)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsEGaPoint(RGaFloat64Vector egaPoint)
    {
        var v = egaPoint - PositionToRGaVector();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsEGaPoint2D(Float64Vector2D egaPoint, double epsilon = 1e-12)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionNearParallelTo(v, epsilon) &&
               (v.Norm() - RealRadius).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsEGaPoint3D(Float64Vector3D egaPoint, double epsilon = 1e-12)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionNearParallelTo(v, epsilon) &&
               (v.Norm() - RealRadius).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsEGaPoint(RGaFloat64Vector egaPoint, double epsilon = 1e-12)
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
            .AppendLine($"   Weight: ${ConformalSpace.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   Position: ${Position.ToLaTeX()}$")
            .AppendLine($"   Squared Radius: ${ConformalSpace.ToLaTeX(RadiusSquared)}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}