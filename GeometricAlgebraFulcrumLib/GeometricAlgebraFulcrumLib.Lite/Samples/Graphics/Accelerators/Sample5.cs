using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Svg.DrawingBoard;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using Color = SixLabors.ImageSharp.Color;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Graphics.Accelerators
{
    /// <summary>
    /// This sample show all the cases of line-BIH intersection traversal
    /// </summary>
    public static class Sample5
    {
        private static SvgDrawingBoard DrawCase(string caseName, ILine2D line, IBoundingBox2D boundingBox, double t0, double t1, double s0, double s1)
        {
            var lineDirName = 
                line.DirectionX > 0 
                ? "p" 
                : (line.DirectionX < 0 ? "n" : "z");

            var clipDirName =
                (line.DirectionX >= 0 && s0 < s1) || (line.DirectionX <= 0 && s0 > s1)
                ? "n"
                : (s0 == s1 ? "t" : "o");

            caseName = lineDirName + clipDirName + "-" + caseName;

            var drawingBoard = boundingBox.CreateDrawingBoard(4, 0);
            drawingBoard.BackgroundRectColor = Color.White;

            //Draw line
            drawingBoard
                .ActiveLayer
                .SetPen(2, Color.Black, 16, 12)
                .DrawLine(line);

            var pt0 = line.GetPointAt(t0);
            var pt1 = line.GetPointAt(t1);
            var ps0 = line.GetPointAt(s0);
            var ps1 = line.GetPointAt(s1);

            //Draw active part of line between t0 and t1
            drawingBoard
                .ActiveLayer
                .SetPen(8, Color.Black)
                .DrawLineSegment(pt0, pt1);

            //Draw left and right child nodes regions
            var boundingBoxLeft = BoundingBox2D.Create(
                double.NegativeInfinity,
                double.NegativeInfinity,
                ps0.X,
                double.PositiveInfinity
            );

            drawingBoard
                .ActiveLayer
                .SetPen(1, Color.Red)
                .SetFill(Color.Red, 0.5)
                .DrawBoundingBox(boundingBoxLeft);

            var boundingBoxRight = BoundingBox2D.Create(
                ps1.X,
                double.NegativeInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity
            );

            drawingBoard
                .ActiveLayer
                .SetPen(1, Color.Blue)
                .SetFill(Color.Blue, 0.5)
                .DrawBoundingBox(boundingBoxRight);

            //Draw points at t0 and t1
            drawingBoard
                .ActiveLayer
                .SetPen(2, Color.Black)
                .SetFill(Color.Orange)
                .DrawCircleMarker(pt0, 8)
                .SetFill(Color.OrangeRed)
                .DrawCircleMarker(pt1, 8);

            //Draw points at s0 and s1
            drawingBoard
                .ActiveLayer
                .SetPen(2, Color.Black)
                .SetFill(Color.MediumSlateBlue)
                .DrawSquareMarker(ps0, 8)
                .SetFill(Color.DarkSlateBlue)
                .DrawSquareMarker(ps1, 8);

            //Add text to drawing
            var delta = 
                drawingBoard.PixelsToLength(lineDirName == "p" ? 24 : 0);

            drawingBoard
                .ActiveLayer
                .SetPen(1, Color.Black)
                .DrawText("t0", pt0.X - delta, pt0.Y + delta)
                .DrawText("t1", pt1.X - delta, pt1.Y + delta)
                .DrawText("s0", ps0.X - delta, ps0.Y + delta);

            if (clipDirName != "t")
                drawingBoard
                    .ActiveLayer
                    .DrawText("s1", ps1.X - delta, ps1.Y + delta);
            
            var fileName = caseName + ".png";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            drawingBoard.SaveToPngFile(filePath);

            return drawingBoard;
        }

        public static void Execute()
        {
            var boundingBox = BoundingBox2D.Create(-160, -120, 160, 120);

            var line =
                Line2D.Create(
                    Float64Vector2D.Create(boundingBox.MinX, boundingBox.MinY),
                    Float64Vector2D.Create(boundingBox.GetLengthX(), boundingBox.GetLengthY())
                );

            //A) Line Direction > 0, Clip0 < Clip1 => s0 < s1
            //Case A1: t0 < t1 < s0 < s1
            DrawCase("t0-t1-s0-s1", line, boundingBox, 0.2, 0.4, 0.6, 0.8);

            //Case A2: s0 < s1 < t0 < t1
            DrawCase("s0-s1-t0-t1", line, boundingBox, 0.6, 0.8, 0.2, 0.4);

            //Case A3: s0 < t0 < t1 < s1
            DrawCase("s0-t0-t1-s1", line, boundingBox, 0.4, 0.6, 0.2, 0.8);

            //Case A4: t0 < s0 < s1 < t1
            DrawCase("t0-s0-s1-t1", line, boundingBox, 0.2, 0.8, 0.4, 0.6);

            //Case A5: t0 < s0 < t1 < s1
            DrawCase("t0-s0-t1-s1", line, boundingBox, 0.2, 0.6, 0.4, 0.8);

            //Case A6: s0 < t0 < s1 < t1
            DrawCase("s0-t0-s1-t1", line, boundingBox, 0.4, 0.8, 0.2, 0.6);


            //B) Line Direction > 0, Clip0 > Clip1 => s0 > s1
            //Case B1: t0 < t1 < s1 < s0
            DrawCase("t0-t1-s1-s0", line, boundingBox, 0.2, 0.4, 0.8, 0.6);

            //Case B2: s1 < s0 < t0 < t1
            DrawCase("s1-s0-t0-t1", line, boundingBox, 0.6, 0.8, 0.4, 0.2);

            //Case B3: s1 < t0 < t1 < s0
            DrawCase("s1-t0-t1-s0", line, boundingBox, 0.4, 0.6, 0.8, 0.2);

            //Case B4: t0 < s1 < s0 < t1
            DrawCase("t0-s1-s0-t1", line, boundingBox, 0.2, 0.8, 0.6, 0.4);

            //Case B5: t0 < s1 < t1 < s0
            DrawCase("t0-s1-t1-s0", line, boundingBox, 0.2, 0.6, 0.8, 0.4);

            //Case B6: s1 < t0 < s0 < t1
            DrawCase("s1-t0-s0-t1", line, boundingBox, 0.4, 0.8, 0.6, 0.2);


            //C) Line Direction > 0, Clip0 = Clip1 => s0 = s1
            //Case C1: t0 < t1 < s0
            DrawCase("t0-t1-s0", line, boundingBox, 0.2, 0.4, 0.7, 0.7);

            //Case C2: s0 < t0 < t1
            DrawCase("s0-t0-t1", line, boundingBox, 0.6, 0.8, 0.3, 0.3);

            //Case C3: t0 < s0 < t1
            DrawCase("t0-s0-t1", line, boundingBox, 0.2, 0.8, 0.5, 0.5);


            line =
                Line2D.Create(
                    Float64Vector2D.Create(boundingBox.MaxX, boundingBox.MinY),
                    Float64Vector2D.Create(-boundingBox.GetLengthX(), boundingBox.GetLengthY())
                );

            //D) Line Direction < 0, Clip0 < Clip1 => s0 < s1
            //Case D1: t0 < t1 < s0 < s1
            DrawCase("t0-t1-s0-s1", line, boundingBox, 0.2, 0.4, 0.6, 0.8);

            //Case D2: s0 < s1 < t0 < t1
            DrawCase("s0-s1-t0-t1", line, boundingBox, 0.6, 0.8, 0.2, 0.4);

            //Case D3: s0 < t0 < t1 < s1
            DrawCase("s0-t0-t1-s1", line, boundingBox, 0.4, 0.6, 0.2, 0.8);

            //Case D4: t0 < s0 < s1 < t1
            DrawCase("t0-s0-s1-t1", line, boundingBox, 0.2, 0.8, 0.4, 0.6);

            //Case D5: t0 < s0 < t1 < s1
            DrawCase("t0-s0-t1-s1", line, boundingBox, 0.2, 0.6, 0.4, 0.8);

            //Case D6: s0 < t0 < s1 < t1
            DrawCase("s0-t0-s1-t1", line, boundingBox, 0.4, 0.8, 0.2, 0.6);


            //E) Line Direction < 0, Clip0 > Clip1 => s0 > s1
            //Case E1: t0 < t1 < s1 < s0
            DrawCase("t0-t1-s1-s0", line, boundingBox, 0.2, 0.4, 0.8, 0.6);

            //Case E2: s1 < s0 < t0 < t1
            DrawCase("s1-s0-t0-t1", line, boundingBox, 0.6, 0.8, 0.4, 0.2);

            //Case E3: s1 < t0 < t1 < s0
            DrawCase("s1-t0-t1-s0", line, boundingBox, 0.4, 0.6, 0.8, 0.2);

            //Case E4: t0 < s1 < s0 < t1
            DrawCase("t0-s1-s0-t1", line, boundingBox, 0.2, 0.8, 0.6, 0.4);

            //Case E5: t0 < s1 < t1 < s0
            DrawCase("t0-s1-t1-s0", line, boundingBox, 0.2, 0.6, 0.8, 0.4);

            //Case E6: s1 < t0 < s0 < t1
            DrawCase("s1-t0-s0-t1", line, boundingBox, 0.4, 0.8, 0.6, 0.2);


            //F) Line Direction < 0, Clip0 = Clip1 => s0 = s1
            //Case F1: t0 < t1 < s0
            DrawCase("t0-t1-s0", line, boundingBox, 0.2, 0.4, 0.7, 0.7);

            //Case F2: s0 < t0 < t1
            DrawCase("s0-t0-t1", line, boundingBox, 0.6, 0.8, 0.3, 0.3);

            //Case F3: t0 < s0 < t1
            DrawCase("t0-s0-t1", line, boundingBox, 0.2, 0.8, 0.5, 0.5);


        }
    }
}
