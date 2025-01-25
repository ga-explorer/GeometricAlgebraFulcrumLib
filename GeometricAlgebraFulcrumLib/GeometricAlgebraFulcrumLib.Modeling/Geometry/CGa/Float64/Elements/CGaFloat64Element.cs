using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public abstract class CGaFloat64Element :
    IAlgebraicElement
{
    public CGaFloat64ElementSpecs Specs { get; }

    private double _weight = 1d;
    public double Weight
    {
        get => _weight;
        set => _weight = value.IsValid() && value >= 0
            ? value
            : throw new InvalidOperationException();
    }

    public abstract CGaFloat64Blade Position { get; }

    public CGaFloat64Blade Direction { get; }

    /// <summary>
    /// The normal direction is the unit orthogonal subspace to the direction of
    /// this element such that their product is the Euclidean pseudo-scalar Ie
    /// (i.e. they form a right-handed coordinate system; a positive determinant)
    /// </summary>
    public CGaFloat64Blade NormalDirection
        => Direction.VGaNormal();

    public abstract double RadiusSquared { get; set; }

    public double RealRadius
        => RadiusSquared.SqrtOfAbs();

    public double RealRadiusSquared
        => RadiusSquared.Abs();


    public CGaFloat64GeometricSpace GeometricSpace
        => Specs.GeometricSpace;

    public GaFloat64GeometricSpaceBasisSpecs BasisSpecs
        => Specs.GeometricSpace.BasisSpecs;

    public CGaFloat64Visualizer Visualizer
        => GeometricSpace switch
        {
            CGaFloat64GeometricSpace4D space => space.Visualizer,
            CGaFloat64GeometricSpace5D space => space.Visualizer,
            _ => throw new InvalidOperationException()
        };

    public int VSpaceDimensions
        => GeometricSpace.VSpaceDimensions;

    public CGaFloat64ElementKind Kind
        => Specs.Kind;

    public CGaFloat64ElementEncoding Encoding
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
    protected CGaFloat64Element(CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64ElementKind kind, double weight, CGaFloat64Blade direction)
    {
        Debug.Assert(
            direction.IsVGaBlade()
        );

        var directionNorm = direction.Norm();
        if (weight.IsValid() && !weight.IsNearZero() && !directionNorm.IsNearZero())
        {
            Weight = weight;
            Direction = direction.Divide(directionNorm);
        }
        else
        {
            Weight = 0d;
            Direction = cgaGeometricSpace.OneScalarBlade;
        }

        Specs = new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            kind,
            CGaFloat64ElementEncoding.VGa,
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


    public abstract bool IsSameElement(CGaFloat64Element element2, bool ignoreWeight = false);

    public abstract CGaFloat64Blade EncodeOpnsBlade();

    public abstract CGaFloat64Blade EncodeIpnsBlade();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PositionToHGaPoint()
    {
        return Position.VGaVectorToHGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PositionToPGaPoint()
    {
        return Position.VGaVectorToPGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PositionToOpnsFlatPoint()
    {
        return Position.VGaVectorToOpnsFlatPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PositionToIpnsPoint()
    {
        return Position.VGaVectorToIpnsPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D PositionToVector2D()
    {
        return Position.Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D PositionToVector3D()
    {
        return Position.Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector PositionToVector()
    {
        return Position.Decode.VGaDirection.Vector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector PositionToRGaVector()
    {
        return Position.Decode.VGaDirection.RGaVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D DirectionToVector2D()
    {
        return Direction.Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D DirectionToVector2D(double length)
    {
        return Direction.Decode.VGaDirection.Vector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D DirectionToVector3D()
    {
        return Direction.Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D DirectionToVector3D(double length)
    {
        return Direction.Decode.VGaDirection.Vector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector DirectionToVector()
    {
        return Direction.Decode.VGaDirection.Vector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector DirectionToRGaVector()
    {
        return Direction.Decode.VGaDirection.RGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D DirectionToBivector2D()
    {
        return Direction.Decode.VGaDirection.Bivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D DirectionToBivector3D()
    {
        return Direction.Decode.VGaDirection.Bivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector DirectionToRGaBivector()
    {
        return Direction.Decode.VGaDirection.Bivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D DirectionToTrivector3D()
    {
        return Direction.Decode.VGaDirection.Trivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector DirectionToRGaKVector()
    {
        return Direction.Decode.VGaDirection.KVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector2D> DirectionToVectors2D()
    {
        return Direction.Decode.VGaDirection.BladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector3D> DirectionToVectors3D()
    {
        return Direction.Decode.VGaDirection.BladeToVectors3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector> DirectionToVectors()
    {
        return Direction.Decode.VGaDirection.BladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> DirectionToRGaVectors()
    {
        return Direction.Decode.VGaDirection.BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> DirectionToVGaVectorBlades()
    {
        return Direction.Decode.VGaDirection.BladeToVectorVGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D NormalDirectionToVector2D()
    {
        Debug.Assert(GeometricSpace.Is4D);

        return NormalDirection.Decode.VGaDirection.Vector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D NormalDirectionToVector2D(double length)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return NormalDirection.Decode.VGaDirection.Vector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D NormalDirectionToVector3D()
    {
        return NormalDirection.Decode.VGaDirection.Vector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D NormalDirectionToVector3D(double length)
    {
        return NormalDirection.Decode.VGaDirection.Vector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector NormalDirectionToVector()
    {
        return NormalDirection.Decode.VGaDirection.Vector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector NormalDirectionToRGaVector()
    {
        return NormalDirection.Decode.VGaDirection.RGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D NormalDirectionToBivector2D()
    {
        return NormalDirection.Decode.VGaDirection.Bivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D NormalDirectionToBivector3D()
    {
        return NormalDirection.Decode.VGaDirection.Bivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector NormalDirectionToRGaBivector()
    {
        return NormalDirection.Decode.VGaDirection.Bivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D NormalDirectionToTrivector3D()
    {
        return NormalDirection.Decode.VGaDirection.Trivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector NormalDirectionToRGaKVector()
    {
        return NormalDirection.Decode.VGaDirection.KVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector2D> NormalDirectionToVectors2D()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector3D> NormalDirectionToVectors3D()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectors3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector> NormalDirectionToVectors()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> NormalDirectionToRGaVectors()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<CGaFloat64Blade> NormalDirectionToVGaVectorBlades()
    {
        return NormalDirection.Decode.VGaDirection.BladeToVectorVGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinFloat64Vector2D egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinFloat64Vector3D egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(LinFloat64Vector egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(RGaFloat64Vector egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinFloat64Vector2D egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinFloat64Vector3D egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(LinFloat64Vector egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(RGaFloat64Vector egaVector)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinFloat64Vector2D egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinFloat64Vector3D egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(LinFloat64Vector egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(RGaFloat64Vector egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinFloat64Vector2D egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinFloat64Vector3D egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(LinFloat64Vector egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(RGaFloat64Vector egaVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GeometricSpace
            .Encode.VGa.VectorAsRGaVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(zeroEpsilon);
    }


    public LinFloat64Vector2D SurfacePointToVector2D(LinFloat64Vector2D egaProbeDirection, double distanceFromPosition, double distanceFromSurface)
    {
        if (IsDirection0D())
            return PositionToVector2D() +
                   egaProbeDirection.SetLength(distanceFromPosition);

        if (IsDirection1D())
            return PositionToVector2D() +
                   DirectionToVector2D().SetLength(distanceFromPosition) +
                   NormalDirectionToVector2D(distanceFromSurface);

        if (this is CGaFloat64Round round)
            return round.RoundSurfacePointToVector2D(
                egaProbeDirection,
                distanceFromSurface
            );

        return PositionToVector2D() +
               egaProbeDirection.SetLength(distanceFromPosition);
    }

    public LinFloat64Vector3D SurfacePointToVector3D(LinFloat64Vector3D egaProbeDirection, double distanceFromPosition, double distanceFromSurface)
    {
        if (IsDirection0D())
            return PositionToVector3D() +
                   egaProbeDirection.SetLength(distanceFromPosition);

        if (IsDirection1D())
            return PositionToVector3D() +
                   DirectionToVector3D().SetLength(distanceFromPosition) +
                   DirectionToVector3D().GetNormal(distanceFromSurface);

        if (this is CGaFloat64Round round)
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
    public CGaFloat64Direction ToDirection()
    {
        if (this is CGaFloat64Direction direction)
            return direction;

        return new CGaFloat64Direction(
            GeometricSpace,
            Weight,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat ToFlat()
    {
        if (this is CGaFloat64Flat flat)
            return flat;

        return new CGaFloat64Flat(
            GeometricSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat ToFlat(LinFloat64Vector2D egaPosition)
    {
        return ToFlat(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat ToFlat(LinFloat64Vector3D egaPosition)
    {
        return ToFlat(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat ToFlat(LinFloat64Vector egaPosition)
    {
        return ToFlat(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat ToFlat(RGaFloat64Vector egaPosition)
    {
        return ToFlat(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat ToFlat(CGaFloat64Blade egaPosition)
    {
        return new CGaFloat64Flat(
            GeometricSpace,
            Weight,
            egaPosition,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent ToTangent()
    {
        if (this is CGaFloat64Tangent tangent)
            return tangent;

        return new CGaFloat64Tangent(
            GeometricSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent ToTangent(LinFloat64Vector2D egaPosition)
    {
        return ToTangent(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent ToTangent(LinFloat64Vector3D egaPosition)
    {
        return ToTangent(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent ToTangent(LinFloat64Vector egaPosition)
    {
        return ToTangent(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent ToTangent(RGaFloat64Vector egaPosition)
    {
        return ToTangent(
            GeometricSpace.Encode.VGa.Vector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent ToTangent(CGaFloat64Blade egaPosition)
    {
        return new CGaFloat64Tangent(
            GeometricSpace,
            Weight,
            egaPosition,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRound(double radiusSquared)
    {
        if (this is CGaFloat64Round round && round.RadiusSquared == radiusSquared)
            return round;

        return new CGaFloat64Round(
            GeometricSpace,
            Weight,
            radiusSquared,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRound(LinFloat64Vector2D egaPosition, double radiusSquared)
    {
        return ToRound(
            GeometricSpace.Encode.VGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRound(LinFloat64Vector3D egaPosition, double radiusSquared)
    {
        return ToRound(
            GeometricSpace.Encode.VGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRound(LinFloat64Vector egaPosition, double radiusSquared)
    {
        return ToRound(
            GeometricSpace.Encode.VGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRound(RGaFloat64Vector egaPosition, double radiusSquared)
    {
        return ToRound(
            GeometricSpace.Encode.VGa.Vector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRound(CGaFloat64Blade egaPosition, double radiusSquared)
    {
        return new CGaFloat64Round(
            GeometricSpace,
            Weight,
            radiusSquared,
            egaPosition,
            Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRealRound(LinFloat64Vector2D egaPosition, double radius)
    {
        return ToRound(
            GeometricSpace.Encode.VGa.Vector(egaPosition),
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRealRound(LinFloat64Vector3D egaPosition, double radius)
    {
        return ToRound(
            GeometricSpace.Encode.VGa.Vector(egaPosition),
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRealRound(LinFloat64Vector egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRealRound(RGaFloat64Vector egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToRealRound(CGaFloat64Blade egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToImaginaryRound(LinFloat64Vector3D egaPosition, double radius)
    {
        return ToRound(
            GeometricSpace.Encode.VGa.Vector(egaPosition),
            -radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ToImaginaryRound(RGaFloat64Vector egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            -radius * radius
        );
    }
}