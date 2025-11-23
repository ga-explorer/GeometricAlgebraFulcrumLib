using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double radius, double[] position, double[] direction)
    {
        return ImaginaryRoundPointPair(
            radius,
            position.CreateLinVector(),
            direction.CreateLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double radius, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(double[] point1, double[] point2)
    {
        return ImaginaryRoundPointPairFromPoints(
            point1.CreateLinVector(),
            point2.CreateLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            this,
            1,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double weight, double radius, double[] position, double[] direction)
    {
        return ImaginaryRoundPointPair(
            weight, 
            radius,
            position.CreateLinVector(),
            direction.CreateLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double weight, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double weight, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double weight, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPair(double weight, double radius, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(double weight, double[] point1, double[] point2)
    {
        return ImaginaryRoundPointPairFromPoints(
            weight, 
            point1.CreateLinVector(),
            point2.CreateLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).L2NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundPointPairFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            this,
            weight,
            -radiusSquared,
            Encode.VGa.Vector(center),
            Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircle(double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircle(double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircle(double radius, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircle(double weight, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircle(double weight, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircle(double weight, double radius, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(double[] point1, double[] point2, double[] point3)
    {
        return ImaginaryRoundCircleFromPoints(
            point1.ToLinVector(), 
            point2.ToLinVector(), 
            point3.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1.ToXGaFloat64Vector(),
            point2.ToXGaFloat64Vector(),
            point3.ToXGaFloat64Vector()
        ).DecodeOpnsRound.Element();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(double weight, double[] point1, double[] point2, double[] point3)
    {
        return ImaginaryRoundCircleFromPoints(
            weight,
            point1.ToLinVector(), 
            point2.ToLinVector(), 
            point3.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(double weight, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1.ToXGaFloat64Vector(),
            point2.ToXGaFloat64Vector(),
            point3.ToXGaFloat64Vector()
        ).DecodeOpnsRound.Element();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundCircleFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var round = Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphere3D(double radius, double[] position)
    {
        return ImaginaryRoundSphere(radius, position.ToLinVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphere(double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphere(double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphere3D(double weight, double radius, double[] position)
    {
        return ImaginaryRoundSphere(weight, radius, position.ToLinVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphere(double weight, double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphere(double weight, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphereFromPoints(double[] point1, double[] point2, double[] point3, double[] point4)
    {
        return ImaginaryRoundSphereFromPoints(
            point1.ToXGaVector(),
            point2.ToXGaVector(),
            point3.ToXGaVector(),
            point4.ToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphereFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphereFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3, XGaFloat64Vector point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphereFromPoints(double weight, double[] point1, double[] point2, double[] point3, double[] point4)
    {
        return ImaginaryRoundSphereFromPoints(
            weight, 
            point1.ToXGaVector(),
            point2.ToXGaVector(),
            point3.ToXGaVector(),
            point4.ToXGaVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphereFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundSphereFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3, XGaFloat64Vector point4)
    {
        var round = Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRound(double radius, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRound(double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            this,
            1,
            -radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRound(double weight, double radius, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRound(double weight, double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            this,
            weight,
            -radius * radius,
            position,
            direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundFromColumnVectors(double radius, LinFloat64Vector2D position, double[,] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double radius, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return ImaginaryRound(
            1,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundFromVectors(double radius, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return ImaginaryRound(
            1,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundFromColumnVectors(double weight, double radius, LinFloat64Vector2D position, double[,] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double weight, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double weight, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double weight, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return ImaginaryRound(
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
    public CGaFloat64Round ImaginaryRoundFromVectors(double weight, double radius, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return ImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Round ImaginaryRoundFromVectors(double weight, double radius, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return ImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(Processor)
        );
    }


}