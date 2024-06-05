using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

public class RGaConformalFlat :
    RGaConformalElement
{
    public override RGaConformalBlade Position { get; }

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

            return Position.Lcp(NormalDirectionToRGaVector()).InternalScalarValue;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalFlat(RGaConformalSpace conformalSpace, double weight, RGaConformalBlade position, RGaConformalBlade direction)
        : base(
            conformalSpace, 
            RGaConformalElementKind.Flat, 
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
               Direction.IsEGaBlade() &&
               Position.IsEGaVector() &&
               Direction.Norm().IsNearOne();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(RGaConformalElement element2, bool ignoreWeight = false)
    {
        if (element2 is not RGaConformalFlat flat2)
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
    public override RGaConformalBlade EncodeOpnsBlade()
    {
        return Weight * ConformalSpace.Eo.Op(Direction.Op(ConformalSpace.Ei)).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaConformalBlade EncodeIpnsBlade()
    {
        return Weight * Direction.EGaDual().GradeInvolution().TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade EncodePGaBlade()
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
    public IReadOnlyList<RGaFloat64Vector> GetSurfacePointVectors()
    {
        var pointList = new List<RGaFloat64Vector>(Direction.Grade + 1)
        {
            PositionToRGaVector()
        };

        pointList.AddRange(
            DirectionToRGaVectors()
                .Select(v =>
                    PositionToRGaVector() + v
                )
        );

        return pointList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointEGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodeEGaVector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointPGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodePGaPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodeIpnsRoundPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodeOpnsFlatPoint)
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
    public bool SurfaceContainsPoint(RGaFloat64Vector egaPoint)
    {
        return IsDirectionParallelTo(egaPoint - PositionToRGaVector());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(double egaPointX, double egaPointY, double epsilon = 1e-12)
    {
        var egaPoint = LinFloat64Vector2D.Create(egaPointX, egaPointY);

        return IsDirectionNearParallelTo(egaPoint - PositionToVector2D(), epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinFloat64Vector2D egaPoint, double epsilon = 1e-12)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector2D(), epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(double egaPointX, double egaPointY, double egaPointZ, double epsilon = 1e-12)
    {
        var egaPoint = LinFloat64Vector3D.Create(egaPointX, egaPointY, egaPointZ);

        return IsDirectionNearParallelTo(egaPoint - PositionToVector3D(), epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinFloat64Vector3D egaPoint, double epsilon = 1e-12)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector3D(), epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(LinFloat64Vector egaPoint, double epsilon = 1e-12)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToVector(), epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(RGaFloat64Vector egaPoint, double epsilon = 1e-12)
    {
        return IsDirectionNearParallelTo(egaPoint - PositionToRGaVector(), epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SurfaceNearContainsPoint(RGaConformalBlade egaPoint, double epsilon = 1e-12)
    {
        return IsDirectionNearParallelTo(
            (egaPoint - Position).DecodeEGaVector(), 
            epsilon
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