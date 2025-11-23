using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingBoard
{
    public class ImageSharpDrawingBoard : 
        IDisposable
    {
        public static PngEncoder DefaultPngEncoder { get; set; }
            = new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha,
                CompressionLevel = PngCompressionLevel.NoCompression,
                FilterMethod = PngFilterMethod.None,
                TransparentColorMode = PngTransparentColorMode.Preserve
            };


        public static ImageSharpDrawingBoard Create(WorldToScreenMap2D worldToScreen, Color backgroundColor)
        {
            return new ImageSharpDrawingBoard(worldToScreen, backgroundColor);
        }


        private readonly FontCollection _fonts = new FontCollection();


        public Color BackgroundColor { get; private set; }

        public WorldToScreenMap2D WorldToScreen { get; private set; }

        public int ScreenWidth
            => WorldToScreen.ScreenWidth;

        public int ScreenHeight
            => WorldToScreen.ScreenHeight;

        public int ScreenMargin
            => WorldToScreen.ScreenMargin;

        public double WorldSizeX 
            => WorldToScreen.WorldBox.SizeX;
        
        public double WorldSizeY 
            => WorldToScreen.WorldBox.SizeY;

        public Image<Rgba32> Image { get; private set; }


        private ImageSharpDrawingBoard(WorldToScreenMap2D worldToScreen, Color backgroundColor)
        {
            WorldToScreen = worldToScreen;
            BackgroundColor = backgroundColor;
            Image = new Image<Rgba32>(
                ScreenWidth,
                ScreenHeight,
                BackgroundColor.ToPixel<Rgba32>()
            );
        }

        
        public virtual void Dispose()
        {
            Image.Dispose();
        }

        public ImageSharpDrawingBoard ClearBoard(Color backgroundColor)
        {
            BackgroundColor = backgroundColor;

            Image.Dispose();
            Image = new Image<Rgba32>(
                ScreenWidth,
                ScreenHeight,
                BackgroundColor.ToPixel<Rgba32>()
            );

            return this;
        }


        public ImageSharpDrawingBoard DrawPoints(IEnumerable<LinFloat64Vector2D> points, ImageSharpPointMarkerDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawPoints(WorldToScreen, points, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawArrows(IEnumerable<(LinFloat64Vector2D, LinFloat64PolarAngle)> pointAnglePairs, ImageSharpArrowHeadDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawArrows(WorldToScreen, pointAnglePairs, style)
            );
            
            return this;
        }
        
        public ImageSharpDrawingBoard DrawPolyLine(IReadOnlyList<LinFloat64Vector2D> points, ImageSharpLineDrawingStyle lineStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawPolyLine(WorldToScreen, points, lineStyle)
            );
            
            return this;
        }
        
        public ImageSharpDrawingBoard DrawPolyLine(IReadOnlyList<LinFloat64Vector2D> points, ImageSharpLineDrawingStyle lineStyle, ImageSharpPointMarkerDrawingStyle pointStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawPolyLine(WorldToScreen, points, lineStyle, pointStyle)
            );
            
            return this;
        }
        
        public ImageSharpDrawingBoard DrawLine((LinFloat64Vector2D, LinFloat64Vector2D) pointPair, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawLine(WorldToScreen, pointPair, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawLine(IPair<LinFloat64Vector2D> pointPair, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawLine(WorldToScreen, pointPair, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawLines(IEnumerable<Float64LineSegment2D> pointPairs, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawLines(WorldToScreen, pointPairs, style)
            );
            
            return this;
        }
        
        public ImageSharpDrawingBoard DrawLines(IEnumerable<IPair<LinFloat64Vector2D>> pointPairs, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawLines(WorldToScreen, pointPairs, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawLines(IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pointPairs, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawLines(WorldToScreen, pointPairs, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawVectors(LinFloat64Vector2D origin, IEnumerable<LinFloat64Vector2D> directions, ImageSharpLineDrawingStyle lineStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawVectors(WorldToScreen, origin, directions, lineStyle)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawVectors(IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pointDirectionPairs, ImageSharpLineDrawingStyle lineStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawVectors(WorldToScreen, pointDirectionPairs, lineStyle)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawVectors(LinFloat64Vector2D origin, IEnumerable<LinFloat64Vector2D> directions, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawVectors(WorldToScreen, origin, directions, lineStyle, arrowStyle)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawVectors(IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pointDirectionPairs, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawVectors(WorldToScreen, pointDirectionPairs, lineStyle, arrowStyle)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawCircle(LinFloat64Vector2D center, double radius, ImageSharpLineDrawingStyle lineStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawCircle(WorldToScreen, center, radius, lineStyle)
            );
            
            return this;
        }
        
        public ImageSharpDrawingBoard DrawSquare(LinFloat64Vector2D center, double size, ImageSharpLineDrawingStyle lineStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawSquare(WorldToScreen, center, size, lineStyle)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawRectangle(LinFloat64Vector2D center, double sizeX, double sizeY, ImageSharpLineDrawingStyle lineStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawRectangle(WorldToScreen, center, sizeX, sizeY, lineStyle)
            );
            
            return this;
        }
        
        public ImageSharpDrawingBoard DrawRectangle(Float64BoundingBox2D box, ImageSharpLineDrawingStyle lineStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawRectangle(WorldToScreen, box, lineStyle)
            );
            
            return this;
        }


        public ImageSharpDrawingBoard DrawVertexPoints(Float64RegularPolygon2D polygon, ImageSharpPointMarkerDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawVertexPoints(WorldToScreen, polygon, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawSideMidPoints(Float64RegularPolygon2D polygon, ImageSharpPointMarkerDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawSideMidPoints(WorldToScreen, polygon, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawSides(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawSides(WorldToScreen, polygon, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawDiagonals(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawDiagonals(WorldToScreen, polygon, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawChords(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
        {
            Image.Mutate(ctx => 
                ctx.DrawChords(WorldToScreen, polygon, style)
            );
            
            return this;
        }

        public ImageSharpDrawingBoard DrawSideNormals(Float64RegularPolygon2D polygon, double length, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
        {
            Image.Mutate(ctx => 
                ctx.DrawSideNormals(WorldToScreen, polygon, length, lineStyle, arrowStyle)
            );
            
            return this;
        }


        public ImageSharpDrawingBoard SavePngImage(string filePath)
        {
            Image.SaveAsPng($"{filePath}.png", DefaultPngEncoder);

            return this;
        }

    }
}
