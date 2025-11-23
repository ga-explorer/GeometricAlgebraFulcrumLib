using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public struct HtmlValuePoint : IHtmlValue
{
    public double X { get; }

    public double Y { get; }

    public HtmlValueLengthUnit Unit { get; }

    public string ValueText 
        => new StringBuilder()
            .Append(X.ToHtmlLengthText(Unit))
            .Append(",")
            .Append(Y.ToHtmlLengthText(Unit))
            .ToString();


    public HtmlValuePoint(double x, double y)
    {
        X = x;
        Y = y;
        Unit = HtmlValueLengthUnit.None;
    }

    public HtmlValuePoint(HtmlValueLengthUnit unit, double x, double y)
    {
        X = x;
        Y = y;
        Unit = unit ?? HtmlValueLengthUnit.None;
    }


    public HtmlValuePoint TranslateBy(double tx, double ty)
    {
        return new HtmlValuePoint(Unit, X + tx, Y + ty);
    }

    public HtmlValuePoint ScaleBy(double s)
    {
        return new HtmlValuePoint(Unit, X * s, Y * s);
    }

    public HtmlValuePoint ScaleBy(double s, double ox, double oy)
    {
        var t = 1.0d - s;

        return new HtmlValuePoint(Unit, X * s + ox * t, Y * s + oy * t);
    }

    public HtmlValuePoint ScaleBy(double sx, double sy)
    {
        return new HtmlValuePoint(Unit, X * sx, Y * sy);
    }

    public HtmlValuePoint ScaleBy(double sx, double sy, double ox, double oy)
    {
        var tx = 1.0d - sx;
        var ty = 1.0d - sy;

        return new HtmlValuePoint(Unit, X * sx + ox * tx, Y * sy + oy * ty);
    }

    public HtmlValuePoint ReflectOnX()
    {
        return new HtmlValuePoint(Unit, X, -Y);
    }

    public HtmlValuePoint ReflectOnY()
    {
        return new HtmlValuePoint(Unit, -X, Y);
    }

    public HtmlValuePoint ReflectOnOrigin()
    {
        return new HtmlValuePoint(Unit, -X, -Y);
    }

    public HtmlValuePoint ReflectOnPoint(double ox, double oy)
    {
        return new HtmlValuePoint(Unit, 2.0d * ox - X, 2.0d * oy - Y);
    }

    public HtmlValuePoint RotateByDegrees(double angle)
    {
        var cosAngle = Math.Cos(angle * Math.PI / 180.0d);
        var sinAngle = Math.Sin(angle * Math.PI / 180.0d);

        return new HtmlValuePoint(
            Unit, 
            X * cosAngle - Y * sinAngle, 
            X * sinAngle + Y * cosAngle
        );
    }

    public HtmlValuePoint RotateByDegrees(double angle, double ox, double oy)
    {
        var cosAngle = Math.Cos(angle * Math.PI / 180.0d);
        var sinAngle = Math.Sin(angle * Math.PI / 180.0d);
        var x = X - ox;
        var y = Y - oy;

        return new HtmlValuePoint(
            Unit, 
            ox + x * cosAngle - y * sinAngle, 
            oy + x * sinAngle + y * cosAngle
        );
    }

    public HtmlValuePoint RotateByRadians(double angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return new HtmlValuePoint(
            Unit,
            X * cosAngle - Y * sinAngle,
            X * sinAngle + Y * cosAngle
        );
    }

    public HtmlValuePoint RotateByRadians(double angle, double ox, double oy)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);
        var x = X - ox;
        var y = Y - oy;

        return new HtmlValuePoint(
            Unit,
            ox + x * cosAngle - y * sinAngle,
            oy + x * sinAngle + y * cosAngle
        );
    }

    //public HtmlValuePoint TransformBy(double[,] matrix3X3)
    //{

    //}

    public override string ToString()
    {
        return ValueText;
    }
}