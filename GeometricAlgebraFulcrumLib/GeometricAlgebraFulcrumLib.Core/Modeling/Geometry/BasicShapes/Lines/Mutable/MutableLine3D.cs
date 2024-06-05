using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Mutable;

public sealed class MutableLine3D : ILine3D
{
    public static MutableLine3D Create(double originX, double originY, double originZ, double directionX, double directionY, double directionZ)
    {
        return new MutableLine3D(
            originX,
            originY,
            originZ,
            directionX,
            directionY,
            directionZ
        );
    }

    public static MutableLine3D Create(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction)
    {
        return new MutableLine3D(
            origin.X,
            origin.Y,
            origin.Z,
            direction.X,
            direction.Y,
            direction.Z
        );
    }


    public double OriginX { get; set; }

    public double OriginY { get; set; }

    public double OriginZ { get; set; }


    public double DirectionX { get; set; }

    public double DirectionY { get; set; }

    public double DirectionZ { get; set; }


    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(OriginZ) &&
               !double.IsNaN(DirectionX) &&
               !double.IsNaN(DirectionY) &&
               !double.IsNaN(DirectionZ);
    }


    public MutableLine3D()
    {
    }

    internal MutableLine3D(double pX, double pY, double pZ, double vX, double vY, double vZ)
    {
        OriginX = pX;
        OriginY = pY;
        OriginZ = pZ;

        DirectionX = vX;
        DirectionY = vY;
        DirectionZ = vZ;
    }


    public MutableLine3D SetOrigin(double originX, double originY, double originZ)
    {
        OriginX = originX;
        OriginY = originY;
        OriginZ = originZ;

        return this;
    }

    public MutableLine3D SetOrigin(ILinFloat64Vector3D origin)
    {
        OriginX = origin.X;
        OriginY = origin.Y;
        OriginZ = origin.Z;

        return this;
    }

    public MutableLine3D SetDirection(double directionX, double directionY, double directionZ)
    {
        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;

        return this;
    }

    public MutableLine3D SetDirection(ILinFloat64Vector3D direction)
    {
        DirectionX = direction.X;
        DirectionY = direction.Y;
        DirectionZ = direction.Z;

        return this;
    }

    public MutableLine3D SetDirectionLength(double newLength)
    {
        var oldLength =
            DirectionX * DirectionX +
            DirectionY * DirectionY +
            DirectionZ * DirectionZ;

        var factor = newLength / oldLength;

        DirectionX = DirectionX * factor;
        DirectionY = DirectionY * factor;
        DirectionZ = DirectionZ * factor;

        return this;
    }

    public MutableLine3D SetDirectionLengthToUnit()
    {
        var oldLength =
            DirectionX * DirectionX +
            DirectionY * DirectionY +
            DirectionZ * DirectionZ;

        var factor = 1 / oldLength;

        DirectionX = DirectionX * factor;
        DirectionY = DirectionY * factor;
        DirectionZ = DirectionZ * factor;

        return this;
    }

    public MutableLine3D SetLine(double originX, double originY, double originZ, double directionX, double directionY, double directionZ)
    {
        OriginX = originX;
        OriginY = originY;
        OriginZ = originZ;

        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;

        return this;
    }

    public MutableLine3D SetLine(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction)
    {
        OriginX = origin.X;
        OriginY = origin.Y;
        OriginZ = origin.Z;

        DirectionX = direction.X;
        DirectionY = direction.Y;
        DirectionZ = direction.Z;

        return this;
    }

    public MutableLine3D SetLine(ILine3D line)
    {
        OriginX = line.OriginX;
        OriginY = line.OriginY;
        OriginZ = line.OriginZ;

        DirectionX = line.DirectionX;
        DirectionY = line.DirectionY;
        DirectionZ = line.DirectionZ;

        return this;
    }


    public Line3D ToLine()
    {
        return new Line3D(
            OriginX,
            OriginY,
            OriginZ,
            DirectionX,
            DirectionY,
            DirectionZ
        );
    }
}