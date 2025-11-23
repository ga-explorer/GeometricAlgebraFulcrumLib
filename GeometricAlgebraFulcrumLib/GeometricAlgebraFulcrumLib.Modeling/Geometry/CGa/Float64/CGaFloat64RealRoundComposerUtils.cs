using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double radius, double[] position, double[] direction)
    {
        return RealRoundPointPair(
            radius,
            position.ToLinVector(),
            direction.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double radius, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double weight, double radius, double[] position, double[] direction)
    {
        return RealRoundPointPair(
            weight, 
            radius,
            position.ToLinVector(),
            direction.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double weight, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double weight, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double weight, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPair(double weight, double radius, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(double[] point1, double[] point2)
    {
        return RealRoundPointPairFromPoints(
            point1.ToLinVector(),
            point2.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(double weight, double[] point1, double[] point2)
    {
        return RealRoundPointPairFromPoints(
            weight, 
            point1.ToLinVector(),
            point2.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundPointPairFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle2D(double radius, double positionX, double positionY)
    {
        return RealRoundCircle(
            radius,
            LinFloat64Vector2D.Create(positionX, positionY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle2D(double radius, double[] position)
    {
        return RealRoundCircle(
            radius,
            position.ToLinVector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double radius, LinFloat64Vector2D position)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(LinFloat64Bivector2D.E12)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double radius, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle2D(double weight, double radius, double positionX, double positionY)
    {
        return RealRoundCircle(
            weight,
            radius,
            LinFloat64Vector2D.Create(positionX, positionY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle2D(double weight, double radius, double[] position)
    {
        return RealRoundCircle(
            weight,
            radius,
            position.ToLinVector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double weight, double radius, LinFloat64Vector2D position)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(LinFloat64Bivector2D.E12)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double weight, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double weight, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircle(double weight, double radius, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(double[] point1, double[] point2, double[] point3)
    {
        return RealRoundCircleFromPoints(
            point1.ToLinVector(),
            point2.ToLinVector(),
            point3.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1.ToXGaFloat64Vector(),
            point2.ToXGaFloat64Vector(),
            point3.ToXGaFloat64Vector()
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(double weight, double[] point1, double[] point2, double[] point3)
    {
        return RealRoundCircleFromPoints(
            weight, 
            point1.ToLinVector(),
            point2.ToLinVector(),
            point3.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(double weight, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1.ToXGaFloat64Vector(),
            point2.ToXGaFloat64Vector(),
            point3.ToXGaFloat64Vector()
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundCircleFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere3D(double radius, double[] position)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere(double radius, double positionX, double positionY, double positionZ)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(positionX, positionY, positionZ),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere(double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere(double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere3D(double weight, double radius, double[] position)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere(double weight, double radius, double positionX, double positionY, double positionZ)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(positionX, positionY, positionZ),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere(double weight, double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphere(double weight, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphereFromPoints(double[] point1, double[] point2, double[] point3, double[] point4)
    {
        return RealRoundSphereFromPoints(
            point1.ToXGaVector(),
            point2.ToXGaVector(),
            point3.ToXGaVector(),
            point4.ToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphereFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphereFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3, XGaFloat64Vector point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphereFromPoints(double weight, double[] point1, double[] point2, double[] point3, double[] point4)
    {
        return RealRoundSphereFromPoints(
            weight,
            point1.ToXGaVector(),
            point2.ToXGaVector(),
            point3.ToXGaVector(),
            point4.ToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphereFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundSphereFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3, XGaFloat64Vector point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRound(double radius, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRound(double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRound(double weight, double radius, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRound(double weight, double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            radius * radius,
            position,
            direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromRowVectors(double radius, double[] position, double[,] directionVectors)
    {
        return RealRound(
            1,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .RowsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromColumnVectors(double radius, double[] position, double[,] directionVectors)
    {
        return RealRound(
            1,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .ColumnsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return RealRound(
            1,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return RealRound(
            1,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return RealRound(
            1,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double radius, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return RealRound(
            1,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double radius, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return RealRound(
            1,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromRowVectors(double weight, double radius, double[] position, double[,] directionVectors)
    {
        return RealRound(
            weight,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .RowsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromColumnVectors(double weight, double radius, double[] position, double[,] directionVectors)
    {
        return RealRound(
            weight,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .ColumnsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double weight, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return RealRound(
            weight,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double weight, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return RealRound(
            weight,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double weight, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return RealRound(
            weight,
            radius,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double weight, double radius, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return RealRound(
            weight,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round RealRoundFromVectors(double weight, double radius, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return RealRound(
            weight,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }

}