using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Globalization;
using System.Xml.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Svg.DrawingBoard
{
    public class SvgDrawingBoard : IDisposable
    {
        private readonly XElement _svgRoot;
        private readonly XNamespace _svgNs = "http://www.w3.org/2000/svg";
        private readonly XElement _defsElement;
        private readonly Dictionary<string, string> _markerCache = new();
        private int _markerCounter = 0;

        public WorldToScreenMap2D WorldToScreen { get; }
        public Color BackgroundColor { get; private set; }

        public int ScreenWidth => WorldToScreen.ScreenWidth;
        public int ScreenHeight => WorldToScreen.ScreenHeight;

        public SvgDrawingBoard(WorldToScreenMap2D worldToScreen, Color backgroundColor)
        {
            WorldToScreen = worldToScreen;
            BackgroundColor = backgroundColor;

            _svgRoot = new XElement(_svgNs + "svg",
                new XAttribute("xmlns", _svgNs),
                new XAttribute("width", ScreenWidth),
                new XAttribute("height", ScreenHeight),
                new XAttribute("viewBox", $"0 0 {ScreenWidth} {ScreenHeight}")
            );

            _defsElement = new XElement(_svgNs + "defs");
            _svgRoot.Add(_defsElement);

            _svgRoot.Add(new XElement(_svgNs + "rect",
                new XAttribute("width", "100%"),
                new XAttribute("height", "100%"),
                new XAttribute("fill", ColorToSvg(BackgroundColor))
            ));
        }

        public void Dispose() { }

        public SvgDrawingBoard ClearBoard(Color backgroundColor)
        {
            BackgroundColor = backgroundColor;
            _svgRoot.Elements().Remove();
            _defsElement.RemoveNodes();
            _markerCache.Clear();
            _markerCounter = 0;

            _svgRoot.Add(_defsElement);
            _svgRoot.Add(new XElement(_svgNs + "rect",
                new XAttribute("width", "100%"),
                new XAttribute("height", "100%"),
                new XAttribute("fill", ColorToSvg(BackgroundColor))
            ));

            return this;
        }

        // -------------------------
        // Public API
        // -------------------------

        public SvgDrawingBoard DrawPoints(IEnumerable<LinFloat64Vector2D> points, ImageSharpPointMarkerDrawingStyle style)
        {
            foreach (var p in points) DrawPoint(p, style);
            return this;
        }

        public SvgDrawingBoard DrawPoint(LinFloat64Vector2D point, ImageSharpPointMarkerDrawingStyle style)
        {
            var (cx, cy) = WorldToScreen.MapPoint(point);
            var stroke = ColorToSvg(style.StrokeColor);
            var fill = ColorToSvg(style.FillColor);
            var size = style.Size;
            var half = size / 2f;

            switch (style.Shape)
            {
                case PointMarkerShape.Circle:
                    _svgRoot.Add(new XElement(_svgNs + "circle",
                        new XAttribute("cx", F(cx)),
                        new XAttribute("cy", F(cy)),
                        new XAttribute("r", F(half)),
                        new XAttribute("stroke", stroke),
                        new XAttribute("stroke-width", F(style.StrokeWidth)),
                        new XAttribute("fill", fill)
                    ));
                    break;

                case PointMarkerShape.Square:
                    _svgRoot.Add(new XElement(_svgNs + "rect",
                        new XAttribute("x", F(cx - half)),
                        new XAttribute("y", F(cy - half)),
                        new XAttribute("width", F(size)),
                        new XAttribute("height", F(size)),
                        new XAttribute("stroke", stroke),
                        new XAttribute("stroke-width", F(style.StrokeWidth)),
                        new XAttribute("fill", fill)
                    ));
                    break;

                case PointMarkerShape.Cross:
                    AddLine(cx - half, cy, cx + half, cy, stroke, style.StrokeWidth);
                    AddLine(cx, cy - half, cx, cy + half, stroke, style.StrokeWidth);
                    break;

                case PointMarkerShape.X:
                    AddLine(cx - half, cy - half, cx + half, cy + half, stroke, style.StrokeWidth);
                    AddLine(cx - half, cy + half, cx + half, cy - half, stroke, style.StrokeWidth);
                    break;

                case PointMarkerShape.TriangleUp:
                    _svgRoot.Add(new XElement(_svgNs + "polygon",
                        new XAttribute("points",
                            $"{F(cx)},{F(cy - half)} {F(cx - half)},{F(cy + half)} {F(cx + half)},{F(cy + half)}"),
                        new XAttribute("stroke", stroke),
                        new XAttribute("stroke-width", F(style.StrokeWidth)),
                        new XAttribute("fill", fill)
                    ));
                    break;

                case PointMarkerShape.TriangleDown:
                    _svgRoot.Add(new XElement(_svgNs + "polygon",
                        new XAttribute("points",
                            $"{F(cx)},{F(cy + half)} {F(cx - half)},{F(cy - half)} {F(cx + half)},{F(cy - half)}"),
                        new XAttribute("stroke", stroke),
                        new XAttribute("stroke-width", F(style.StrokeWidth)),
                        new XAttribute("fill", fill)
                    ));
                    break;
            }

            return this;
        }
        
        public SvgDrawingBoard DrawLines(IEnumerable<Pair<LinFloat64Vector2D>> pairs, ImageSharpLineDrawingStyle style)
        {
            var stroke = ColorToSvg(style.StrokeColor);
            foreach (var (p1, p2) in pairs)
            {
                var (x1, y1) = WorldToScreen.MapPoint(p1);
                var (x2, y2) = WorldToScreen.MapPoint(p2);

                AddLine(x1, y1, x2, y2, stroke, style.StrokeWidth);
            }
            return this;
        }

        public SvgDrawingBoard DrawLines(IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pairs, ImageSharpLineDrawingStyle style)
        {
            var stroke = ColorToSvg(style.StrokeColor);
            foreach (var (p1, p2) in pairs)
            {
                var (x1, y1) = WorldToScreen.MapPoint(p1);
                var (x2, y2) = WorldToScreen.MapPoint(p2);
                AddLine(x1, y1, x2, y2, stroke, style.StrokeWidth);
            }
            return this;
        }

        public SvgDrawingBoard DrawPolyLine(IReadOnlyList<LinFloat64Vector2D> points, ImageSharpLineDrawingStyle style)
        {
            var stroke = ColorToSvg(style.StrokeColor);
            var path = string.Join(" ", points.Select(p =>
            {
                var (x, y) = WorldToScreen.MapPoint(p);
                return $"{F(x)},{F(y)}";
            }));

            _svgRoot.Add(new XElement(_svgNs + "polyline",
                new XAttribute("points", path),
                new XAttribute("stroke", stroke),
                new XAttribute("stroke-width", F(style.StrokeWidth)),
                new XAttribute("fill", "none")
            ));

            return this;
        }

        public SvgDrawingBoard DrawArrows(IEnumerable<(LinFloat64Vector2D, LinFloat64PolarAngle)> pairs, ImageSharpArrowHeadDrawingStyle style)
        {
            var stroke = ColorToSvg(style.StrokeColor);
            var fill = ColorToSvg(style.FillColor);

            foreach (var (end, angle) in pairs)
            {
                var (x, y) = WorldToScreen.MapPoint(end);
                var deg = -angle.DegreesValue;
                var path = ArrowPath(style);

                var g = new XElement(_svgNs + "g",
                    new XAttribute("transform", $"translate({F(x)},{F(y)}) rotate({F(deg)})"),
                    new XElement(_svgNs + "path",
                        new XAttribute("d", path),
                        new XAttribute("stroke", stroke),
                        new XAttribute("stroke-width", F(style.StrokeWidth)),
                        new XAttribute("fill", fill)
                    )
                );

                _svgRoot.Add(g);
            }

            return this;
        }

        public SvgDrawingBoard DrawVectors(IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> pairs, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
        {
            var strokeLine = ColorToSvg(lineStyle.StrokeColor);
            var strokeArrow = ColorToSvg(arrowStyle.StrokeColor);
            var fillArrow = ColorToSvg(arrowStyle.FillColor);
            var markerId = GetOrCreateArrowMarker(arrowStyle);

            foreach (var (origin, dir) in pairs)
            {
                var (x1, y1) = WorldToScreen.MapPoint(origin);
                var (x2, y2) = WorldToScreen.MapPoint(origin + dir);

                _svgRoot.Add(new XElement(_svgNs + "line",
                    new XAttribute("x1", F(x1)),
                    new XAttribute("y1", F(y1)),
                    new XAttribute("x2", F(x2)),
                    new XAttribute("y2", F(y2)),
                    new XAttribute("stroke", strokeLine),
                    new XAttribute("stroke-width", F(lineStyle.StrokeWidth)),
                    new XAttribute("marker-end", $"url(#{markerId})")
                ));
            }

            return this;
        }

        public SvgDrawingBoard DrawCircle(LinFloat64Vector2D center, double radius, ImageSharpLineDrawingStyle style)
        {
            var (cx, cy) = WorldToScreen.MapPoint(center);
            var r = WorldToScreen.MapSize(radius).Item1;
            _svgRoot.Add(new XElement(_svgNs + "circle",
                new XAttribute("cx", F(cx)),
                new XAttribute("cy", F(cy)),
                new XAttribute("r", F(r)),
                new XAttribute("stroke", ColorToSvg(style.StrokeColor)),
                new XAttribute("stroke-width", F(style.StrokeWidth)),
                new XAttribute("fill", "none")
            ));
            return this;
        }

        public SvgDrawingBoard DrawRectangle(LinFloat64Vector2D center, double sizeX, double sizeY, ImageSharpLineDrawingStyle style)
        {
            var (cx, cy) = WorldToScreen.MapPoint(center);
            var (w, h) = WorldToScreen.MapSize(sizeX, sizeY);
            _svgRoot.Add(new XElement(_svgNs + "rect",
                new XAttribute("x", F(cx - w / 2)),
                new XAttribute("y", F(cy - h / 2)),
                new XAttribute("width", F(w)),
                new XAttribute("height", F(h)),
                new XAttribute("stroke", ColorToSvg(style.StrokeColor)),
                new XAttribute("stroke-width", F(style.StrokeWidth)),
                new XAttribute("fill", "none")
            ));
            return this;
        }

        public SvgDrawingBoard DrawPolygon(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
        {
            var stroke = ColorToSvg(style.StrokeColor);
            var points = string.Join(" ", polygon.VertexPoints.Select(p =>
            {
                var (x, y) = WorldToScreen.MapPoint(p);
                return $"{F(x)},{F(y)}";
            }));

            _svgRoot.Add(new XElement(_svgNs + "polygon",
                new XAttribute("points", points),
                new XAttribute("stroke", stroke),
                new XAttribute("stroke-width", F(style.StrokeWidth)),
                new XAttribute("fill", "none")
            ));

            return this;
        }

        public SvgDrawingBoard DrawPolygonSides(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
        {
            var pairs = polygon.Sides;
            return DrawLines(pairs, style);
        }

        public SvgDrawingBoard DrawPolygonChords(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
        {
            var pairs = polygon.Chords;
            return DrawLines(pairs, style);
        }

        public SvgDrawingBoard DrawPolygonDiagonals(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle style)
        {
            var pairs = polygon.Diagonals;
            return DrawLines(pairs, style);
        }

        public SvgDrawingBoard DrawPolygonSideNormals(Float64RegularPolygon2D polygon, ImageSharpLineDrawingStyle lineStyle, ImageSharpArrowHeadDrawingStyle arrowStyle)
        {
            var strokeLine = ColorToSvg(lineStyle.StrokeColor);
            var markerId = GetOrCreateArrowMarker(arrowStyle);

            foreach (var (p1, p2) in polygon.Sides)
            {
                var side = p2 - p1;
                var mid = (p1 + p2) / 2;
                var normal = side.GetUnitNormal() * polygon.InnerRadius;

                var (x1, y1) = WorldToScreen.MapPoint(mid);
                var (x2, y2) = WorldToScreen.MapPoint(mid + normal);

                _svgRoot.Add(new XElement(_svgNs + "line",
                    new XAttribute("x1", F(x1)),
                    new XAttribute("y1", F(y1)),
                    new XAttribute("x2", F(x2)),
                    new XAttribute("y2", F(y2)),
                    new XAttribute("stroke", strokeLine),
                    new XAttribute("stroke-width", F(lineStyle.StrokeWidth)),
                    new XAttribute("marker-end", $"url(#{markerId})")
                ));
            }

            return this;
        }


        // -------------------------
        // Output
        // -------------------------
        public string ToSvgString() => _svgRoot.ToString();
        public void SaveToFile(string path) 
            => new XDocument(_svgRoot).Save(path);

        // -------------------------
        // Internals
        // -------------------------
        private void AddLine(double x1, double y1, double x2, double y2, string stroke, float strokeWidth)
        {
            _svgRoot.Add(new XElement(_svgNs + "line",
                new XAttribute("x1", F(x1)),
                new XAttribute("y1", F(y1)),
                new XAttribute("x2", F(x2)),
                new XAttribute("y2", F(y2)),
                new XAttribute("stroke", stroke),
                new XAttribute("stroke-width", F(strokeWidth))
            ));
        }

        private string GetOrCreateArrowMarker(ImageSharpArrowHeadDrawingStyle style)
        {
            var key = $"{style.SizeX}-{style.SizeY}-{style.StrokeColor}-{style.FillColor}";
            if (_markerCache.TryGetValue(key, out var id)) return id;

            id = $"marker{_markerCounter++}";
            _markerCache[key] = id;

            var marker = new XElement(_svgNs + "marker",
                new XAttribute("id", id),
                new XAttribute("viewBox", $"-{F(style.SizeX)} -{F(style.SizeY)} {F(style.SizeX * 2)} {F(style.SizeY * 2)}"),
                new XAttribute("refX", "0"),
                new XAttribute("refY", "0"),
                new XAttribute("orient", "auto"),
                new XAttribute("markerWidth", F(style.SizeX)),
                new XAttribute("markerHeight", F(style.SizeY))
            );

            marker.Add(new XElement(_svgNs + "path",
                new XAttribute("d", ArrowPath(style)),
                new XAttribute("fill", ColorToSvg(style.FillColor)),
                new XAttribute("stroke", ColorToSvg(style.StrokeColor)),
                new XAttribute("stroke-width", F(style.StrokeWidth))
            ));

            _defsElement.Add(marker);
            return id;
        }

        private static string ArrowPath(ImageSharpArrowHeadDrawingStyle style)
        {
            // Tip at (0,0), tail left
            return $"M 0 0 L -{F(style.SizeX)} {F(style.SizeY / 2)} L -{F(style.SizeX * 0.6)} 0 L -{F(style.SizeX)} -{F(style.SizeY / 2)} Z";
        }
        
        private static string ColorToSvg(Color c)
        {
            var p = c.ToPixel<Rgba32>();
            var alpha = (p.A / 255.0).ToString("0.###", CultureInfo.InvariantCulture);

            return $"rgba({p.R},{p.G},{p.B},{alpha})";
        }

        private static string F(double v) => v.ToString("0.###", CultureInfo.InvariantCulture);
        private static string F(float v) => v.ToString("0.###", CultureInfo.InvariantCulture);
    }
}
