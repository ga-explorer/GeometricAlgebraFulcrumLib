using System.Collections;
using DataStructuresLib.Basic;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using WebComposerLib.Html.Media;
using WebComposerLib.Svg;
using WebComposerLib.Svg.Elements.Containers;
using WebComposerLib.Svg.Elements.Shape;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Svg.DrawingBoard
{
    public sealed class SvgDrawingBoard : IReadOnlyList<SvgDrawingBoardLayer>
    {
        public static SvgDrawingBoard Create(int pixelsWidth, int pixelsHeight)
        {
            return new SvgDrawingBoard(pixelsWidth, pixelsHeight);
        }

        public static SvgDrawingBoard Create(int pixelsWidth, int pixelsHeight, double viewBoxWidth)
        {
            return new SvgDrawingBoard(pixelsWidth, pixelsHeight)
                .SetViewBox(
                    0,
                    0,
                    viewBoxWidth
                );
        }

        public static SvgDrawingBoard Create(int pixelsWidth, int pixelsHeight, double viewBoxMidX, double viewBoxMidY, double viewBoxWidth)
        {
            return new SvgDrawingBoard(pixelsWidth, pixelsHeight)
                .SetViewBox(
                    viewBoxMidX,
                    viewBoxMidY,
                    viewBoxWidth
                );
        }

        public static SvgDrawingBoard Create(IBoundingBox2D viewBox, double pixelsByLengthRatio)
        {
            var pixelsWidth = (int)(viewBox.GetLengthX() * pixelsByLengthRatio);
            var pixelsHeight = (int)(viewBox.GetLengthY() * pixelsByLengthRatio);
            var midPoint = viewBox.GetMidPoint();

            return new SvgDrawingBoard(pixelsWidth, pixelsHeight)
                .SetViewBox(
                    midPoint.X,
                    midPoint.Y,
                    viewBox.GetLengthX()
                );
        }


        private readonly List<SvgDrawingBoardLayer> _layersList
            = new List<SvgDrawingBoardLayer>();


        public SvgDrawingBoardLayer ActiveLayer { get; private set; }

        public SvgDrawingBoardLayer BackLayer
            => _layersList[0];

        public SvgDrawingBoardLayer FrontLayer
            => _layersList[^1];

        public IEnumerable<SvgDrawingBoardLayer> Layers
            => _layersList;

        public int Count
            => _layersList.Count;

        public SvgDrawingBoardLayer this[int layerIndex]
            => _layersList[layerIndex.Mod(_layersList.Count)];

        public SvgDrawingBoardLayer? this[string layerName]
            => _layersList.FirstOrDefault(layer => layer.LayerName == layerName);


        /// <summary>
        /// The ratio between the whiteboard width\height in pixels to
        /// the user defined view box width\height
        /// </summary>
        public double PixelsByLengthRatio { get; private set; } = 1.0d;

        /// <summary>
        /// The ratio between user defined view box width\height to
        /// the whiteboard width\height in pixels
        /// </summary>
        public double LengthByPixelsRatio { get; private set; } = 1.0d;

        public bool DrawBackgroundRect { get; set; } = true;

        public Color BackgroundRectColor { get; set; }
            = Color.BlanchedAlmond;

        public int BackgroundRectBorderWidth { get; set; }
            = 2;


        /// <summary>
        /// Drawing board width in pixels
        /// </summary>
        public int PixelsWidth { get; }

        /// <summary>
        /// Drawing board height in pixels
        /// </summary>
        public int PixelsHeight { get; }

        /// <summary>
        /// User defined drawing board view box width
        /// </summary>
        public double ViewBoxWidth { get; private set; }

        /// <summary>
        /// User defined drawing board view box height
        /// </summary>
        public double ViewBoxHeight { get; private set; }

        /// <summary>
        /// User defined drawing board view box min X value
        /// </summary>
        public double ViewBoxMinX { get; private set; }

        /// <summary>
        /// User defined drawing board view box min Y value
        /// </summary>
        public double ViewBoxMinY { get; private set; }

        /// <summary>
        /// User defined drawing board view box max X value
        /// </summary>
        public double ViewBoxMaxX
            => ViewBoxMinX + ViewBoxWidth;

        /// <summary>
        /// User defined drawing board view box max Y value
        /// </summary>
        public double ViewBoxMaxY
            => ViewBoxMinY + ViewBoxHeight;

        /// <summary>
        /// User defined drawing board view box mid X value
        /// </summary>
        public double ViewBoxMidX
            => ViewBoxMinX + ViewBoxWidth / 2;

        /// <summary>
        /// User defined drawing board view box max Y value
        /// </summary>
        public double ViewBoxMidY
            => ViewBoxMinY + ViewBoxHeight / 2;


        /// <summary>
        /// The current width in pixels of the pen used to draw the upcoming
        /// shapes in the active layer
        /// </summary>
        public int PenPixelsWidth
        {
            get => ActiveLayer.CurrentStyle.PenPixelsWidth;
            set => ActiveLayer.CurrentStyle.PenPixelsWidth = value;
        }

        /// <summary>
        /// The current color of the pen used to draw the upcoming shapes
        /// in the active layer
        /// </summary>
        public Color PenColor
        {
            get => ActiveLayer.CurrentStyle.PenColor;
            set => ActiveLayer.CurrentStyle.PenColor = value;
        }

        /// <summary>
        /// The current opacity of the pen used to draw the upcoming shapes
        /// in the active layer
        /// </summary>
        public double PenOpacity
        {
            get => ActiveLayer.CurrentStyle.PenOpacity;
            set => ActiveLayer.CurrentStyle.PenOpacity = value;
        }

        /// <summary>
        /// The current dash pattern of the pen used to draw the upcoming shapes
        /// in the active layer
        /// </summary>
        public string PenDashPattern
            => ActiveLayer.CurrentStyle.PenDashPattern;

        /// <summary>
        /// The current fill color of the pen used to draw the upcoming shapes
        /// in the active layer
        /// </summary>
        public Color FillColor
        {
            get => ActiveLayer.CurrentStyle.FillColor;
            set => ActiveLayer.CurrentStyle.FillColor = value;
        }

        /// <summary>
        /// The current fill opacity of the pen used to draw the upcoming shapes
        /// in the active layer
        /// </summary>
        public double FillOpacity
        {
            get => ActiveLayer.CurrentStyle.FillOpacity;
            set => ActiveLayer.CurrentStyle.FillOpacity = value;
        }


        private SvgDrawingBoard(int pixelsWidth, int pixelsHeight)
        {
            PixelsWidth = pixelsWidth;
            PixelsHeight = pixelsHeight;

            Reset();
        }


        public SvgElementSvg GetSvgElement()
        {
            var svgElement = SvgElementSvg.CreateRoot("White Board");

            svgElement
                .SetCanvasSize(PixelsWidth, PixelsHeight)
                .ViewBox
                .SetTo(ViewBoxMinX, ViewBoxMinY, ViewBoxWidth, ViewBoxHeight);

            if (DrawBackgroundRect)
            {
                var backgroundRect =
                    SvgElementRectangle
                        .Create()
                        .SetRectangle(ViewBoxMinX, ViewBoxMinY, ViewBoxWidth, ViewBoxHeight);

                backgroundRect
                    .Style
                    .Fill.SetToRgb(BackgroundRectColor)
                    .StrokeWidth.SetTo(BackgroundRectBorderWidth * LengthByPixelsRatio)
                    .Stroke.SetToRgb(Color.Black);

                svgElement.Contents.Append(backgroundRect);
            }

            foreach (var layer in _layersList.Where(layer => layer.IsVisible))
                svgElement.Contents.Append(layer.GetSvgElement());

            return svgElement;
        }

        public string GetSvgCode()
        {
            var svgElement = GetSvgElement();

            var composer = new WclSvgComposer();

            composer.AppendSvgFileHeader();
            composer.AppendTag(svgElement);

            return composer.ToString();
        }

        /// <summary>
        /// Clear all contents of white board without changing its current
        /// properties
        /// </summary>
        /// <returns></returns>
        public SvgDrawingBoard Clear()
        {
            return ClearLayers();
        }

        /// <summary>
        /// Clear all contents of white board and reset its
        /// properties to their defaults except for its pixels width and height
        /// </summary>
        /// <returns></returns>
        public SvgDrawingBoard Reset()
        {
            BackgroundRectColor = Color.BlanchedAlmond;

            SetViewBox(0, 0, PixelsWidth);

            return Clear();
        }

        /// <summary>
        /// Convert the given user defined length to pixels using the value of
        /// the white board's PixelsByLengthRatio property
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public double LengthToPixels(double length)
        {
            return length * PixelsByLengthRatio;
        }

        /// <summary>
        /// Convert the given pixels to user defined length using the value of
        /// the white board's LengthByPixelsRatio property
        /// </summary>
        /// <param name="pixels"></param>
        /// <returns></returns>
        public double PixelsToLength(double pixels)
        {
            return pixels * LengthByPixelsRatio;
        }


        /// <summary>
        /// Set the view box user defined middle point and width of drawing board
        /// </summary>
        /// <param name="midX"></param>
        /// <param name="midY"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public SvgDrawingBoard SetViewBox(double midX, double midY, double width)
        {
            PixelsByLengthRatio = PixelsWidth / width;
            LengthByPixelsRatio = 1.0d / PixelsByLengthRatio;

            ViewBoxWidth = width;
            ViewBoxHeight = PixelsHeight * LengthByPixelsRatio;

            ViewBoxMinX = midX - ViewBoxWidth / 2;
            ViewBoxMinY = midY - ViewBoxHeight / 2;

            return this;
        }

        public bool IsPointVisible(double x, double y)
        {
            return ViewBoxMinX <= x && x <= ViewBoxMaxX &&
                   ViewBoxMinY <= y && y <= ViewBoxMaxY;
        }

        public BoundingBox2D GetViewBox()
        {
            return BoundingBox2D.Create(
                ViewBoxMinX,
                ViewBoxMinY,
                ViewBoxMaxX,
                ViewBoxMaxY
            );
        }


        public SvgDrawingBoard ClearLayers()
        {
            _layersList.Clear();

            ActiveLayer = new SvgDrawingBoardLayer(this, "Layer 0");
            _layersList.Add(ActiveLayer);

            return this;
        }


        public SvgDrawingBoardLayer AddFrontLayer(string layerName, bool setAsActiveLayer = true)
        {
            var layer = new SvgDrawingBoardLayer(this, layerName);

            _layersList.Add(layer);

            if (setAsActiveLayer)
                ActiveLayer = layer;

            return layer;
        }

        public SvgDrawingBoard AddFrontLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Add(
                    new SvgDrawingBoardLayer(this, layerName)
                );

            return this;
        }

        public SvgDrawingBoard AddFrontLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Add(
                    new SvgDrawingBoardLayer(this, layerName)
                );

            return this;
        }

        public SvgDrawingBoardLayer AddBackLayer(string layerName, bool setAsActiveLayer = true)
        {
            var layer = new SvgDrawingBoardLayer(this, layerName);

            _layersList.Insert(0, layer);

            if (setAsActiveLayer)
                ActiveLayer = layer;

            return layer;
        }

        public SvgDrawingBoard AddBackLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Insert(
                    0,
                    new SvgDrawingBoardLayer(this, layerName)
                );

            return this;
        }

        public SvgDrawingBoard AddBackLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Insert(
                    0,
                    new SvgDrawingBoardLayer(this, layerName)
                );

            return this;
        }

        public SvgDrawingBoardLayer AddLayer(int layerIndex, string layerName, bool setAsActiveLayer = true)
        {
            var layer = new SvgDrawingBoardLayer(this, layerName);

            if (layerIndex < 0)
                _layersList.Insert(0, layer);

            else if (layerIndex >= _layersList.Count)
                _layersList.Add(layer);

            else
                _layersList.Insert(layerIndex, layer);

            if (setAsActiveLayer)
                ActiveLayer = layer;

            return layer;
        }


        public SvgDrawingBoard RemoveEmptyLayers()
        {
            for (var i = _layersList.Count - 1; i >= 0; i++)
                if (_layersList[i].IsEmpty)
                    RemoveLayer(i);

            return this;
        }

        public SvgDrawingBoard RemoveLayer(int layerIndex)
        {
            if (ReferenceEquals(ActiveLayer, _layersList[layerIndex]))
                ActiveLayer = null;

            _layersList.RemoveAt(layerIndex);

            //Make sure there is at least one layer in this drawing board
            if (_layersList.Count == 0)
            {
                ActiveLayer = new SvgDrawingBoardLayer(this, "Layer 0");
                _layersList.Add(ActiveLayer);
            }

            //Make sure the active layer is not null
            if (ReferenceEquals(ActiveLayer, null))
                ActiveLayer = _layersList[0];

            return this;
        }

        public SvgDrawingBoard RemoveLayer(string layerName)
        {
            return RemoveLayer(GetLayerIndex(layerName));
        }

        public SvgDrawingBoard RemoveLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                RemoveLayer(GetLayerIndex(layerName));

            return this;
        }

        public SvgDrawingBoard RemoveLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                RemoveLayer(GetLayerIndex(layerName));

            return this;
        }


        public SvgDrawingBoard ShowLayer(int layerIndex)
        {
            _layersList[layerIndex].IsVisible = true;

            return this;
        }

        public SvgDrawingBoard ShowLayer(string layerName)
        {
            return ShowLayer(GetLayerIndex(layerName));
        }

        public SvgDrawingBoard ShowLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                ShowLayer(GetLayerIndex(layerName));

            return this;
        }

        public SvgDrawingBoard ShowLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                ShowLayer(GetLayerIndex(layerName));

            return this;
        }


        public SvgDrawingBoard HideLayer(int layerIndex)
        {
            _layersList[layerIndex].IsVisible = false;

            return this;
        }

        public SvgDrawingBoard HideLayer(string layerName)
        {
            return HideLayer(GetLayerIndex(layerName));
        }

        public SvgDrawingBoard HideLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                HideLayer(GetLayerIndex(layerName));

            return this;
        }

        public SvgDrawingBoard HideLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                HideLayer(GetLayerIndex(layerName));

            return this;
        }


        public int GetLayerIndex(string layerName)
        {
            return _layersList.FindIndex(layer => layer.LayerName == layerName);
        }


        public SvgDrawingBoard SetActiveLayer(int layerIndex)
        {
            ActiveLayer = _layersList[layerIndex.Mod(_layersList.Count)];

            return this;
        }

        public SvgDrawingBoard SetActiveLayer(string layerName)
        {
            var layer = this[layerName];

            if (!ReferenceEquals(layer, null))
                ActiveLayer = layer;

            return this;
        }

        public SvgDrawingBoard SwapLayers(int layerIndex1, int layerIndex2)
        {
            _layersList.SwapItems(
                layerIndex1.Mod(_layersList.Count),
                layerIndex2.Mod(_layersList.Count)
            );

            return this;
        }

        public SvgDrawingBoard SetLayerIndex(int oldLayerIndex, int newLayerIndex)
        {
            _layersList.SetItemIndex(
                oldLayerIndex.Mod(_layersList.Count),
                newLayerIndex
            );

            return this;
        }

        public SvgDrawingBoard SetLayerAsBack(int oldLayerIndex)
        {
            _layersList.SetItemFirst(
                oldLayerIndex.Mod(_layersList.Count)
            );

            return this;
        }

        public SvgDrawingBoard SetLayerAsFront(int oldLayerIndex)
        {
            _layersList.SetItemLast(
                oldLayerIndex.Mod(_layersList.Count)
            );

            return this;
        }


        public Image RenderToPng()
        {
            var svgCode = GetSvgCode();

            return svgCode.SvgCodeToPngImage();
        }

        /// <summary>
        /// Save SVG code to file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public SvgDrawingBoard SaveToSvgFile(string filePath)
        {
            File.WriteAllText(filePath, GetSvgCode());

            return this;
        }

        public SvgDrawingBoard SaveToPngFile(string pngFilePath)
        {
            var svgCode = GetSvgCode();

            svgCode.SvgCodeToPngFile(pngFilePath);

            //var svgDocument = SvgDocument.FromSvg<SvgDocument>(GetSvgCode());

            //using (var bitmap = svgDocument.Draw(PixelsWidth, PixelsHeight))
            //{
            //    //bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //    bitmap.Save(pngFilePath, ImageFormat.Png);
            //}

            return this;
        }


        public IEnumerator<SvgDrawingBoardLayer> GetEnumerator()
        {
            return _layersList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _layersList.GetEnumerator();
        }

        public override string ToString()
        {
            return GetSvgCode();
        }
    }
}
