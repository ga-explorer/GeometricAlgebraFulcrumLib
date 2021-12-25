using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Svg.Content;
using GraphicsComposerLib.Svg.Elements.Containers;
using GraphicsComposerLib.Svg.Elements.Shape;
using GraphicsComposerLib.Svg.Elements.Text;
using GraphicsComposerLib.Svg.Transforms;

namespace GraphicsComposerLib.Svg.DrawingBoard
{
    public class SvgDrawingBoardLayer
    {
        private readonly SvgContentsList _contentsList 
            = new SvgContentsList();


        public SvgDrawingBoard ParentDrawingBoard { get; }

        public SvgDrawingBoardStyle DefaultStyle { get; }

        public SvgDrawingBoardStyle CurrentStyle { get; }

        public SvgDrawingBoardLayerActiveStyle ActiveStyle { get; set; } 
            = SvgDrawingBoardLayerActiveStyle.Current;

        public string LayerName { get; set; }

        public bool IsVisible { get; set; } = true;

        public bool IsEmpty => _contentsList.Count == 0;


        /// <summary>
        /// The current width in pixels of the pen used to draw the upcoming shapes
        /// </summary>
        public int PenPixelsWidth
        {
            get { return CurrentStyle.PenPixelsWidth; }
            set { CurrentStyle.PenPixelsWidth = value; }
        }

        /// <summary>
        /// The current color of the pen used to draw the upcoming shapes
        /// </summary>
        public Color PenColor
        {
            get { return CurrentStyle.PenColor; }
            set { CurrentStyle.PenColor = value; }
        }

        /// <summary>
        /// The current opacity of the pen used to draw the upcoming shapes
        /// </summary>
        public double PenOpacity
        {
            get { return CurrentStyle.PenOpacity; }
            set { CurrentStyle.PenOpacity = value; }
        }

        /// <summary>
        /// The current dash pattern of the pen used to draw the upcoming shapes
        /// </summary>
        public string PenDashPattern
            => CurrentStyle.PenDashPattern;

        /// <summary>
        /// The current fill color of the pen used to draw the upcoming shapes
        /// </summary>
        public Color FillColor
        {
            get { return CurrentStyle.FillColor; }
            set { CurrentStyle.FillColor = value; }
        }

        /// <summary>
        /// The current fill opacity of the pen used to draw the upcoming shapes
        /// </summary>
        public double FillOpacity
        {
            get { return CurrentStyle.FillOpacity; }
            set { CurrentStyle.FillOpacity = value; }
        }


        /// <summary>
        /// The default width in pixels of the pen used to draw shapes
        /// </summary>
        public int DefaultPenPixelsWidth
        {
            get { return DefaultStyle.PenPixelsWidth; }
            set { DefaultStyle.PenPixelsWidth = value; }
        }

        /// <summary>
        /// The default color of the pen used to draw shapes
        /// </summary>
        public Color DefaultPenColor
        {
            get { return DefaultStyle.PenColor; }
            set { DefaultStyle.PenColor = value; }
        }

        /// <summary>
        /// The default opacity of the pen used to draw shapes
        /// </summary>
        public double DefaultPenOpacity
        {
            get { return DefaultStyle.PenOpacity; }
            set { DefaultStyle.PenOpacity = value; }
        }

        /// <summary>
        /// The current dash pattern of the pen used to draw shapes
        /// </summary>
        public string DefaultPenDashPattern
            => DefaultStyle.PenDashPattern;

        /// <summary>
        /// The default fill color of the pen used to draw shapes
        /// </summary>
        public Color DefaultFillColor
        {
            get { return DefaultStyle.FillColor; }
            set { DefaultStyle.FillColor = value; }
        }

        /// <summary>
        /// The current fill opacity of the pen used to draw shapes
        /// </summary>
        public double DefaultFillOpacity
        {
            get { return DefaultStyle.FillOpacity; }
            set { DefaultStyle.FillOpacity = value; }
        }


        internal SvgDrawingBoardLayer(SvgDrawingBoard parentDrawingBoard, string layerName)
        {
            LayerName = layerName;
            DefaultStyle = new SvgDrawingBoardStyle(parentDrawingBoard);
            CurrentStyle = new SvgDrawingBoardStyle(parentDrawingBoard);
            ParentDrawingBoard = parentDrawingBoard;
        }


        /// <summary>
        /// Clear all contents of white board layer without changing its current
        /// properties
        /// </summary>
        /// <returns></returns>
        public SvgDrawingBoardLayer Clear()
        {
            _contentsList.Clear();

            return this;
        }

        /// <summary>
        /// Clear all contents of white board layer and reset its
        /// properties to their defaults
        /// </summary>
        /// <returns></returns>
        public SvgDrawingBoardLayer Reset()
        {
            DefaultStyle.Reset();
            CurrentStyle.Reset();

            return Clear();
        }

        public SvgDrawingBoardLayer Show()
        {
            IsVisible = true;

            return this;
        }

        public SvgDrawingBoardLayer Hide()
        {
            IsVisible = false;

            return this;
        }

        public SvgElementGroup GetSvgElement()
        {
            var svgGroup = SvgElementGroup.Create(LayerName);

            svgGroup.Transform.SetTo(
                SvgTransformComposer.Create(
                    SvgTransformTranslate.Create(0, ParentDrawingBoard.ViewBoxMidY),
                    SvgTransformScale.Create(1, -1),
                    SvgTransformTranslate.Create(0, -ParentDrawingBoard.ViewBoxMidY)
                )
            );

            foreach (var svgElement in _contentsList)
                svgGroup.Contents.Append(svgElement);

            svgGroup.Style.UpdateFrom(DefaultStyle, true, true, true);

            return svgGroup;
        }


        public SvgDrawingBoardLayer SetPen(int penPixelsWidth, Color penColor, params int[] penDashPattern)
        {
            CurrentStyle.SetPen(penPixelsWidth, penColor, penDashPattern);

            return this;
        }

        public SvgDrawingBoardLayer SetFill(Color fillColor, double fillOpacity = 1)
        {
            CurrentStyle.SetFill(fillColor, fillOpacity);

            return this;
        }

        /// <summary>
        /// Set the current pen's drawing pattern to solid
        /// </summary>
        /// <returns></returns>
        public SvgDrawingBoardLayer ClearPenDashPattern()
        {
            CurrentStyle.ClearPenDashPattern();

            return this;
        }

        /// <summary>
        /// Set the current pen's drawing pattern
        /// </summary>
        /// <param name="penDashPattern"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer SetPenDashPattern(params int[] penDashPattern)
        {
            CurrentStyle.SetPenDashPattern(penDashPattern);

            return this;
        }


        public SvgDrawingBoardLayer SetDefaultPen(int penPixelsWidth, Color penColor, params int[] penDashPattern)
        {
            DefaultStyle.SetPen(penPixelsWidth, penColor, penDashPattern);

            return this;
        }

        public SvgDrawingBoardLayer SetDefaultFill(Color fillColor, double fillOpacity = 1)
        {
            DefaultStyle.SetFill(fillColor, fillOpacity);

            return this;
        }

        /// <summary>
        /// Set the default pen's drawing pattern to solid
        /// </summary>
        /// <returns></returns>
        public SvgDrawingBoardLayer ClearDefaultPenDashPattern()
        {
            DefaultStyle.ClearPenDashPattern();

            return this;
        }

        /// <summary>
        /// Set the default pen's drawing pattern
        /// </summary>
        /// <param name="penDashPattern"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer SetDefaultPenDashPattern(params int[] penDashPattern)
        {
            CurrentStyle.SetPenDashPattern(penDashPattern);

            return this;
        }


        /// <summary>
        /// Draw a rectangle that fills the whole view box
        /// </summary>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawFullRectangle()
        {
            var rectangle = SvgElementRectangle
                .Create()
                .SetRectangle(
                    ParentDrawingBoard.ViewBoxMinX,
                    ParentDrawingBoard.ViewBoxMinY,
                    ParentDrawingBoard.ViewBoxWidth,
                    ParentDrawingBoard.ViewBoxHeight
                );

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                rectangle.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(rectangle);

            return this;
        }

        /// <summary>
        /// Draw a straight line
        /// </summary>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <param name="point2X"></param>
        /// <param name="point2Y"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawRectangle(double point1X, double point1Y, double point2X, double point2Y)
        {
            var rectangle = SvgElementRectangle
                .Create()
                .SetRectangle(
                    Math.Min(point1X, point2X),
                    Math.Min(point1Y, point2Y),
                    Math.Abs(point1X - point2X),
                    Math.Abs(point1Y - point2Y)
                );

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                rectangle.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(rectangle);

            return this;
        }

        /// <summary>
        /// Draw a straight line segment between two points
        /// </summary>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <param name="point2X"></param>
        /// <param name="point2Y"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawLineSegment(double point1X, double point1Y, double point2X, double point2Y)
        {
            var lineSegment = SvgElementLine
                .Create()
                .SetLine(
                    point1X, 
                    point1Y, 
                    point2X, 
                    point2Y
                );

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                lineSegment.Style.UpdateFrom(CurrentStyle, true, false, false);

            _contentsList.Append(lineSegment);

            return this;
        }

        /// <summary>
        /// Draw a circle marker
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawCircle(double centerX, double centerY, double radius)
        {
            var circle = SvgElementCircle
                .Create()
                .SetCircle(
                    centerX,
                    centerY,
                    radius
                );

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                circle.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(circle);

            return this;
        }

        /// <summary>
        /// Draw a circle marker
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawSquare(double centerX, double centerY, double radius)
        {
            var circle = SvgElementRectangle
                .Create()
                .SetRectangle(
                    centerX - radius,
                    centerY - radius,
                    2 * radius,
                    2 * radius
                );

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                circle.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(circle);

            return this;
        }

        /// <summary>
        /// Draw a regular polygon
        /// </summary>
        /// <param name="sidesCount"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="offsetAngle"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawPolygon(int sidesCount, double centerX, double centerY, double radius, double offsetAngle = 0)
        {
            if (sidesCount < 2)
                return this;

            if (sidesCount == 2)
            {
                var x = centerX + radius * Math.Cos(offsetAngle);
                var y = centerY + radius * Math.Sin(offsetAngle);

                return DrawLineSegment(-x, -y, x, y);
            }

            var angleFactor = 2 * Math.PI / sidesCount;
            var pointsList = Enumerable
                .Range(0, sidesCount)
                .Select(i => offsetAngle + i * angleFactor)
                .Select(a => (ITuple2D)new Tuple2D(
                    centerX + radius * Math.Cos(a),
                    centerY + radius * Math.Sin(a)
                ));

            return DrawPolygon(pointsList);
        }

        /// <summary>
        /// Draw an arbitrary polygon
        /// </summary>
        /// <param name="pointsList"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawPolygon(IEnumerable<ITuple2D> pointsList)
        {
            var polygon = SvgElementPolygon
                .Create()
                .SetPoints(pointsList);

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                polygon.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(polygon);

            return this;
        }

        /// <summary>
        /// Draw an arbitrary polygon
        /// </summary>
        /// <param name="pointsList"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawPolygon(params ITuple2D[] pointsList)
        {
            var polygon = SvgElementPolygon
                .Create()
                .SetPoints(pointsList);

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                polygon.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(polygon);

            return this;
        }

        /// <summary>
        /// Draw an arbitrary polyline
        /// </summary>
        /// <param name="pointsList"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawPolyline(IEnumerable<ITuple2D> pointsList)
        {
            var polygon = SvgElementPolyline
                .Create()
                .SetPoints(pointsList);

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                polygon.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(polygon);

            return this;
        }

        /// <summary>
        /// Draw an arbitrary polyline
        /// </summary>
        /// <param name="pointsList"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawPolyline(params ITuple2D[] pointsList)
        {
            var polylineElement = SvgElementPolyline
                .Create()
                .SetPoints(pointsList);

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                polylineElement.Style.UpdateFrom(CurrentStyle, true, true, false);

            _contentsList.Append(polylineElement);

            return this;
        }


        /// <summary>
        /// Draw a circle marker
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="pixelsRadius"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawCircleMarker(double centerX, double centerY, int pixelsRadius)
        {
            return DrawCircle(
                centerX, 
                centerY, 
                pixelsRadius * ParentDrawingBoard.LengthByPixelsRatio
            );
        }
        
        /// <summary>
        /// Draw a circle marker
        /// </summary>
        /// <param name="center"></param>
        /// <param name="pixelsRadius"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawCircleMarker(ITuple2D center, int pixelsRadius)
        {
            return DrawCircle(
                center.X, 
                center.Y, 
                pixelsRadius * ParentDrawingBoard.LengthByPixelsRatio
            );
        }

        /// <summary>
        /// Draw a circle marker
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="pixelsRadius"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawSquareMarker(double centerX, double centerY, int pixelsRadius)
        {
            return DrawSquare(
                centerX,
                centerY,
                pixelsRadius * ParentDrawingBoard.LengthByPixelsRatio
            );
        }

        /// <summary>
        /// Draw a regular polygon marker
        /// </summary>
        /// <param name="sidesCount"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="pixelsRadius"></param>
        /// <param name="offsetAngle"></param>
        /// <returns></returns>
        public SvgDrawingBoardLayer DrawPolygonMarker(int sidesCount, double centerX, double centerY, int pixelsRadius, double offsetAngle = 0)
        {
            return DrawPolygon(
                sidesCount, 
                centerX, 
                centerY,
                pixelsRadius * ParentDrawingBoard.LengthByPixelsRatio, 
                offsetAngle
            );
        }


        public SvgDrawingBoardLayer DrawText(string text, double x, double y)
        {
            var textElement = SvgElementText
                .Create()
                .X.SetTo(x)
                .Y.SetTo(y);

            textElement.Contents.Append(text.ToSvgContentText());

            _contentsList.Append(textElement);

            //TODO: Set style here
            textElement.Transform.SetTo(
                SvgTransformComposer.Create(
                    SvgTransformTranslate.Create(0, y),
                    SvgTransformScale.Create(1, -1),
                    SvgTransformTranslate.Create(0, -y)
                )
            );

            if (ActiveStyle == SvgDrawingBoardLayerActiveStyle.Current)
                textElement.Style.UpdateFrom(CurrentStyle, true, true, true);

            textElement
                .Style
                .Font.SetTo("Verdana", "Helvetica", "Arial", "sans-serif")
                .FontSize.SetTo(32 * ParentDrawingBoard.LengthByPixelsRatio);

            return this;
        }
    }
}