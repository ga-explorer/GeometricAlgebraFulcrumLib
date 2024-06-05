using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public abstract class XGaConformalElement<T> :
    IAlgebraicElement
{
    public XGaConformalElementSpecs<T> Specs { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => Specs.ConformalSpace.ScalarProcessor;

    public Scalar<T> Weight { get; set; }

    public abstract XGaConformalBlade<T> Position { get; }
    
    public XGaConformalBlade<T> Direction { get; }
    
    /// <summary>
    /// The normal direction is the unit orthogonal subspace to the direction of
    /// this element such that their product is the Euclidean pseudo-scalar Ie
    /// (i.e. they form a right-handed coordinate system; a positive determinant)
    /// </summary>
    public XGaConformalBlade<T> NormalDirection
        => Direction.EGaNormal();

    public abstract Scalar<T> RadiusSquared { get; set; }

    public Scalar<T> RealRadius 
        => RadiusSquared.SqrtOfAbs();
    
    public Scalar<T> RealRadiusSquared
        => RadiusSquared.Abs();


    public XGaConformalSpace<T> ConformalSpace 
        => Specs.ConformalSpace;
    
    public XGaConformalSpace4D<T> ConformalSpace4D 
        => Specs.ConformalSpace4D;
    
    public XGaConformalSpace5D<T> ConformalSpace5D 
        => Specs.ConformalSpace5D;

    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs 
        => Specs.ConformalSpace.BasisSpecs;

    public int VSpaceDimensions
        => ConformalSpace.VSpaceDimensions;

    public XGaConformalElementKind Kind 
        => Specs.Kind;
    
    public XGaConformalElementEncoding Encoding 
        => Specs.Encoding;


    public bool IsDirection 
        => Specs.IsDirection;
    
    public bool IsTangent 
        => Specs.IsTangent;
    
    public bool IsFlat
        => Specs.IsFlat;

    public bool IsRound 
        => Specs.IsRound;

    public bool IsPoint 
        => Direction.Grade == 0;
    
    public bool IsLine 
        => !IsRound && Direction.Grade == 1;
    
    public bool IsPlane 
        => !IsRound && Direction.Grade == 2;
    
    public bool IsVolume 
        => !IsRound && Direction.Grade == 3;
    
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

    public bool IsTangentPoint 
        => IsTangent && Direction.Grade == 0;
    
    public bool IsTangentLine 
        => IsTangent && Direction.Grade == 1;
    
    public bool IsTangentPlane 
        => IsTangent && Direction.Grade == 2;
    
    public bool IsTangentVolume 
        => IsTangent && Direction.Grade == 3;
    
    public bool IsTangentHyperPlane 
        => IsTangent && Direction.Grade == VSpaceDimensions - 2;

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

    public bool IsRoundPoint 
        => IsRound && Direction.Grade == 0;

    public bool IsRoundPointPair 
        => IsRound && Direction.Grade == 1;
    
    public bool IsRoundCircle 
        => IsRound && Direction.Grade == 2;

    public bool IsRoundSphere 
        => IsRound && Direction.Grade == 3;
    
    public bool IsRoundHyperSphere 
        => IsRound && Direction.Grade == VSpaceDimensions - 2;
    
    public bool IsRealRoundPointPair 
        => IsRound && Direction.Grade == 1 && RadiusSquared > 0;
    
    public bool IsRealRoundCircle 
        => IsRound && Direction.Grade == 2 && RadiusSquared > 0;

    public bool IsRealRoundSphere 
        => IsRound && Direction.Grade == 3 && RadiusSquared > 0;
    
    public bool IsRealRoundHyperSphere 
        => IsRound && Direction.Grade == VSpaceDimensions - 2 && RadiusSquared > 0;
    
    public bool IsImaginaryRoundPointPair 
        => IsRound && Direction.Grade == 1 && RadiusSquared < 0;
    
    public bool IsImaginaryRoundCircle 
        => IsRound && Direction.Grade == 2 && RadiusSquared < 0;

    public bool IsImaginaryRoundSphere 
        => IsRound && Direction.Grade == 3 && RadiusSquared < 0;
    
    public bool IsImaginaryRoundHyperSphere 
        => IsRound && Direction.Grade == VSpaceDimensions - 2 && RadiusSquared < 0;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaConformalElement(XGaConformalSpace<T> conformalSpace, XGaConformalElementKind kind, IScalar<T> weight, XGaConformalBlade<T> direction)
    {
        Debug.Assert(
            direction.IsEGaBlade()
        );

        var directionNorm = direction.Norm();
        if (weight.IsValid() && !weight.IsNearZero() && !directionNorm.IsNearZero())
        {
            Weight = weight.ToScalar();
            Direction = direction.Divide(directionNorm);
        }
        else
        {
            Weight = conformalSpace.ScalarProcessor.Zero;
            Direction = conformalSpace.OneScalarBlade;
        }

        Specs = new XGaConformalElementSpecs<T>(
            conformalSpace,
            kind,
            XGaConformalElementEncoding.EGa,
            Direction.Grade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract bool IsValid();

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


    public abstract bool IsSameElement(XGaConformalElement<T> element2, bool ignoreWeight = false);

    public abstract XGaConformalBlade<T> EncodeOpnsBlade();

    public abstract XGaConformalBlade<T> EncodeIpnsBlade();

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> PositionToHGaPoint()
    {
        return Position.EGaVectorToHGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> PositionToPGaPoint()
    {
        return Position.EGaVectorToPGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> PositionToOpnsFlatPoint()
    {
        return Position.EGaVectorToOpnsFlatPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalBlade<T> PositionToIpnsPoint()
    {
        return Position.EGaVectorToIpnsPoint();
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
    public IReadOnlyList<XGaConformalBlade<T>> DirectionToEGaVectorBlades()
    {
        return Direction.DecodeEGaBladeToVectorEGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> NormalDirectionToVector2D()
    {
        Debug.Assert(ConformalSpace.Is4D);

        return NormalDirection.DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> NormalDirectionToVector2D(IScalar<T> length)
    {
        Debug.Assert(ConformalSpace.Is4D);

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
    public IReadOnlyList<XGaConformalBlade<T>> NormalDirectionToEGaVectorBlades()
    {
        return NormalDirection.DecodeEGaBladeToVectorEGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector2D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector3D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(XGaVector<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector2D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector3D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(XGaVector<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector2D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector3D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(XGaVector<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector2D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector3D<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector<T> egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(XGaVector<T> egaVector)
    {
        return ConformalSpace
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
        
        if (this is XGaConformalRound<T> round)
            return round.RoundSurfacePointToVector2D(
                egaProbeDirection, 
                distanceFromSurface
            );

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

        if (this is XGaConformalRound<T> round)
            return round.RoundSurfacePointToVector3D(
                egaProbeDirection, 
                distanceFromSurface
            );
        
        if (IsPlane)
            return PositionToVector3D() + 
                   egaProbeDirection.ProjectOnBivector(DirectionToBivector3D()).SetLength(distanceFromPosition) +
                   NormalDirectionToVector3D(distanceFromSurface);

        return PositionToVector3D() + 
               egaProbeDirection.SetLength(distanceFromPosition);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalDirection<T> ToDirection()
    {
        if (this is XGaConformalDirection<T> direction)
            return direction;

        return new XGaConformalDirection<T>(
            ConformalSpace,
            Weight,
            Direction
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalFlat<T> ToFlat()
    {
        if (this is XGaConformalFlat<T> flat)
            return flat;

        return new XGaConformalFlat<T>(
            ConformalSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalFlat<T> ToFlat(LinVector2D<T> egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalFlat<T> ToFlat(LinVector3D<T> egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalFlat<T> ToFlat(LinVector<T> egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalFlat<T> ToFlat(XGaVector<T> egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalFlat<T> ToFlat(XGaConformalBlade<T> egaPosition)
    {
        return new XGaConformalFlat<T>(
            ConformalSpace,
            Weight,
            egaPosition,
            Direction
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalTangent<T> ToTangent()
    {
        if (this is XGaConformalTangent<T> tangent)
            return tangent;

        return new XGaConformalTangent<T>(
            ConformalSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalTangent<T> ToTangent(LinVector2D<T> egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalTangent<T> ToTangent(LinVector3D<T> egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalTangent<T> ToTangent(LinVector<T> egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalTangent<T> ToTangent(XGaVector<T> egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalTangent<T> ToTangent(XGaConformalBlade<T> egaPosition)
    {
        return new XGaConformalTangent<T>(
            ConformalSpace,
            Weight,
            egaPosition,
            Direction
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRound(Scalar<T> radiusSquared)
    {
        if (this is XGaConformalRound<T> round && round.RadiusSquared == radiusSquared)
            return round;

        return new XGaConformalRound<T>(
            ConformalSpace,
            Weight,
            radiusSquared,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRound(LinVector2D<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRound(LinVector3D<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRound(LinVector<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRound(XGaVector<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRound(XGaConformalBlade<T> egaPosition, Scalar<T> radiusSquared)
    {
        return new XGaConformalRound<T>(
            ConformalSpace,
            Weight,
            radiusSquared,
            egaPosition,
            Direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRealRound(LinVector2D<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRealRound(LinVector3D<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radius * radius
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRealRound(LinVector<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRealRound(XGaVector<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToRealRound(XGaConformalBlade<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToImaginaryRound(LinVector3D<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radius.Square().Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalRound<T> ToImaginaryRound(XGaVector<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius.Square().Negative()
        );
    }
}