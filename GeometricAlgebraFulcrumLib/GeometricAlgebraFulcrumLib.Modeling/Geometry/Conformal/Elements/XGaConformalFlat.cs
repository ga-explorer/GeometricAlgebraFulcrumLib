using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public class XGaConformalFlat<T> :
    XGaConformalElement<T>
{
    public override XGaConformalBlade<T> Position { get; }

    public override Scalar<T> RadiusSquared
    {
        get => Position.ConformalSpace.ScalarZero;
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
    internal XGaConformalFlat(XGaConformalSpace<T> conformalSpace, IScalar<T> weight, XGaConformalBlade<T> position, XGaConformalBlade<T> direction)
        : base(
            conformalSpace, 
            XGaConformalElementKind.Flat, 
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
    public override bool IsSameElement(XGaConformalElement<T> element2, bool ignoreWeight = false)
    {
        if (element2 is not XGaConformalFlat<T> flat2)
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
    public override XGaConformalBlade<T> EncodeOpnsBlade()
    {
        return Weight * ConformalSpace.Eo.Op(Direction.Op(ConformalSpace.Ei)).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaConformalBlade<T> EncodeIpnsBlade()
    {
        return Weight * Direction.EGaDual().GradeInvolution().TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> EncodePGaBlade()
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
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointEGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodeEGaVector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointPGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodePGaPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointIpnsBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodeIpnsRoundPoint)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaConformalBlade<T>> GetSurfacePointOpnsFlatBlades()
    {
        return GetSurfacePointVectors()
            .Select(ConformalSpace.EncodeOpnsFlatPoint)
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
    public bool SurfaceNearContainsPoint(XGaConformalBlade<T> egaPoint)
    {
        return IsDirectionNearParallelTo(
            (egaPoint - Position).DecodeEGaVector()
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