using System.Text;
using DataStructuresLib.Basic;
using WebComposerLib.Html.Media;

namespace WebComposerLib.Svg.Values;

public readonly struct SvgPoint : 
    ISvgValue,
    IPair<double>
{
    public static SvgPoint Create(double x, double y)
    {
        return new SvgPoint(x, y);
    }
        
    public static SvgPoint Create(IPair<double> point)
    {
        return new SvgPoint(point.Item1, point.Item2);
    }
        
    public static SvgPoint Create(SvgLengthUnit unit, double x, double y)
    {
        return new SvgPoint(unit, x, y);
    }
        
    public static SvgPoint Create(SvgLengthUnit unit, IPair<double> point)
    {
        return new SvgPoint(unit, point.Item1, point.Item2);
    }


    public double X { get; }

    public double Y { get; }

    public double Item1 
        => X;

    public double Item2 
        => Y;

    public SvgLengthUnit Unit { get; }

    public string ValueText 
        => new StringBuilder()
            .Append(X.ToSvgLengthText(Unit))
            .Append(",")
            .Append(Y.ToSvgLengthText(Unit))
            .ToString();


    private SvgPoint(double x, double y)
    {
        X = x;
        Y = y;
        Unit = SvgLengthUnit.None;
    }

    private SvgPoint(SvgLengthUnit unit, double x, double y)
    {
        X = x;
        Y = y;
        Unit = unit;
    }


    public SvgPoint TranslateBy(double tx, double ty)
    {
        return Create(Unit, X + tx, Y + ty);
    }

    public SvgPoint ScaleBy(double s)
    {
        return Create(Unit, X * s, Y * s);
    }

    public SvgPoint ScaleBy(double s, double ox, double oy)
    {
        var t = 1.0d - s;

        return Create(Unit, X * s + ox * t, Y * s + oy * t);
    }

    public SvgPoint ScaleBy(double sx, double sy)
    {
        return Create(Unit, X * sx, Y * sy);
    }

    public SvgPoint ScaleBy(double sx, double sy, double ox, double oy)
    {
        var tx = 1.0d - sx;
        var ty = 1.0d - sy;

        return Create(Unit, X * sx + ox * tx, Y * sy + oy * ty);
    }

    public SvgPoint ReflectOnX()
    {
        return Create(Unit, X, -Y);
    }

    public SvgPoint ReflectOnY()
    {
        return Create(Unit, -X, Y);
    }

    public SvgPoint ReflectOnOrigin()
    {
        return Create(Unit, -X, -Y);
    }

    public SvgPoint ReflectOnPoint(double ox, double oy)
    {
        return Create(Unit, 2.0d * ox - X, 2.0d * oy - Y);
    }

    public SvgPoint RotateByDegrees(double angle)
    {
        var cosAngle = Math.Cos(angle * Math.PI / 180.0d);
        var sinAngle = Math.Sin(angle * Math.PI / 180.0d);

        return Create(Unit, 
            X * cosAngle - Y * sinAngle, 
            X * sinAngle + Y * cosAngle);
    }

    public SvgPoint RotateByDegrees(double angle, double ox, double oy)
    {
        var cosAngle = Math.Cos(angle * Math.PI / 180.0d);
        var sinAngle = Math.Sin(angle * Math.PI / 180.0d);
        var x = X - ox;
        var y = Y - oy;

        return Create(Unit, 
            ox + x * cosAngle - y * sinAngle, 
            oy + x * sinAngle + y * cosAngle);
    }

    public SvgPoint RotateByRadians(double angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return Create(Unit,
            X * cosAngle - Y * sinAngle,
            X * sinAngle + Y * cosAngle);
    }

    public SvgPoint RotateByRadians(double angle, double ox, double oy)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);
        var x = X - ox;
        var y = Y - oy;

        return Create(Unit,
            ox + x * cosAngle - y * sinAngle,
            oy + x * sinAngle + y * cosAngle);
    }

    //public SvgValuePoint TransformBy(double[,] matrix3X3)
    //{

    //}

    public override string ToString()
    {
        return ValueText;
    }

}