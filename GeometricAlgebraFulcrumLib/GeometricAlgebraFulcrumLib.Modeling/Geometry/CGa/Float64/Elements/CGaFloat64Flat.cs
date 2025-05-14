using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public class CGaFloat64Flat :
    CGaFloat64Element
{
    public override CGaFloat64Blade Position { get; }

    public override double RadiusSquared
    {
        get => 0d;
        set => throw new ReadOnlyException();
    }

    public double OriginToHyperPlaneDistance
    {
        get
        {
            Debug.Assert(Direction.Grade == VSpaceDimensions - 3);

            return Position.Lcp(NormalDirectionToXGaVector()).InternalScalarValue;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Flat(CGaFloat64GeometricSpace cgaGeometricSpace, double weight, CGaFloat64Blade position, CGaFloat64Blade direction)
        : base(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Flat,
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
    public override bool IsSameElement(CGaFloat64Element element2, bool ignoreWeight = false)
    {
        if (element2 is not CGaFloat64Flat flat2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqual(element2.Weight))
            return false;

        if (!Direction.IsNearEqual(flat2.Direction))
            return false;

        if (!SurfaceNearContainsPoint(element2.Position))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeOpnsBlade()
    {
        return Weight * GeometricSpace.Eo.Op(Direction.Op(GeometricSpace.Ei)).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeIpnsBlade()
    {
        return Weight * Direction.VGaDual().GradeInvolution().TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade EncodePGaBlade()
    {
        return EncodeIpnsBlade().IpnsToPGa();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector2D> GetSurfacePointVectors2D()
    {
        var pointList = new List<LinFloat64Vector2D>(Direction.Grade + 1)
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
    public IReadOnlyList<LinFloat64Vector3D> GetSurfacePointVectors3D()
    {
        var pointList = new List<LinFloat64Vector3D>(Direction.Grade + 1)
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
    public IReadOnlyList<XGaFloat64Vector> GetSurfacePointVectors()
    {
        var pointList = new List<XGaFloat64Vector>(Direction.Grade + 1)
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
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointVGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.Encode.VGa.Vector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointPGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.Encode.PGa.Point)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.Encode.IpnsRound.Point)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointVectors()
            .Select(GeometricSpace.Encode.OpnsFlat.Point)
            .ToImmutableArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(LinFloat64Vector2D egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToVector2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(LinFloat64Vector3D egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(LinFloat64Vector egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceContainsPoint(XGaFloat64Vector egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToXGaVector());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(double egaPointX, double egaPointY, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var egaPoint = LinFloat64Vector2D.Create(egaPointX, egaPointY);

        return IsDirectionNearParallelTo(egaPoint - PositionToVector2D(), zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinFloat64Vector2D egaPoint, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector2D(), zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(double egaPointX, double egaPointY, double egaPointZ, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var egaPoint = LinFloat64Vector3D.Create(egaPointX, egaPointY, egaPointZ);

        return IsDirectionNearParallelTo(egaPoint - PositionToVector3D(), zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinFloat64Vector3D egaPoint, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector3D(), zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinFloat64Vector egaPoint, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector(), zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(XGaFloat64Vector egaPoint, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToXGaVector(), zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(CGaFloat64Blade egaPoint, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return IsDirectionNearParallelTo(
            (egaPoint - Position).DecodeVGaDirection.XGaVector(),
            zeroEpsilon
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