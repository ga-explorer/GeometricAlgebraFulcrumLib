using System.Text;

namespace GraphicsComposerLib.POVRay.SDL.Values
{
    public sealed class SdlVectorLiteral3D : SdlValue, ISdlVectorValue
    {
        public static SdlVectorLiteral3D operator +(SdlVectorLiteral3D v1, SdlVectorLiteral3D v2)
        {
            return new SdlVectorLiteral3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static SdlVectorLiteral3D operator -(SdlVectorLiteral3D v1, SdlVectorLiteral3D v2)
        {
            return new SdlVectorLiteral3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static SdlVectorLiteral3D operator *(SdlVectorLiteral3D v1, SdlVectorLiteral3D v2)
        {
            return new SdlVectorLiteral3D(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public static SdlVectorLiteral3D operator *(SdlScalarLiteral v1, SdlVectorLiteral3D v2)
        {
            return new SdlVectorLiteral3D(v1.Scalar * v2.X, v1.Scalar * v2.Y, v1.Scalar * v2.Z);
        }

        public static SdlVectorLiteral3D operator *(SdlVectorLiteral3D v1, SdlScalarLiteral v2)
        {
            return new SdlVectorLiteral3D(v1.X * v2.Scalar, v1.Y * v2.Scalar, v1.Z * v2.Scalar);
        }

        public static SdlVectorLiteral3D operator /(SdlVectorLiteral3D v1, SdlScalarLiteral v2)
        {
            return new SdlVectorLiteral3D(v1.X / v2.Scalar, v1.Y / v2.Scalar, v1.Z / v2.Scalar);
        }

        public static SdlVectorLiteral3D operator *(double v1, SdlVectorLiteral3D v2)
        {
            return new SdlVectorLiteral3D(v1 * v2.X, v1 * v2.Y, v1 * v2.Z);
        }

        public static SdlVectorLiteral3D operator *(SdlVectorLiteral3D v1, double v2)
        {
            return new SdlVectorLiteral3D(v1.X * v2, v1.Y * v2, v1.Z * v2);
        }

        public static SdlVectorLiteral3D operator /(SdlVectorLiteral3D v1, double v2)
        {
            return new SdlVectorLiteral3D(v1.X / v2, v1.Y / v2, v1.Z / v2);
        }


        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public override string Value => new StringBuilder()
            .Append(X.ToString("G"))
            .Append(',')
            .Append(Y.ToString("G"))
            .Append(',')
            .Append(Z.ToString("G"))
            .ToString();


        public SdlVectorLiteral3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public override string ToString()
        {
            return TaggedValue;
        }
    }
}
