using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public class XGaConformalRound<T> :
    XGaConformalElement<T>
{
    public override XGaConformalBlade<T> Position { get; }

    public XGaConformalBlade<T> Center
        => Position;
    
    public override Scalar<T> RadiusSquared { get; set; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalRound(XGaConformalSpace<T> conformalSpace, IScalar<T> weight, IScalar<T> radiusSquared, XGaConformalBlade<T> position, XGaConformalBlade<T> direction)
        : base(
            conformalSpace, 
            XGaConformalElementKind.Round, 
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
               Weight >= 0 &&
               Direction.IsEGaBlade() &&
               Position.IsEGaVector() &&
               Direction.Norm().IsNearOne() &&
               RadiusSquared.IsValid();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(XGaConformalElement<T> element2, bool ignoreWeight = false)
    {
        if (element2 is not XGaConformalRound<T> round2)
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
    public override XGaConformalBlade<T> EncodeOpnsBlade()
    {
        return Weight * (ConformalSpace.Eo + RadiusSquared * ConformalSpace.EiByTwo)
            .Op(Direction)
            .TranslateBy(Position);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaConformalBlade<T> EncodeIpnsBlade()
    {
        var direction = 
            ((VSpaceDimensions - 2).IsEven() ? Direction : -Direction).EGaDual();

        return Weight * (ConformalSpace.Eo - RadiusSquared * ConformalSpace.EiByTwo)
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
                ConformalSpace.EuclideanProcessor.VectorZero,
                (a, b) => a + b
            ).DivideByNorm()
        );

        return directionVectors
            .Select(v => PositionToXGaVector() + v * RealRadius)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointEGaVectorBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(ConformalSpace.EncodeEGaVector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointPGaVectorBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(ConformalSpace.EncodePGaPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(ConformalSpace.EncodeIpnsRoundPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointXGaVectors()
            .Select(ConformalSpace.EncodeOpnsFlatPoint)
            .ToImmutableArray();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsEGaPoint2D(LinVector2D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsEGaPoint3D(LinVector3D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsEGaPoint(XGaVector<T> egaPoint)
    {
        var v = egaPoint - PositionToXGaVector();

        return IsDirectionParallelTo(v) &&
               v.NormSquared() == RealRadiusSquared;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsEGaPoint2D(LinVector2D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector2D();

        return IsDirectionNearParallelTo(v) &&
               (v.Norm() - RealRadius).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsEGaPoint3D(LinVector3D<T> egaPoint)
    {
        var v = egaPoint - PositionToVector3D();

        return IsDirectionNearParallelTo(v) &&
               (v.Norm() - RealRadius).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsEGaPoint(XGaVector<T> egaPoint)
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