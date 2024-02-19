namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

public sealed class SdlScalarLiteral : SdlValue, ISdlScalarValue
{
    public static SdlScalarLiteral Zero { get; private set; }

    public static SdlScalarLiteral One { get; private set; }


    static SdlScalarLiteral()
    {
        Zero = new SdlScalarLiteral(0.0D);

        One = new SdlScalarLiteral(1.0D);
    }


    public static SdlScalarLiteral operator *(SdlScalarLiteral s1, SdlScalarLiteral s2)
    {
        return new SdlScalarLiteral(s1.Scalar * s2.Scalar);
    }


    public double Scalar { get; set; }

    public override string Value => Scalar.ToString("G");


    public SdlScalarLiteral(double scalar)
    {
        Scalar = scalar;
    }
}