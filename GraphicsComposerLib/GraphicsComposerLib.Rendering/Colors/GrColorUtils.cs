using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GraphicsComposerLib.Rendering.Colors;

public static class GrColorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D RgbToVector3D(this Color color)
    {
        var c = color.ToPixel<Rgb24>();

        return Float64Vector3D.Create(
            c.R / 255.0d,
            c.G / 255.0d,
            c.B / 255.0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D RgbaToVector4D(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return Float64Vector4D.Create(
            c.R / 255.0d,
            c.G / 255.0d,
            c.B / 255.0d,
            c.A / 255.0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToRgbColor(this ITriplet<double> colorVector)
    {
        return Color.FromRgb(
            (byte) (colorVector.Item1.ClampToUnit() * 255),
            (byte) (colorVector.Item2.ClampToUnit() * 255),
            (byte) (colorVector.Item3.ClampToUnit() * 255)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToRgbaColor(this IQuad<double> colorVector)
    {
        return Color.FromRgba(
            (byte) (colorVector.Item1.ClampToUnit() * 255),
            (byte) (colorVector.Item2.ClampToUnit() * 255),
            (byte) (colorVector.Item3.ClampToUnit() * 255),
            (byte) (colorVector.Item4.ClampToUnit() * 255)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color RgbLerp(this double t, Color color1, Color color2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        var c1 = color1.RgbToVector3D();
        var c2 = color2.RgbToVector3D();
            
        return ((1.0d - t) * c1 + t * c2).ToRgbColor();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color RgbaLerp(this double t, Color color1, Color color2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        var c1 = color1.RgbaToVector4D();
        var c2 = color2.RgbaToVector4D();
            
        return ((1.0d - t) * c1 + t * c2).ToRgbaColor();
    }
}