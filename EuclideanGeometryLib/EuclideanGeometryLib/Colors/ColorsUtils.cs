using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EuclideanGeometryLib.BasicMath;

namespace EuclideanGeometryLib.Colors
{
    public static class ColorsUtils
    {
        #region Color Interpolation

        /// <summary>
        /// Interpolate between Color.White and this color using a number t between 0 and 1
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color LerpFromWhite(this Color c2, double t)
        {
            var c1 = Color.White;

            var s = 1.0d - t;
            return Color.FromArgb(
                (int)Math.Round(s * c1.A + t * c2.A),
                (int)Math.Round(s * c1.R + t * c2.R),
                (int)Math.Round(s * c1.G + t * c2.G),
                (int)Math.Round(s * c1.B + t * c2.B)
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
            var c1 = Color.Black;

            var s = 1.0d - t;
            return Color.FromArgb(
                (int)Math.Round(s * c1.A + t * c2.A),
                (int)Math.Round(s * c1.R + t * c2.R),
                (int)Math.Round(s * c1.G + t * c2.G),
                (int)Math.Round(s * c1.B + t * c2.B)
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
            var s = 1.0d - t;
            return Color.FromArgb(
                (int)Math.Round(s * c1.A + t * c2.A),
                (int)Math.Round(s * c1.R + t * c2.R),
                (int)Math.Round(s * c1.G + t * c2.G),
                (int)Math.Round(s * c1.B + t * c2.B)
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
            var c2 = Color.White;

            var s = 1.0d - t;
            return Color.FromArgb(
                (int)Math.Round(s * c1.A + t * c2.A),
                (int)Math.Round(s * c1.R + t * c2.R),
                (int)Math.Round(s * c1.G + t * c2.G),
                (int)Math.Round(s * c1.B + t * c2.B)
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
            var c2 = Color.Black;

            var s = 1.0d - t;
            return Color.FromArgb(
                (int)Math.Round(s * c1.A + t * c2.A),
                (int)Math.Round(s * c1.R + t * c2.R),
                (int)Math.Round(s * c1.G + t * c2.G),
                (int)Math.Round(s * c1.B + t * c2.B)
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
            var s = 1.0d - t;
            return Color.FromArgb(
                (int)Math.Round(s * c1.A + t * c2.A),
                (int)Math.Round(s * c1.R + t * c2.R),
                (int)Math.Round(s * c1.G + t * c2.G),
                (int)Math.Round(s * c1.B + t * c2.B)
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

                colorPaletteList.Add(Color.FromArgb(
                    (int)(red * 255),
                    (int)(green * 255),
                    (int)(blue * 255)
                ));
            }

            return colorPaletteList.ToArray();
        }

        public static Image SaveToImage(this Color[] colorPalette, int imageWidth)
        {
            var image = new Bitmap(imageWidth, colorPalette.Length);

            for (var row = 0; row < colorPalette.Length; row++)
            {
                var color = colorPalette[row];

                for (var col = 0; col < imageWidth; col++)
                    image.SetPixel(col, row, color);
            }

            return image;
        }

        public static Color ToSystemColor(double red, double green, double blue)
        {
            var r = (int)(red.Clamp(1) * 255);
            var g = (int)(green.Clamp(1) * 255);
            var b = (int)(blue.Clamp(1) * 255);

            return Color.FromArgb(r, g, b);
        }

        public static IEnumerable<double> GetNormalizedColorComponentsRgb(this Color color)
        {
            const double d = 1.0d / 255.0d;

            yield return d * color.R;
            yield return d * color.G;
            yield return d * color.B;
        }

        public static IEnumerable<double> GetNormalizedColorComponentsRgbA(this Color color)
        {
            const double d = 1.0d / 255.0d;

            yield return d * color.R;
            yield return d * color.G;
            yield return d * color.B;
            yield return d * color.A;
        }

        public static IEnumerable<double> GetNormalizedColorComponentsARgb(this Color color)
        {
            const double d = 1.0d / 255.0d;

            yield return d * color.A;
            yield return d * color.R;
            yield return d * color.G;
            yield return d * color.B;
        }
    }
}
