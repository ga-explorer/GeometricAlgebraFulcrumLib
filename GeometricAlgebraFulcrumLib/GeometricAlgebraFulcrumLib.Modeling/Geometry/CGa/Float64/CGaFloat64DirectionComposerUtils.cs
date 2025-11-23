using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionScalar(double weight, double direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.Scalar(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionScalar(double weight, IntegerSign direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.Scalar(direction.ToFloat64())
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(double directionX, double directionY)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Vector(directionX, directionY)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(double directionX, double directionY, double directionZ)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Vector(directionX, directionY, directionZ)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(params double[] direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(LinFloat64Vector2D direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(LinFloat64Vector3D direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(LinFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Vector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(XGaFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Vector(direction)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(double weight, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(double weight, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(double weight, LinFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionLine(double weight, XGaFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionPlane(LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionPlane(LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionPlane(XGaFloat64Bivector direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionPlane(double weight, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionPlane(double weight, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionPlane(double weight, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionVolume(double weight, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction Direction(XGaFloat64KVector direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction Direction(CGaFloat64Blade direction)
    {
        return new CGaFloat64Direction(
            this,
            1,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction Direction(double weight, XGaFloat64KVector direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction Direction(double weight, CGaFloat64Blade direction)
    {
        return new CGaFloat64Direction(
            this,
            weight,
            direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromColumnVectors(double[,] directionVectors)
    {
        return Direction(
            1,
            directionVectors.ColumnsToXGaVectors().Op(Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromRowVectors(double[,] directionVectors)
    {
        return Direction(
            1,
            directionVectors.RowsToXGaVectors().Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(params LinFloat64Vector2D[] directionVectors)
    {
        return Direction(
            1,
            directionVectors.Select(v => v.ToXGaVector()).Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(params LinFloat64Vector3D[] directionVectors)
    {
        return Direction(
            1,
            directionVectors.Select(v => v.ToXGaVector()).Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(params LinFloat64Vector[] directionVectors)
    {
        return Direction(
            1,
            directionVectors.Select(v => v.ToXGaFloat64Vector()).Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(params XGaFloat64Vector[] directionVectors)
    {
        return Direction(
            1,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Direction(
            1,
            directionVectors.Op(Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromColumnVectors(double weight, double[,] directionVectors)
    {
        return Direction(
            weight,
            directionVectors.ColumnsToXGaVectors().Op(Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromRowVectors(double weight, double[,] directionVectors)
    {
        return Direction(
            weight,
            directionVectors.RowsToXGaVectors().Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(double weight, params LinFloat64Vector2D[] directionVectors)
    {
        return Direction(
            weight,
            directionVectors.Select(v => v.ToXGaVector()).Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(double weight, params LinFloat64Vector3D[] directionVectors)
    {
        return Direction(
            weight,
            directionVectors.Select(v => v.ToXGaVector()).Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(double weight, params LinFloat64Vector[] directionVectors)
    {
        return Direction(
            weight,
            directionVectors.Select(v => v.ToXGaFloat64Vector()).Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(double weight, params XGaFloat64Vector[] directionVectors)
    {
        return Direction(
            weight,
            directionVectors.Op(Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Direction DirectionFromVectors(double weight, IEnumerable<XGaFloat64Vector> directionVectors)
    {
        return Direction(
            weight,
            directionVectors.Op(Processor)
        );
    }
}