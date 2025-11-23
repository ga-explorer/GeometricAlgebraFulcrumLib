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
    public CGaFloat64Tangent TangentPoint(double positionX, double positionY)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(positionX, positionY),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(double positionX, double positionY, double positionZ)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(positionX, positionY, positionZ),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(params double[] position)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(LinFloat64Vector2D position)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(LinFloat64Vector3D position)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(LinFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(XGaFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(double weight, LinFloat64Vector2D position)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(double weight, LinFloat64Vector3D position)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(double weight, LinFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPoint(double weight, XGaFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            OneScalarBlade
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(double[] position, double[] direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(double weight, double[] position, double[] direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(double weight, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(double weight, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(double weight, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLine(double weight, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(double[] point1, double[] point2)
    {
        return TangentLineFromPoints(
            point1.ToLinVector(),
            point1.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(double weight, double[] point1, double[] point2)
    {
        return TangentLineFromPoints(
            weight,
            point1.ToLinVector(),
            point1.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentLineFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlane(LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlane(LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlane(XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlane(double weight, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlane(double weight, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlane(double weight, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(double[] point1, double[] point2, double[] point3)
    {
        return TangentPlaneFromPoints(
            point1.ToLinVector(),
            point2.ToLinVector(),
            point3.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction =
            Encode.VGa.Vector(point2 - point1)
                .Op(Encode.VGa.Vector(point3 - point2));

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(double weight, double[] point1, double[] point2, double[] point3)
    {
        return TangentPlaneFromPoints(
            weight,
            point1.ToLinVector(),
            point2.ToLinVector(),
            point3.ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(double weight, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction =
            Encode.VGa.Vector(point2 - point1)
                .Op(Encode.VGa.Vector(point3 - point2));

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentPlaneFromPoints(double weight, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentVolume(double weight, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Tangent(XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Tangent(CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Tangent(
            this,
            1,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Tangent(double weight, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            Encode.VGa.Vector(position),
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Tangent(double weight, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Tangent(
            this,
            weight,
            position,
            direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromRowVectors(LinFloat64Vector2D position, double[,] directionVectors)
    {
        return Tangent(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .RowsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromColumnVectors(LinFloat64Vector2D position, double[,] directionVectors)
    {
        return Tangent(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .ColumnsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return Tangent(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return Tangent(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return Tangent(
            1,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return Tangent(
            1,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Tangent(
            1,
            position,
            directionVectors.Op(Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromRowVectors(double weight, LinFloat64Vector2D position, double[,] directionVectors)
    {
        return Tangent(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .RowsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromColumnVectors(double weight, LinFloat64Vector2D position, double[,] directionVectors)
    {
        return Tangent(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .ColumnsToXGaVectors()
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(double weight, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return Tangent(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(double weight, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return Tangent(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(double weight, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return Tangent(
            weight,
            Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(Processor)
                .EncodeVGaBlade(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(double weight, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return Tangent(
            weight,
            position,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent TangentFromVectors(double weight, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Tangent(
            weight,
            position,
            directionVectors.Op(Processor)
        );
    }
}