using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public abstract class CGaElement<T> :
    IAlgebraicElement
{
    public CGaElementSpecs<T> Specs { get; }

    public IScalarProcessor<T> ScalarProcessor
        => Specs.GeometricSpace.ScalarProcessor;

    public Scalar<T> Weight { get; set; }

    public abstract CGaBlade<T> Position { get; }

    public CGaBlade<T> Direction { get; }

    /// <summary>
    /// The normal direction is the unit orthogonal subspace to the direction of
    /// this element such that their product is the Euclidean pseudo-scalar Ie
    /// (i.e. they form a right-handed coordinate system; a positive determinant)
    /// </summary>
    public CGaBlade<T> NormalDirection
        => Direction.VGaNormal();

    public abstract Scalar<T> RadiusSquared { get; set; }

    public Scalar<T> RealRadius
        => RadiusSquared.SqrtOfAbs();

    public Scalar<T> RealRadiusSquared
        => RadiusSquared.Abs();


    public CGaGeometricSpace<T> GeometricSpace
        => Specs.GeometricSpace;

    public CGaGeometricSpace4D<T> GeometricSpace4D
        => Specs.GeometricSpace4D;

    public CGaGeometricSpace5D<T> GeometricSpace5D
        => Specs.GeometricSpace5D;

    public GaGeometricSpaceBasisSpecs<T> BasisSpecs
        => Specs.GeometricSpace.BasisSpecs;

    public int VSpaceDimensions
        => GeometricSpace.VSpaceDimensions;

    public CGaElementKind Kind
        => Specs.Kind;

    public CGaElementEncoding Encoding
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
    protected CGaElement(CGaGeometricSpace<T> cgaGeometricSpace, CGaElementKind kind, IScalar<T> weight, CGaBlade<T> direction)
    {
        Debug.Assert(
            direction.IsVGaBlade()
        );

        var directionNorm = direction.Norm();
        if (weight.IsValid() && !weight.IsNearZero() && !directionNorm.IsNearZero())
        {
            Weight = weight.ToScalar();
            Direction = direction.Divide(directionNorm);
        }
        else
        {
            Weight = cgaGeometricSpace.ScalarProcessor.Zero;
            Direction = cgaGeometricSpace.OneScalarBlade;
        }

        Specs = new CGaElementSpecs<T>(
            cgaGeometricSpace,
            kind,
            CGaElementEncoding.VGa,
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


    public abstract bool IsSameElement(CGaElement<T> element2, bool ignoreWeight = false);

    public abstract CGaBlade<T> EncodeOpnsBlade();

    public abstract CGaBlade<T> EncodeIpnsBlade();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PositionToHGaPoint()
    {
        return Position.VGaVectorToHGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PositionToPGaPoint()
    {
        return Position.VGaVectorToPGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PositionToOpnsFlatPoint()
    {
        return Position.VGaVectorToOpnsFlatPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PositionToIpnsPoint()
    {
        return Position.VGaVectorToIpnsPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> PositionToVector2D()
    {
        return Position.Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> PositionToVector3D()
    {
        return Position.Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> PositionToVector()
    {
        return Position.Decode.VGaDirection.VectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> PositionToXGaVector()
    {
        return Position.Decode.VGaDirection.Vector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> DirectionToVector2D()
    {
        return Direction.Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> DirectionToVector2D(T length)
    {
        return Direction.Decode.VGaDirection.Vector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DirectionToVector3D()
    {
        return Direction.Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DirectionToVector3D(T length)
    {
        return Direction.Decode.VGaDirection.Vector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> DirectionToVector()
    {
        return Direction.Decode.VGaDirection.VectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> DirectionToXGaVector()
    {
        return Direction.Decode.VGaDirection.Vector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> DirectionToBivector2D()
    {
        return Direction.Decode.VGaDirection.Bivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> DirectionToBivector3D()
    {
        return Direction.Decode.VGaDirection.Bivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> DirectionToXGaBivector()
    {
        return Direction.Decode.VGaDirection.Bivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> DirectionToTrivector3D()
    {
        return Direction.Decode.VGaDirection.Trivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> DirectionToXGaKVector()
    {
        return Direction.Decode.VGaDirection.KVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> DirectionToVectors2D()
    {
        return Direction.Decode.VGaDirection.BladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> DirectionToVectors3D()
    {
        return Direction.Decode.VGaDirection.BladeToVectors3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector<T>> DirectionToVectors()
    {
        return Direction.Decode.VGaDirection.BladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> DirectionToXGaVectors()
    {
        return Direction.Decode.VGaDirection.BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> DirectionToVGaVectorBlades()
    {
        return Direction.Decode.VGaDirection.BladeToVectorVGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> NormalDirectionToVector2D()
    {
        Debug.Assert(GeometricSpace.Is4D);

        return NormalDirection.Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> NormalDirectionToVector2D(IScalar<T> length)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return NormalDirection.Decode.VGaDirection.Vector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> NormalDirectionToVector3D()
    {
        return NormalDirection.Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> NormalDirectionToVector3D(IScalar<T> length)
    {
        return NormalDirection.Decode.VGaDirection.Vector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> NormalDirectionToVector()
    {
        return NormalDirection.Decode.VGaDirection.VectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> NormalDirectionToXGaVector()
    {
        return NormalDirection.Decode.VGaDirection.Vector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> NormalDirectionToBivector2D()
    {
        return NormalDirection.Decode.VGaDirection.Bivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> NormalDirectionToBivector3D()
    {
        return NormalDirection.Decode.VGaDirection.Bivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> NormalDirectionToXGaBivector()
    {
        return NormalDirection.Decode.VGaDirection.Bivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> NormalDirectionToTrivector3D()
    {
        return NormalDirection.Decode.VGaDirection.Trivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> NormalDirectionToXGaKVector()
    {
        return NormalDirection.Decode.VGaDirection.KVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> NormalDirectionToVectors2D()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> NormalDirectionToVectors3D()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectors3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector<T>> NormalDirectionToVectors()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaVector<T>> NormalDirectionToXGaVectors()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaBlade<T>> NormalDirectionToVGaVectorBlades()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectorVGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector2D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector3D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(XGaVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector2D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector3D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(XGaVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector2D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector3D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(XGaVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector2D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector3D<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(XGaVector<T> egaVector)
    {
        return GeometricSpace
            .EncodeVGa.VectorAsXGaVector(egaVector)
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

        if (this is CGaRound<T> round)
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

        if (this is CGaRound<T> round)
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
    public CGaDirection<T> ToDirection()
    {
        if (this is CGaDirection<T> direction)
            return direction;

        return new CGaDirection<T>(
            GeometricSpace,
            Weight,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> ToFlat()
    {
        if (this is CGaFlat<T> flat)
            return flat;

        return new CGaFlat<T>(
            GeometricSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> ToFlat(LinVector2D<T> egaPosition)
    {
        return ToFlat(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> ToFlat(LinVector3D<T> egaPosition)
    {
        return ToFlat(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> ToFlat(LinVector<T> egaPosition)
    {
        return ToFlat(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> ToFlat(XGaVector<T> egaPosition)
    {
        return ToFlat(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> ToFlat(CGaBlade<T> egaPosition)
    {
        return new CGaFlat<T>(
            GeometricSpace,
            Weight,
            egaPosition,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> ToTangent()
    {
        if (this is CGaTangent<T> tangent)
            return tangent;

        return new CGaTangent<T>(
            GeometricSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> ToTangent(LinVector2D<T> egaPosition)
    {
        return ToTangent(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> ToTangent(LinVector3D<T> egaPosition)
    {
        return ToTangent(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> ToTangent(LinVector<T> egaPosition)
    {
        return ToTangent(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> ToTangent(XGaVector<T> egaPosition)
    {
        return ToTangent(
            GeometricSpace.EncodeVGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> ToTangent(CGaBlade<T> egaPosition)
    {
        return new CGaTangent<T>(
            GeometricSpace,
            Weight,
            egaPosition,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRound(Scalar<T> radiusSquared)
    {
        if (this is CGaRound<T> round && round.RadiusSquared == radiusSquared)
            return round;

        return new CGaRound<T>(
            GeometricSpace,
            Weight,
            radiusSquared,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRound(LinVector2D<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            GeometricSpace.EncodeVGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRound(LinVector3D<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            GeometricSpace.EncodeVGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRound(LinVector<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            GeometricSpace.EncodeVGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRound(XGaVector<T> egaPosition, Scalar<T> radiusSquared)
    {
        return ToRound(
            GeometricSpace.EncodeVGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRound(CGaBlade<T> egaPosition, Scalar<T> radiusSquared)
    {
        return new CGaRound<T>(
            GeometricSpace,
            Weight,
            radiusSquared,
            egaPosition,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRealRound(LinVector2D<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            GeometricSpace.EncodeVGa.Vector(egaPosition),
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRealRound(LinVector3D<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            GeometricSpace.EncodeVGa.Vector(egaPosition),
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRealRound(LinVector<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRealRound(XGaVector<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToRealRound(CGaBlade<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToImaginaryRound(LinVector3D<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            GeometricSpace.EncodeVGa.Vector(egaPosition),
            radius.Square().Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaRound<T> ToImaginaryRound(XGaVector<T> egaPosition, Scalar<T> radius)
    {
        return ToRound(
            egaPosition,
            radius.Square().Negative()
        );
    }
}