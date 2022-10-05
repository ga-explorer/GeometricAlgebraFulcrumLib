using System.Text;

namespace GraphicsComposerLib.Rendering.Svg.Values
{
    public struct SvgValuePoint : ISvgValue
    {
        public double X { get; }

        public double Y { get; }

        public SvgValueLengthUnit Unit { get; }

        public string ValueText 
            => new StringBuilder()
                .Append(X.ToSvgLengthText(Unit))
                .Append(",")
                .Append(Y.ToSvgLengthText(Unit))
                .ToString();


        public SvgValuePoint(double x, double y)
        {
            X = x;
            Y = y;
            Unit = SvgValueLengthUnit.None;
        }

        public SvgValuePoint(SvgValueLengthUnit unit, double x, double y)
        {
            X = x;
            Y = y;
            Unit = unit ?? SvgValueLengthUnit.None;
        }


        public SvgValuePoint TranslateBy(double tx, double ty)
        {
            return new SvgValuePoint(Unit, X + tx, Y + ty);
        }

        public SvgValuePoint ScaleBy(double s)
        {
            return new SvgValuePoint(Unit, X * s, Y * s);
        }

        public SvgValuePoint ScaleBy(double s, double ox, double oy)
        {
            var t = 1.0d - s;

            return new SvgValuePoint(Unit, X * s + ox * t, Y * s + oy * t);
        }

        public SvgValuePoint ScaleBy(double sx, double sy)
        {
            return new SvgValuePoint(Unit, X * sx, Y * sy);
        }

        public SvgValuePoint ScaleBy(double sx, double sy, double ox, double oy)
        {
            var tx = 1.0d - sx;
            var ty = 1.0d - sy;

            return new SvgValuePoint(Unit, X * sx + ox * tx, Y * sy + oy * ty);
        }

        public SvgValuePoint ReflectOnX()
        {
            return new SvgValuePoint(Unit, X, -Y);
        }

        public SvgValuePoint ReflectOnY()
        {
            return new SvgValuePoint(Unit, -X, Y);
        }

        public SvgValuePoint ReflectOnOrigin()
        {
            return new SvgValuePoint(Unit, -X, -Y);
        }

        public SvgValuePoint ReflectOnPoint(double ox, double oy)
        {
            return new SvgValuePoint(Unit, 2.0d * ox - X, 2.0d * oy - Y);
        }

        public SvgValuePoint RotateByDegrees(double angle)
        {
            var cosAngle = Math.Cos(angle * Math.PI / 180.0d);
            var sinAngle = Math.Sin(angle * Math.PI / 180.0d);

            return new SvgValuePoint(
                Unit, 
                X * cosAngle - Y * sinAngle, 
                X * sinAngle + Y * cosAngle
            );
        }

        public SvgValuePoint RotateByDegrees(double angle, double ox, double oy)
        {
            var cosAngle = Math.Cos(angle * Math.PI / 180.0d);
            var sinAngle = Math.Sin(angle * Math.PI / 180.0d);
            var x = X - ox;
            var y = Y - oy;

            return new SvgValuePoint(
                Unit, 
                ox + x * cosAngle - y * sinAngle, 
                oy + x * sinAngle + y * cosAngle
            );
        }

        public SvgValuePoint RotateByRadians(double angle)
        {
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new SvgValuePoint(
                Unit,
                X * cosAngle - Y * sinAngle,
                X * sinAngle + Y * cosAngle
            );
        }

        public SvgValuePoint RotateByRadians(double angle, double ox, double oy)
        {
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);
            var x = X - ox;
            var y = Y - oy;

            return new SvgValuePoint(
                Unit,
                ox + x * cosAngle - y * sinAngle,
                oy + x * sinAngle + y * cosAngle
            );
        }

        //public SvgValuePoint TransformBy(double[,] matrix3X3)
        //{

        //}

        public override string ToString()
        {
            return ValueText;
        }
    }
}
