using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Visualizer;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public abstract class RGaConformalElement :
    IGeometricElement
{
    public RGaConformalElementSpecs Specs { get; }
    
    private double _weight = 1d;
    public double Weight
    {
        get => _weight;
        set => _weight = value.IsValid() && value >= 0 
            ? value 
            : throw new InvalidOperationException();
    }

    public abstract RGaConformalBlade Position { get; }
    
    public RGaConformalBlade Direction { get; }
    
    /// <summary>
    /// The normal direction is the unit orthogonal subspace to the direction of
    /// this element such that their product is the Euclidean pseudo-scalar Ie
    /// (i.e. they form a right-handed coordinate system; a positive determinant)
    /// </summary>
    public RGaConformalBlade NormalDirection
        => Direction.EGaNormal();

    public abstract double RadiusSquared { get; set; }

    public double RealRadius 
        => RadiusSquared.SqrtOfAbs();
    
    public double RealRadiusSquared
        => RadiusSquared.Abs();


    public RGaConformalSpace ConformalSpace 
        => Specs.ConformalSpace;

    public RGaGeometrySpaceBasisSpecs BasisSpecs 
        => Specs.ConformalSpace.BasisSpecs;

    public RGaConformalVisualizer Visualizer 
        => ConformalSpace switch
        {
            RGaConformalSpace4D space => space.Visualizer,
            RGaConformalSpace5D space => space.Visualizer,
            _ => throw new InvalidOperationException()
        };

    public int VSpaceDimensions
        => ConformalSpace.VSpaceDimensions;

    public RGaConformalElementKind Kind 
        => Specs.Kind;
    
    public RGaConformalElementEncoding Encoding 
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
    protected RGaConformalElement(RGaConformalSpace conformalSpace, RGaConformalElementKind kind, double weight, RGaConformalBlade direction)
    {
        Debug.Assert(
            direction.IsEGaBlade()
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
            Direction = conformalSpace.OneScalarBlade;
        }

        Specs = new RGaConformalElementSpecs(
            conformalSpace,
            kind,
            RGaConformalElementEncoding.EGa,
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


    public abstract bool IsSameElement(RGaConformalElement element2, bool ignoreWeight = false);

    public abstract RGaConformalBlade EncodeOpnsBlade();

    public abstract RGaConformalBlade EncodeIpnsBlade();

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade PositionToHGaPoint()
    {
        return Position.EGaVectorToHGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade PositionToPGaPoint()
    {
        return Position.EGaVectorToPGaPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade PositionToOpnsFlatPoint()
    {
        return Position.EGaVectorToOpnsFlatPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalBlade PositionToIpnsPoint()
    {
        return Position.EGaVectorToIpnsPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D PositionToVector2D()
    {
        return Position.DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D PositionToVector3D()
    {
        return Position.DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector PositionToVector()
    {
        return Position.DecodeEGaVectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector PositionToRGaVector()
    {
        return Position.DecodeEGaVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D DirectionToVector2D()
    {
        return Direction.DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D DirectionToVector2D(double length)
    {
        return Direction.DecodeEGaVector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D DirectionToVector3D()
    {
        return Direction.DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D DirectionToVector3D(double length)
    {
        return Direction.DecodeEGaVector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector DirectionToVector()
    {
        return Direction.DecodeEGaVectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector DirectionToRGaVector()
    {
        return Direction.DecodeEGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D DirectionToBivector2D()
    {
        return Direction.DecodeEGaBivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector3D DirectionToBivector3D()
    {
        return Direction.DecodeEGaBivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector DirectionToRGaBivector()
    {
        return Direction.DecodeEGaBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Trivector3D DirectionToTrivector3D()
    {
        return Direction.DecodeEGaTrivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector DirectionToRGaKVector()
    {
        return Direction.DecodeEGaKVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector2D> DirectionToVectors2D()
    {
        return Direction.DecodeEGaBladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector3D> DirectionToVectors3D()
    {
        return Direction.DecodeEGaBladeToVectors3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector> DirectionToVectors()
    {
        return Direction.DecodeEGaBladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> DirectionToRGaVectors()
    {
        return Direction.DecodeEGaBladeToVectors();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> DirectionToEGaVectorBlades()
    {
        return Direction.DecodeEGaBladeToVectorEGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D NormalDirectionToVector2D()
    {
        Debug.Assert(ConformalSpace.Is4D);

        return NormalDirection.DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D NormalDirectionToVector2D(double length)
    {
        Debug.Assert(ConformalSpace.Is4D);

        return NormalDirection.DecodeEGaVector2D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D NormalDirectionToVector3D()
    {
        return NormalDirection.DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D NormalDirectionToVector3D(double length)
    {
        return NormalDirection.DecodeEGaVector3D().SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector NormalDirectionToVector()
    {
        return NormalDirection.DecodeEGaVectorND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector NormalDirectionToRGaVector()
    {
        return NormalDirection.DecodeEGaVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D NormalDirectionToBivector2D()
    {
        return NormalDirection.DecodeEGaBivector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector3D NormalDirectionToBivector3D()
    {
        return NormalDirection.DecodeEGaBivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector NormalDirectionToRGaBivector()
    {
        return NormalDirection.DecodeEGaBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Trivector3D NormalDirectionToTrivector3D()
    {
        return NormalDirection.DecodeEGaTrivector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector NormalDirectionToRGaKVector()
    {
        return NormalDirection.DecodeEGaKVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector2D> NormalDirectionToVectors2D()
    {
        return NormalDirection.DecodeEGaBladeToVectors2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector3D> NormalDirectionToVectors3D()
    {
        return NormalDirection.DecodeEGaBladeToVectors3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64Vector> NormalDirectionToVectors()
    {
        return NormalDirection.DecodeEGaBladeToVectorsND();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> NormalDirectionToRGaVectors()
    {
        return NormalDirection.DecodeEGaBladeToVectors();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaConformalBlade> NormalDirectionToEGaVectorBlades()
    {
        return NormalDirection.DecodeEGaBladeToVectorEGaBlades();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(Float64Vector2D egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(Float64Vector3D egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(Float64Vector egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionParallelTo(RGaFloat64Vector egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsZero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(Float64Vector2D egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(Float64Vector3D egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(Float64Vector egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionOrthogonalTo(RGaFloat64Vector egaVector)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsZero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(Float64Vector2D egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(Float64Vector3D egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(Float64Vector egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearParallelTo(RGaFloat64Vector egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Op(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(Float64Vector2D egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(Float64Vector3D egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(Float64Vector egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDirectionNearOrthogonalTo(RGaFloat64Vector egaVector, double epsilon = 1e-12)
    {
        return ConformalSpace
            .EncodeEGaVectorAsVector(egaVector)
            .Lcp(Direction.InternalKVector)
            .IsNearZero(epsilon);
    }

    
    public Float64Vector2D SurfacePointToVector2D(Float64Vector2D egaProbeDirection, double distanceFromPosition, double distanceFromSurface)
    {
        if (IsDirection0D())
            return PositionToVector2D() + 
                   egaProbeDirection.SetLength(distanceFromPosition);

        if (IsDirection1D())
            return PositionToVector2D() + 
                   DirectionToVector2D().SetLength(distanceFromPosition) +
                   NormalDirectionToVector2D(distanceFromSurface);
        
        if (this is RGaConformalRound round)
            return round.RoundSurfacePointToVector2D(
                egaProbeDirection, 
                distanceFromSurface
            );

        return PositionToVector2D() + 
               egaProbeDirection.SetLength(distanceFromPosition);
    }
    
    public Float64Vector3D SurfacePointToVector3D(Float64Vector3D egaProbeDirection, double distanceFromPosition, double distanceFromSurface)
    {
        if (IsDirection0D())
            return PositionToVector3D() + 
                   egaProbeDirection.SetLength(distanceFromPosition);

        if (IsDirection1D())
            return PositionToVector3D() + 
                   DirectionToVector3D().SetLength(distanceFromPosition) +
                   DirectionToVector3D().GetNormal(distanceFromSurface);

        if (this is RGaConformalRound round)
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
    public RGaConformalDirection ToDirection()
    {
        if (this is RGaConformalDirection direction)
            return direction;

        return new RGaConformalDirection(
            ConformalSpace,
            Weight,
            Direction
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalFlat ToFlat()
    {
        if (this is RGaConformalFlat flat)
            return flat;

        return new RGaConformalFlat(
            ConformalSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalFlat ToFlat(Float64Vector2D egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalFlat ToFlat(Float64Vector3D egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalFlat ToFlat(Float64Vector egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalFlat ToFlat(RGaFloat64Vector egaPosition)
    {
        return ToFlat(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalFlat ToFlat(RGaConformalBlade egaPosition)
    {
        return new RGaConformalFlat(
            ConformalSpace,
            Weight,
            egaPosition,
            Direction
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalTangent ToTangent()
    {
        if (this is RGaConformalTangent tangent)
            return tangent;

        return new RGaConformalTangent(
            ConformalSpace,
            Weight,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalTangent ToTangent(Float64Vector2D egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalTangent ToTangent(Float64Vector3D egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalTangent ToTangent(Float64Vector egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalTangent ToTangent(RGaFloat64Vector egaPosition)
    {
        return ToTangent(
            ConformalSpace.EncodeEGaVector(egaPosition)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalTangent ToTangent(RGaConformalBlade egaPosition)
    {
        return new RGaConformalTangent(
            ConformalSpace,
            Weight,
            egaPosition,
            Direction
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRound(double radiusSquared)
    {
        if (this is RGaConformalRound round && round.RadiusSquared == radiusSquared)
            return round;

        return new RGaConformalRound(
            ConformalSpace,
            Weight,
            radiusSquared,
            Position,
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRound(Float64Vector2D egaPosition, double radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRound(Float64Vector3D egaPosition, double radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRound(Float64Vector egaPosition, double radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRound(RGaFloat64Vector egaPosition, double radiusSquared)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRound(RGaConformalBlade egaPosition, double radiusSquared)
    {
        return new RGaConformalRound(
            ConformalSpace,
            Weight,
            radiusSquared,
            egaPosition,
            Direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRealRound(Float64Vector2D egaPosition, double radius)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRealRound(Float64Vector3D egaPosition, double radius)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            radius * radius
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRealRound(Float64Vector egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRealRound(RGaFloat64Vector egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToRealRound(RGaConformalBlade egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            radius * radius
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToImaginaryRound(Float64Vector3D egaPosition, double radius)
    {
        return ToRound(
            ConformalSpace.EncodeEGaVector(egaPosition),
            -radius * radius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalRound ToImaginaryRound(RGaFloat64Vector egaPosition, double radius)
    {
        return ToRound(
            egaPosition,
            -radius * radius
        );
    }
}