using System.Collections.Immutable;
using System.Data;
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

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public class CGaFlat<T> :
    CGaElement<T>
{
    public override CGaBlade<T> Position { get; }

    public override Scalar<T> RadiusSquared
    {
        get => Position.GeometricSpace.ScalarZero;
        set => throw new ReadOnlyException();
    }

    public Scalar<T> OriginToHyperPlaneDistance
    {
        get
        {
            Debug.Assert(Direction.Grade == VSpaceDimensions - 3);

            return Position.Lcp(NormalDirectionToXGaVector()).InternalScalar.ToScalar();
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFlat(CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> weight, CGaBlade<T> position, CGaBlade<T> direction)
        : base(
            cgaGeometricSpace,
            CGaElementKind.Flat,
            weight,
            direction
        )
    {
        Position = position;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return Weight >= 0 &&
               Direction.IsVGaBlade() &&
               Position.IsVGaVector() &&
               Direction.Norm().IsNearOne();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(CGaElement<T> element2, bool ignoreWeight = false)
    {
        if (element2 is not CGaFlat<T> flat2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqualTo(element2.Weight))
            return false;

        if (!Direction.IsNearEqual(flat2.Direction))
            return false;

        if (!SurfaceNearContainsPoint(element2.Position))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaBlade<T> EncodeOpnsBlade()
    {
        return Weight * GeometricSpace.Eo.Op(Direction.Op(GeometricSpace.Ei)).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaBlade<T> EncodeIpnsBlade()
    {
        return Weight * Direction.VGaDual().GradeInvolution().TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> EncodePGaBlade()
    {
        return EncodeIpnsBlade().IpnsToPGa();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> GetSurfacePointVectors2D()
    {
        var pointList = new List<LinVector2D<T>>(Direction.Grade + 1)
        {
            PositionToVector2D()
        };

        pointList.AddRange(
            DirectionToVectors2D()
                .Select(v =>
                    PositionToVector2D() + v
                )
        );

        return pointList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> GetSurfacePointVectors3D()
    {
        var pointList = new List<LinVector3D<T>>(Direction.Grade + 1)
        {
            PositionToVector3D()
        };

        pointList.AddRange(
            DirectionToVectors3D()
                .Select(v =>
                    PositionToVector3D() + v
                )
        );

        return pointList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> GetSurfacePointVectors()
    {
        var pointList = new List<XGaVector<T>>(Direction.Grade + 1)
        {
            PositionToXGaVector()
        };

        pointList.AddRange(
            DirectionToXGaVectors()
                .Select(v =>
                    PositionToXGaVector() + v
                )
        );

        return pointList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointVGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.EncodeVGa.Vector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointPGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.EncodePGa.Point)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.EncodeIpnsRound.Point)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.EncodeOpnsFlat.Point)
            .ToImmutableArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(LinVector2D<T> egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToVector2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(LinVector3D<T> egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(LinVector<T> egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(XGaVector<T> egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToXGaVector());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(Scalar<T> egaPointX, Scalar<T> egaPointY)
    {
        var egaPoint = LinVector2D<T>.Create(egaPointX, egaPointY);

        return IsDirectionNearParallelTo(egaPoint - PositionToVector2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinVector2D<T> egaPoint)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(Scalar<T> egaPointX, Scalar<T> egaPointY, Scalar<T> egaPointZ)
    {
        var egaPoint = LinVector3D<T>.Create(egaPointX, egaPointY, egaPointZ);

        return IsDirectionNearParallelTo(egaPoint - PositionToVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinVector3D<T> egaPoint)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinVector<T> egaPoint)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(XGaVector<T> egaPoint)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToXGaVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(CGaBlade<T> egaPoint)
    {
        return IsDirectionNearParallelTo(
            (egaPoint - Position).Decode.VGaDirection.Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (Weight.IsNearZero())
            return "Zero Conformal Flat";

        return new StringBuilder()
            .AppendLine("Conformal Flat:")
            .AppendLine($"   Weight: ${BasisSpecs.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   Position: ${Position.ToLaTeX()}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .AppendLine($"    PGA Blade: ${EncodePGaBlade().ToLaTeX()}$")
            .ToString();
    }
}