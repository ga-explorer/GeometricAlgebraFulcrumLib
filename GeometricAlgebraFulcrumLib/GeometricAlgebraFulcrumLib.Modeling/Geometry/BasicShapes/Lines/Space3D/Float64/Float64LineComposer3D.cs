using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64LineComposer3D : 
    IFloat64Line3D
{
    public static Float64LineComposer3D Create(double originX, double originY, double originZ, double directionX, double directionY, double directionZ)
    {
        return new Float64LineComposer3D(
            originX,
            originY,
            originZ,
            directionX,
            directionY,
            directionZ
        );
    }

    public static Float64LineComposer3D Create(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction)
    {
        return new Float64LineComposer3D(
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


    public Float64LineComposer3D()
    {
    }

    internal Float64LineComposer3D(double pX, double pY, double pZ, double vX, double vY, double vZ)
    {
        OriginX = pX;
        OriginY = pY;
        OriginZ = pZ;

        DirectionX = vX;
        DirectionY = vY;
        DirectionZ = vZ;
    }


    public Float64LineComposer3D SetOrigin(double originX, double originY, double originZ)
    {
        OriginX = originX;
        OriginY = originY;
        OriginZ = originZ;

        return this;
    }

    public Float64LineComposer3D SetOrigin(ILinFloat64Vector3D origin)
    {
        OriginX = origin.X;
        OriginY = origin.Y;
        OriginZ = origin.Z;

        return this;
    }

    public Float64LineComposer3D SetDirection(double directionX, double directionY, double directionZ)
    {
        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;

        return this;
    }

    public Float64LineComposer3D SetDirection(ILinFloat64Vector3D direction)
    {
        DirectionX = direction.X;
        DirectionY = direction.Y;
        DirectionZ = direction.Z;

        return this;
    }

    public Float64LineComposer3D SetDirectionLength(double newLength)
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

    public Float64LineComposer3D SetDirectionLengthToUnit()
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

    public Float64LineComposer3D SetLine(double originX, double originY, double originZ, double directionX, double directionY, double directionZ)
    {
        OriginX = originX;
        OriginY = originY;
        OriginZ = originZ;

        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;

        return this;
    }

    public Float64LineComposer3D SetLine(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction)
    {
        OriginX = origin.X;
        OriginY = origin.Y;
        OriginZ = origin.Z;

        DirectionX = direction.X;
        DirectionY = direction.Y;
        DirectionZ = direction.Z;

        return this;
    }

    public Float64LineComposer3D SetLine(IFloat64Line3D line)
    {
        OriginX = line.OriginX;
        OriginY = line.OriginY;
        OriginZ = line.OriginZ;

        DirectionX = line.DirectionX;
        DirectionY = line.DirectionY;
        DirectionZ = line.DirectionZ;

        return this;
    }


    public Float64Line3D ToLine()
    {
        return new Float64Line3D(
            OriginX,
            OriginY,
            OriginZ,
            DirectionX,
            DirectionY,
            DirectionZ
        );
    }
}