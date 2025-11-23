using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(double positionX, double positionY)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(positionX, positionY),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(double positionX, double positionY, double positionZ)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(positionX, positionY, positionZ),
            OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(params double[] position)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(LinFloat64Vector2D position)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(LinFloat64Vector position)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(XGaFloat64Vector position)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(double weight, LinFloat64Vector2D position)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(double weight, LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(double weight, LinFloat64Vector position)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPoint(double weight, XGaFloat64Vector position)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(double[] position, double[] direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(double weight, double[] position, double[] direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(double weight, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(double weight, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(double weight, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLine(double weight, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(double[] point1, double[] point2)
    {
        var position = point1.VectorAdd(point2).VectorDivide(2);
        var direction = point2.VectorSubtract(point1);

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(double weight, double[] point1, double[] point2)
    {
        var position = point1.VectorAdd(point2).VectorDivide(2);
        var direction = point2.VectorSubtract(point1);

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatLineFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane3D(double[] position, double[] normal)
    {
        if (position.Length != 3 || normal.Length != 3)
            throw new ArgumentException();

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(normal.ToLinVector3D().NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(LinFloat64Vector3D position, LinFloat64Vector3D normal)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(normal.NormalToUnitDirection3D())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane3D(double distance, double[] normal)
    {
        return FlatPlane(distance, normal.ToLinVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(double distance, LinFloat64Vector3D normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane3D(double weight, double distance, double[] normal)
    {
        return FlatPlane(weight, distance, normal.ToLinVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(double weight, double distance, LinFloat64Vector3D normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(double[] point1, double[] point2, double[] point3)
    {
        var position = point1.VectorAdd(point2).VectorAdd(point3).VectorDivide(3);
        var direction = XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)point2.VectorSubtract(point1)).Op(XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)point3.VectorSubtract(point2)));

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(double weight, double[] point1, double[] point2, double[] point3)
    {
        var position = point1.VectorAdd(point2).VectorAdd(point3).VectorDivide(3);
        var direction = XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)point2.VectorSubtract(point1)).Op(XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)point3.VectorSubtract(point2)));

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlaneFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatVolume(LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatVolume(LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(double weight, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(double weight, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatPlane(double weight, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatVolume(double weight, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Flat(XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Flat(CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Flat(
            this,
            1,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Flat(double weight, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Flat(double weight, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Flat(
            this,
            weight,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return Flat(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return Flat(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return Flat(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return Flat(
            1,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Flat(
            1,
            position,
            directionVectors.Op(Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(double weight, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return Flat(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(double weight, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return Flat(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(double weight, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return Flat(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(double weight, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return Flat(
            weight,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromVectors(double weight, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Flat(
            weight,
            position,
            directionVectors.Op(Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromPoints(double weight, params LinFloat64Vector2D[] egaPoints)
    {
        return Encode.OpnsFlat.BladeFromPoints(
                (IReadOnlyList<XGaFloat64Vector>)egaPoints.SelectToArray(p => p.ToXGaVector())
            ).DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromPoints(double weight, params LinFloat64Vector3D[] egaPoints)
    {
        return Encode.OpnsFlat.BladeFromPoints(
                (IReadOnlyList<XGaFloat64Vector>)egaPoints.SelectToArray(p => p.ToXGaVector())
            ).DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromPoints(double weight, params LinFloat64Vector[] egaPoints)
    {
        return Encode.OpnsFlat.BladeFromPoints(
                (IReadOnlyList<XGaFloat64Vector>)egaPoints.SelectToArray(p => p.ToXGaFloat64Vector())
            )
            .DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromPoints(double weight, params XGaFloat64Vector[] egaPoints)
    {
        return Encode.OpnsFlat.BladeFromPoints((IReadOnlyList<XGaFloat64Vector>)egaPoints)
            .DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat FlatFromPoints(double weight, IReadOnlyList<XGaFloat64Vector> egaPoints)
    {
        return Encode.OpnsFlat.BladeFromPoints(egaPoints)
            .DecodeOpnsFlat.Element();
    }
}