using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Colors;

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

    #region Color Interpolation

    /// <summary>
        /// Interpolate between Color.White and this color using a number t between 0 and 1
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color LerpFromWhite(this Color c2, double t)
        {
            var color1 = Color.White.ToPixel<Rgba32>();
            var color2 = c2.ToPixel<Rgba32>();

            var s = 1.0d - t;
            return Color.FromRgba(
                (byte) Math.Round(s * color1.R + t * color2.R),
                (byte) Math.Round(s * color1.G + t * color2.G),
                (byte) Math.Round(s * color1.B + t * color2.B),
                (byte) Math.Round(s * color1.A + t * color2.A)
            );
        }

    /// <summary>
        /// Interpolate between Color.Black and this color using a number t between 0 and 1
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color LerpFromBlack(this Color c2, double t)
        {
            var color1 = Color.Black.ToPixel<Rgba32>();
            var color2 = c2.ToPixel<Rgba32>();

            var s = 1.0d - t;
            return Color.FromRgba(
                (byte) Math.Round(s * color1.R + t * color2.R),
                (byte) Math.Round(s * color1.G + t * color2.G),
                (byte) Math.Round(s * color1.B + t * color2.B),
                (byte) Math.Round(s * color1.A + t * color2.A)
            );
        }

    /// <summary>
        /// Interpolate between c1 and this color using a number t between 0 and 1
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="c1"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color LerpFromColor(this Color c2, Color c1, double t)
        {
            var color1 = c1.ToPixel<Rgba32>();
            var color2 = c2.ToPixel<Rgba32>();

            var s = 1.0d - t;
            return Color.FromRgba(
                (byte) Math.Round(s * color1.R + t * color2.R),
                (byte) Math.Round(s * color1.G + t * color2.G),
                (byte) Math.Round(s * color1.B + t * color2.B),
                (byte) Math.Round(s * color1.A + t * color2.A)
            );
        }

    /// <summary>
        /// Interpolate between this color and Color.White using a number t between 0 and 1
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color LerpToWhite(this Color c1, double t)
        {
            var color1 = c1.ToPixel<Rgba32>();
            var color2 = Color.White.ToPixel<Rgba32>();

            var s = 1.0d - t;
            return Color.FromRgba(
                (byte) Math.Round(s * color1.R + t * color2.R),
                (byte) Math.Round(s * color1.G + t * color2.G),
                (byte) Math.Round(s * color1.B + t * color2.B),
                (byte) Math.Round(s * color1.A + t * color2.A)
            );
        }

    /// <summary>
        /// Interpolate between this color and Color.Black using a number t between 0 and 1
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color LerpToBlack(this Color c1, double t)
        {
            var color1 = c1.ToPixel<Rgba32>();
            var color2 = Color.Black.ToPixel<Rgba32>();

            var s = 1.0d - t;
            return Color.FromRgba(
                (byte) Math.Round(s * color1.R + t * color2.R),
                (byte) Math.Round(s * color1.G + t * color2.G),
                (byte) Math.Round(s * color1.B + t * color2.B),
                (byte) Math.Round(s * color1.A + t * color2.A)
            );
        }

    /// <summary>
        /// Interpolate between this color and c1 using a number t between 0 and 1
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color LerpToColor(this Color c1, Color c2, double t)
        {
            var color1 = c1.ToPixel<Rgba32>();
            var color2 = c2.ToPixel<Rgba32>();

            var s = 1.0d - t;
            return Color.FromRgba(
                (byte) Math.Round(s * color1.R + t * color2.R),
                (byte) Math.Round(s * color1.G + t * color2.G),
                (byte) Math.Round(s * color1.B + t * color2.B),
                (byte) Math.Round(s * color1.A + t * color2.A)
            );
        }

    /// <summary>
        /// Interpolate between Color.White and this color using a list of numbers each between 0 and 1
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="tList"></param>
        /// <returns></returns>
        public static IEnumerable<Color> LerpFromWhite(this Color c2, IEnumerable<double> tList)
        {
            return tList.Select(t => c2.LerpFromWhite(t));
        }

    /// <summary>
        /// Interpolate between Color.Black and this color using a list of numbers each between 0 and 1
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="tList"></param>
        /// <returns></returns>
        public static IEnumerable<Color> LerpFromBlack(this Color c2, IEnumerable<double> tList)
        {
            return tList.Select(t => c2.LerpFromBlack(t));
        }

    /// <summary>
        /// Interpolate between c1 and this color using a list of numbers each between 0 and 1
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="c1"></param>
        /// <param name="tList"></param>
        /// <returns></returns>
        public static IEnumerable<Color> LerpFromWhite(this Color c2, Color c1, IEnumerable<double> tList)
        {
            return tList.Select(t => c2.LerpFromColor(c2, t));
        }

    /// <summary>
        /// Interpolate between this color and Color.White using a list of numbers each between 0 and 1
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="tList"></param>
        /// <returns></returns>
        public static IEnumerable<Color> LerpToWhite(this Color c1, IEnumerable<double> tList)
        {
            return tList.Select(t => c1.LerpToWhite(t));
        }

    /// <summary>
        /// Interpolate between this color and Color.Black using a list of numbers each between 0 and 1
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="tList"></param>
        /// <returns></returns>
        public static IEnumerable<Color> LerpToBlack(this Color c1, IEnumerable<double> tList)
        {
            return tList.Select(t => c1.LerpToBlack(t));
        }

    /// <summary>
        /// Interpolate between this color and c2 using a list of numbers each between 0 and 1
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="tList"></param>
        /// <returns></returns>
        public static IEnumerable<Color> LerpToWhite(this Color c1, Color c2, IEnumerable<double> tList)
        {
            return tList.Select(t => c1.LerpToColor(c2, t));
        }

        #endregion

        public static Color[] GetHsvPalette(int colorsCount)
        {
            var colorPaletteList = new List<Color>(colorsCount);

            var xStep = 1 / (double)colorsCount;
            for (double x = 0; x < 1; x += xStep)
            {
                var red = Math.Min(4 * x - 1.5, -4 * x + 4.5);
                if (red < 0) red = 0; else if (red > 1) red = 1;

                var green = Math.Min(4 * x - 0.5, -4 * x + 3.5);
                if (green < 0) green = 0; else if (green > 1) green = 1;

                var blue = Math.Min(4 * x + 0.5, -4 * x + 2.5);
                if (blue < 0) blue = 0; else if (blue > 1) blue = 1;

                colorPaletteList.Add(Color.FromRgb(
                    (byte) (red * 255),
                    (byte) (green * 255),
                    (byte) (blue * 255)
                ));
            }

            return colorPaletteList.ToArray();
        }

        public static Image SaveToImage(this Color[] colorPalette, int imageWidth)
        {
            var image = new Image<Rgba32>(imageWidth, colorPalette.Length);

            for (var row = 0; row < colorPalette.Length; row++)
            {
                var color = colorPalette[row];

                for (var col = 0; col < imageWidth; col++)
                    image[col, row] = color;
            }

            return image;
        }

        public static Color ToSystemColor(double red, double green, double blue)
        {
            var r = (byte) (double.Clamp(red, 0d, 1d) * 255);
            var g = (byte) (double.Clamp(green, 0d, 1d) * 255);
            var b = (byte) (double.Clamp(blue, 0d, 1d) * 255);

            return Color.FromRgb(r, g, b);
        }

        public static IEnumerable<double> GetNormalizedColorComponentsRgb(this Color c)
        {
            const double d = 1.0d / 255.0d;

            var color = c.ToPixel<Rgb24>();

            yield return d * color.R;
            yield return d * color.G;
            yield return d * color.B;
        }

        public static IEnumerable<double> GetNormalizedColorComponentsRgbA(this Color c)
        {
            const double d = 1.0d / 255.0d;

            var color = c.ToPixel<Rgba32>();

            yield return d * color.R;
            yield return d * color.G;
            yield return d * color.B;
            yield return d * color.A;
        }

        public static IEnumerable<double> GetNormalizedColorComponentsARgb(this Color c)
        {
            const double d = 1.0d / 255.0d;

            var color = c.ToPixel<Rgba32>();

            yield return d * color.A;
            yield return d * color.R;
            yield return d * color.G;
            yield return d * color.B;
        }
}