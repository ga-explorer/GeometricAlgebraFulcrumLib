using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Encoding;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Elements;

public sealed class XGaProjectiveElement<T> :
    IAlgebraicElement
{
    public XGaProjectiveElementSpecs<T> Specs { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => Specs.ProjectiveSpace.ScalarProcessor;

    public Scalar<T> Weight { get; set; }

    public XGaProjectiveBlade<T> Position { get; }
    
    public XGaProjectiveBlade<T> Direction 
        => NormalDirection.EGaUnNormal();
    
    /// <summary>
    /// The normal direction is the unit orthogonal subspace to the direction of
    /// this element such that their product is the Euclidean pseudo-scalar Ie
    /// (i.e. they form a right-handed coordinate system; a positive determinant)
    /// </summary>
    public XGaProjectiveBlade<T> NormalDirection { get; }
    
    public XGaProjectiveSpace<T> ProjectiveSpace 
        => Specs.ProjectiveSpace;
    
    public XGaProjectiveSpace3D<T> ProjectiveSpace3D 
        => Specs.ProjectiveSpace3D;
    
    public XGaProjectiveSpace4D<T> ProjectiveSpace4D 
        => Specs.ProjectiveSpace4D;

    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs 
        => Specs.ProjectiveSpace.BasisSpecs;

    public int VSpaceDimensions
        => ProjectiveSpace.VSpaceDimensions;

    public XGaProjectiveElementKind Kind 
        => Specs.Kind;
    
    public XGaProjectiveElementEncoding Encoding 
        => Specs.Encoding;


    public bool IsDirection 
        => Specs.IsDirection;
    
    public bool IsFlat
        => Specs.IsFlat;

    public bool IsPoint 
        => Direction.Grade == 0;
    
    public bool IsLine 
        => Direction.Grade == 1;
    
    public bool IsPlane 
        => Direction.Grade == 2;
    
    public bool IsVolume 
        => Direction.Grade == 3;
    
    public bool IsDirectionPoint 
        => IsDirection && Direction.Grade == 0;
    
    public bool IsDirectionLine 
        => IsDirection && Direction.Grade == 1;
    
    public bool IsDirectionPlane 
        => IsDirection && Direction.Grade == 2;
    
    public bool IsDirectionVolume 
        => IsDirection && Direction.Grade == 3;
    
    public bool IsDirectionHyperPlane 
        => IsDirection && Direction.Grade == VSpaceDimensions - 2;

    public bool IsFlatPoint 
        => IsFlat && Direction.Grade == 0;
    
    public bool IsFlatLine 
        => IsFlat && Direction.Grade == 1;
    
    public bool IsFlatPlane 
        => IsFlat && Direction.Grade == 2;
    
    public bool IsFlatVolume 
        => IsFlat && Direction.Grade == 3;
    
    public bool IsFlatHyperPlane 
        => IsFlat && Direction.Grade == VSpaceDimensions - 2;
    
    public Scalar<T> OriginToHyperPlaneDistance
    {
        get
        {
            Debug.Assert(Direction.Grade == VSpaceDimensions - 3);

            return Position.Lcp(NormalDirectionToXGaVector()).InternalScalar.ToScalar();
        }
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaProjectiveElement(XGaProjectiveSpace<T> projectiveSpace, IScalar<T> weight, XGaProjectiveBlade<T> position, XGaProjectiveBlade<T> direction)
    {
        Debug.Assert(
            position.IsEGaBlade() &&
            direction.IsEGaBlade()
        );

        Position = position;

        var directionNorm = direction.Norm();
        if (weight.IsValid() && !weight.IsNearZero() && !directionNorm.IsNearZero())
        {
            Weight = weight.ToScalar();
            NormalDirection = direction.Divide(directionNorm).EGaNormal();
        }
        else
        {
            Weight = projectiveSpace.ScalarProcessor.Zero;
            NormalDirection = projectiveSpace.OneScalarBlade.EGaNormal();
        }

        Specs = new XGaProjectiveElementSpecs<T>(
            projectiveSpace,
            XGaProjectiveElementKind.Euclidean,
            XGaProjectiveElementEncoding.EGa,
            Direction.Grade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaProjectiveElement(XGaProjectiveSpace<T> projectiveSpace, IScalar<T> weight, XGaProjectiveBlade<T> direction)
    {
        Debug.Assert(
            direction.IsEGaBlade()
        );

        Position = projectiveSpace.ZeroVectorBlade;

        var directionNorm = direction.Norm();
        if (weight.IsValid() && !weight.IsNearZero() && !directionNorm.IsNearZero())
        {
            Weight = weight.ToScalar();
            NormalDirection = direction.Divide(directionNorm).EGaNormal();
        }
        else
        {
            Weight = projectiveSpace.ScalarProcessor.Zero;
            NormalDirection = projectiveSpace.OneScalarBlade.EGaNormal();
        }

        Specs = new XGaProjectiveElementSpecs<T>(
            projectiveSpace,
            XGaProjectiveElementKind.Ideal,
            XGaProjectiveElementEncoding.EGa,
            Direction.Grade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Weight >= 0 &&
               Direction.IsEGaBlade() &&
               Position.IsEGaVector() &&
               Direction.Norm().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirection0D()
    {
        return Direction.Grade == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirection1D()
    {
        return Direction.Grade == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirection2D()
    {
        return Direction.Grade == 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirection3D()
    {
        return Direction.Grade == 3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirection4D()
    {
        return Direction.Grade == 4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOfGrade(int grade)
    {
        return Direction.Grade == grade;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSameElement(XGaProjectiveElement<T> element2, bool ignoreWeight = false)
    {
        if (!ignoreWeight && !Weight.IsNearEqualTo(element2.Weight))
            return false;

        if (!Direction.IsNearEqual(element2.Direction))
            return false;

        if (!SurfaceNearContainsPoint(element2.Position))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> EncodeBlade()
    {
        throw new NotImplementedException();

        //return Weight * NormalDirection.PGaDual().TranslateBy(Position);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveBlade<T> PositionToPGaPoint()
    {
        return Position.EGaVectorToPGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> PositionToVector2D()
    {
        return Position.DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> PositionToVector3D()
    {
        return Position.DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> PositionToVector()
    {
        return Position.DecodeEGaVectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> PositionToXGaVector()
    {
        return Position.DecodeEGaVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> DirectionToVector2D()
    {
        return Direction.DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> DirectionToVector2D(T length)
    {
        return Direction.DecodeEGaVector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DirectionToVector3D()
    {
        return Direction.DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DirectionToVector3D(T length)
    {
        return Direction.DecodeEGaVector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> DirectionToVector()
    {
        return Direction.DecodeEGaVectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> DirectionToXGaVector()
    {
        return Direction.DecodeEGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> DirectionToBivector2D()
    {
        return Direction.DecodeEGaBivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> DirectionToBivector3D()
    {
        return Direction.DecodeEGaBivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> DirectionToXGaBivector()
    {
        return Direction.DecodeEGaBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> DirectionToTrivector3D()
    {
        return Direction.DecodeEGaTrivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> DirectionToXGaKVector()
    {
        return Direction.DecodeEGaKVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> DirectionToVectors2D()
    {
        return Direction.DecodeEGaBladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> DirectionToVectors3D()
    {
        return Direction.DecodeEGaBladeToVectors3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector<T>> DirectionToVectors()
    {
        return Direction.DecodeEGaBladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> DirectionToXGaVectors()
    {
        return Direction.DecodeEGaBladeToVectors();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaProjectiveBlade<T>> DirectionToEGaVectorBlades()
    {
        return Direction.DecodeEGaBladeToVectorEGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> NormalDirectionToVector2D()
    {
        Debug.Assert(ProjectiveSpace.Is4D);

        return NormalDirection.DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> NormalDirectionToVector2D(IScalar<T> length)
    {
        Debug.Assert(ProjectiveSpace.Is4D);

        return NormalDirection.DecodeEGaVector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> NormalDirectionToVector3D()
    {
        return NormalDirection.DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> NormalDirectionToVector3D(IScalar<T> length)
    {
        return NormalDirection.DecodeEGaVector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> NormalDirectionToVector()
    {
        return NormalDirection.DecodeEGaVectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> NormalDirectionToXGaVector()
    {
        return NormalDirection.DecodeEGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> NormalDirectionToBivector2D()
    {
        return NormalDirection.DecodeEGaBivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> NormalDirectionToBivector3D()
    {
        return NormalDirection.DecodeEGaBivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> NormalDirectionToXGaBivector()
    {
        return NormalDirection.DecodeEGaBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> NormalDirectionToTrivector3D()
    {
        return NormalDirection.DecodeEGaTrivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> NormalDirectionToXGaKVector()
    {
        return NormalDirection.DecodeEGaKVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> NormalDirectionToVectors2D()
    {
        return NormalDirection.DecodeEGaBladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> NormalDirectionToVectors3D()
    {
        return NormalDirection.DecodeEGaBladeToVectors3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector<T>> NormalDirectionToVectors()
    {
        return NormalDirection.DecodeEGaBladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> NormalDirectionToXGaVectors()
    {
        return NormalDirection.DecodeEGaBladeToVectors();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaProjectiveBlade<T>> NormalDirectionToEGaVectorBlades()
    {
        return NormalDirection.DecodeEGaBladeToVectorEGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector2D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector3D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(XGaVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector2D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector3D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(XGaVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector2D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector3D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(XGaVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector2D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector3D<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(XGaVector<T> egaVector)
    {
        return ProjectiveSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    
    public LinVector2D<T> SurfacePointToVector2D(LinVector2D<T> egaProbeDirection, Scalar<T> distanceFromPosition, Scalar<T> distanceFromSurface)
    {
        if (IsDirection0D())
            return PositionToVector2D() + 
                   egaProbeDirection.SetLength(distanceFromPosition);

        if (IsDirection1D())
            return PositionToVector2D() + 
                   DirectionToVector2D().SetLength(distanceFromPosition) +
                   NormalDirectionToVector2D(distanceFromSurface);
        
        return PositionToVector2D() + 
               egaProbeDirection.SetLength(distanceFromPosition);
    }
    
    public LinVector3D<T> SurfacePointToVector3D(LinVector3D<T> egaProbeDirection, Scalar<T> distanceFromPosition, Scalar<T> distanceFromSurface)
    {
        if (IsDirection0D())
            return PositionToVector3D() + 
                   egaProbeDirection.SetLength(distanceFromPosition);

        if (IsDirection1D())
            return PositionToVector3D() + 
                   DirectionToVector3D().SetLength(distanceFromPosition) +
                   DirectionToVector3D().GetNormal(distanceFromSurface);

        if (IsPlane)
            return PositionToVector3D() + 
                   egaProbeDirection.ProjectOnBivector(DirectionToBivector3D()).SetLength(distanceFromPosition) +
                   NormalDirectionToVector3D(distanceFromSurface);

        return PositionToVector3D() + 
               egaProbeDirection.SetLength(distanceFromPosition);
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
    public IReadOnlyList<XGaProjectiveBlade<T>> GetSurfacePointEGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(ProjectiveSpace.EncodeEGaVector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaProjectiveBlade<T>> GetSurfacePointPGaBlades()
    {
        return GetSurfacePointVectors()
            .Select(ProjectiveSpace.EncodePGaPoint)
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
    public bool SurfaceNearContainsPoint(XGaProjectiveBlade<T> egaPoint)
    {
        return IsDirectionNearParallelTo(
            (egaPoint - Position).DecodeEGaVector()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (Weight.IsNearZero())
            return "Zero Projective Flat";

        return new StringBuilder()
            .AppendLine("Projective Flat:")
            .AppendLine($"   Weight: ${BasisSpecs.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   Position: ${Position.ToLaTeX()}$")
            .AppendLine($"   Blade: ${EncodeBlade().ToLaTeX()}$")
            .ToString();
    }
}