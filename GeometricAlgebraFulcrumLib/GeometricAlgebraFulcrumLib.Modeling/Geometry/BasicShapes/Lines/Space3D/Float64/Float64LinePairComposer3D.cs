using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64LinePairComposer3D : 
    IFloat64LinePair3D
{
    public static Float64LinePairComposer3D Create(IFloat64Line3D line1, IFloat64Line3D line2)
    {
        return new Float64LinePairComposer3D(
            line1.OriginX,
            line1.OriginY,
            line1.OriginZ,
            line1.DirectionX,
            line1.DirectionY,
            line1.DirectionZ,
            line2.OriginX,
            line2.OriginY,
            line2.OriginZ,
            line2.DirectionX,
            line2.DirectionY,
            line2.DirectionZ
        );
    }

    public static Float64LinePairComposer3D Create(ILinFloat64Vector3D origin1, ILinFloat64Vector3D direction1, ILinFloat64Vector3D origin2, ILinFloat64Vector3D direction2)
    {
        return new Float64LinePairComposer3D(
            origin1.X,
            origin1.Y,
            origin1.Z,
            direction1.X,
            direction1.Y,
            direction1.Z,
            origin2.X,
            origin2.Y,
            origin2.Z,
            direction2.X,
            direction2.Y,
            direction2.Z
        );
    }


    public double Origin1X { get; set; }

    public double Origin1Y { get; set; }

    public double Origin1Z { get; set; }


    public double Origin2X { get; set; }

    public double Origin2Y { get; set; }

    public double Origin2Z { get; set; }


    public double Direction1X { get; set; }

    public double Direction1Y { get; set; }

    public double Direction1Z { get; set; }


    public double Direction2X { get; set; }

    public double Direction2Y { get; set; }

    public double Direction2Z { get; set; }


    public bool IsValid()
    {
        return !double.IsNaN(Origin1X) &&
               !double.IsNaN(Origin1Y) &&
               !double.IsNaN(Direction1X) &&
               !double.IsNaN(Direction1Y) &&
               !double.IsNaN(Origin2X) &&
               !double.IsNaN(Origin2Y) &&
               !double.IsNaN(Direction2X) &&
               !double.IsNaN(Direction2Y);
    }


    public Float64LinePairComposer3D()
    {
    }

    internal Float64LinePairComposer3D(double o1X, double o1Y, double o1Z, double d1X, double d1Y, double d1Z, double o2X, double o2Y, double o2Z, double d2X, double d2Y, double d2Z)
    {
        Origin1X = o1X;
        Origin1Y = o1Y;
        Origin1Z = o1Z;

        Origin2X = o2X;
        Origin2Y = o2Y;
        Origin2Z = o2Z;

        Direction1X = d1X;
        Direction1Y = d1Y;
        Direction1Z = d1Z;

        Direction2X = d2X;
        Direction2Y = d2Y;
        Direction2Z = d2Z;
    }


    public Float64LinePairComposer3D SetOrigin1(double originX, double originY, double originZ)
    {
        Origin1X = originX;
        Origin1Y = originY;
        Origin1Z = originZ;

        return this;
    }

    public Float64LinePairComposer3D SetOrigin1(ILinFloat64Vector3D origin)
    {
        Origin1X = origin.X;
        Origin1Y = origin.Y;
        Origin1Z = origin.Z;

        return this;
    }

    public Float64LinePairComposer3D SetDirection1(double directionX, double directionY, double directionZ)
    {
        Direction1X = directionX;
        Direction1Y = directionY;
        Direction1Z = directionZ;

        return this;
    }

    public Float64LinePairComposer3D SetDirection1(ILinFloat64Vector3D direction)
    {
        Direction1X = direction.X;
        Direction1Y = direction.Y;
        Direction1Z = direction.Z;

        return this;
    }

    public Float64LinePairComposer3D SetLine1(IFloat64Line3D line)
    {
        Origin1X = line.OriginX;
        Origin1Y = line.OriginY;
        Origin1Z = line.OriginZ;

        Direction1X = line.DirectionX;
        Direction1Y = line.DirectionY;
        Direction1Z = line.DirectionZ;

        return this;
    }

    public Float64LinePairComposer3D SetOrigin2(double originX, double originY, double originZ)
    {
        Origin2X = originX;
        Origin2Y = originY;
        Origin2Z = originZ;

        return this;
    }

    public Float64LinePairComposer3D SetOrigin2(ILinFloat64Vector3D origin)
    {
        Origin2X = origin.X;
        Origin2Y = origin.Y;
        Origin2Z = origin.Z;

        return this;
    }

    public Float64LinePairComposer3D SetDirection2(double directionX, double directionY, double directionZ)
    {
        Direction2X = directionX;
        Direction2Y = directionY;
        Direction2Z = directionZ;

        return this;
    }

    public Float64LinePairComposer3D SetDirection2(ILinFloat64Vector3D direction)
    {
        Direction2X = direction.X;
        Direction2Y = direction.Y;
        Direction2Z = direction.Z;

        return this;
    }

    public Float64LinePairComposer3D SetLine2(IFloat64Line3D line)
    {
        Origin2X = line.OriginX;
        Origin2Y = line.OriginY;
        Origin2Z = line.OriginZ;

        Direction2X = line.DirectionX;
        Direction2Y = line.DirectionY;
        Direction2Z = line.DirectionZ;

        return this;
    }

    public Float64LinePairComposer3D SetLinePair(IFloat64Line3D line1, IFloat64Line3D line2)
    {
        Origin1X = line1.OriginX;
        Origin1Y = line1.OriginY;
        Origin1Z = line1.OriginZ;

        Direction1X = line1.DirectionX;
        Direction1Y = line1.DirectionY;
        Direction1Z = line1.DirectionZ;

        Origin2X = line2.OriginX;
        Origin2Y = line2.OriginY;
        Origin2Z = line2.OriginZ;

        Direction2X = line2.DirectionX;
        Direction2Y = line2.DirectionY;
        Direction2Z = line2.DirectionZ;

        return this;
    }
}