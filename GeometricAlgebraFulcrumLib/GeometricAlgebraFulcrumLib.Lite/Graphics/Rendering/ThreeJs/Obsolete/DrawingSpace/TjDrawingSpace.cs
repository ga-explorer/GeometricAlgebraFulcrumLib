namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.DrawingSpace;

//    public sealed class TjDrawingSpace : 
//        IReadOnlyList<TjDrawingSpaceLayer>, 
//        IJsCodeHtmlPageGenerator
//    {
//        public static TjDrawingSpace Create()
//        {
//            return new TjDrawingSpace();
//        }


//        private readonly List<string> _javascriptIncludes 
//            = new List<string>()
//            {
//                "js/xeogl/xeogl.js",
//                "js/xeogl/geometry/vectorTextGeometry.js",
//                "js/xeogl/helpers/axisHelper.js"
//            };

//        private const string HtmlTemplateText1 = @"
//<!DOCTYPE html>
//<html lang=""en"">
//    <head>
//        <title>#page-title#</title>
//        <meta charset=""utf-8"">
//        <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui"">
        
//        #javascript-includes#
		
//        <link href=""css/styles.css"" rel=""stylesheet""/>
//    </head>
//    <body>
//        <script>
//            function init() {
//                #xeogl-script#
//            };

//            window.onload = init;
//        </script>
//    </body>
//</html>
//";

//        private const string HtmlTemplateText2 = @"
//<!DOCTYPE html>
//<html>
//    <head>
//        <title>#page-title#</title>

//        #javascript-includes#

//        <style>
//            body{
//                /* set margin to 0 and overflow to hidden, to use the complete page */
//                margin: 0;
//                overflow: hidden;
//            }
//        </style>
//    </head>
//    <body>
//        <!-- Div which will hold the Output -->
//        <div id = ""WebGL-output"" >
//        </ div >

//        <!--Javascript code -->
//        <script>
//            // once everything is loaded, we run our WebGL stuff.
//            function init()
//            {
//                #xeogl-script#
//            };

//            window.onload = init;
//        </script>
//    </body>
//</html>
//";

//        private readonly List<TjDrawingSpaceLayer> _layersList
//            = new List<TjDrawingSpaceLayer>();

//        private readonly List<TjLight> _lightsList
//            = new List<TjLight>();

//        private readonly Dictionary<string, TjGeometry> _geometryDictionary
//            = new ADictionary<string, TjGeometry>();

//        private readonly Dictionary<string, TjMaterial> _materialDictionary
//            = new ADictionary<string, TjMaterial>();


//        public TjCamera Camera { get; }
//            = new TjCamera();

//        public TjDrawingSpaceLayer ActiveLayer { get; private set; }

//        public TjDrawingSpaceLayer BackLayer
//            => _layersList[0];

//        public TjDrawingSpaceLayer FrontLayer
//            => _layersList[^1];

//        public IEnumerable<TjDrawingSpaceLayer> Layers
//            => _layersList;

//        public IEnumerable<TjDrawingSpaceLayer> DrawableLayers
//            => _layersList.Where(r => r.IsVisible && !r.IsEmpty);

//        public int Count
//            => _layersList.Count;

//        public TjDrawingSpaceLayer this[int layerIndex]
//            => _layersList[layerIndex.Mod(_layersList.Count)];

//        public TjDrawingSpaceLayer this[string layerName]
//            => _layersList.FirstOrDefault(layer => layer.LayerName == layerName);

//        public Color AmbientLightColor { get; set; }
//            = Color.BlanchedAlmond;

//        public double AmbientLightIntensity { get; set; }
//            = 0.75;


//        public string HtmlTemplateText 
//            => HtmlTemplateText1;

//        public string HtmlPageTitle { get; set; } 
//            = "Three.js Page";

//        public IEnumerable<string> JavaScriptIncludes 
//            => _javascriptIncludes;


//        private TjDrawingSpace()
//        {
//            Reset();
//        }


//        public TjScene GetTjScene()
//        {
//            var scene = new TjScene
//            {
//                Background = AmbientLightColor
//            };

//            scene.Add(
//                DrawableLayers.Select(r => r.GetTjObject())
//            );

//            return scene;
//        }

//        public string GetJavaScriptCode()
//        {
//            var composer = new LinearTextComposer();

//            //Initialize scene and add light sources
//            composer
//                .AppendLine("var scene = xeogl.getDefaultScene();")
//                .AppendLine("scene.clearLights();")
//                .AppendLine();

//            var ambientLight = new TjAmbientLight()
//            {
//                LightColor = AmbientLightColor,
//                LightIntensity = AmbientLightIntensity
//            };

//            composer
//                .Append(ambientLight.ToString())
//                .AppendLine(";")
//                .AppendLine();

//            foreach (var light in _lightsList)
//            {
//                composer
//                    .Append(light.ToString())
//                    .AppendLine(";")
//                    .AppendLine();
//            }

//            //Create variables for stored geometry elements
//            foreach (var pair in _geometryDictionary)
//            {
//                composer
//                    .AppendAtNewLine("const ")
//                    .Append(pair.Key)
//                    .Append("Geometry = ")
//                    .Append(pair.Value.ToString())
//                    .AppendLine(";")
//                    .AppendLine();
//            }

//            //Create variables for stored material elements
//            foreach (var pair in _materialDictionary)
//            {
//                composer
//                    .AppendAtNewLine("const ")
//                    .Append(pair.Key)
//                    .Append("Material = ")
//                    .Append(pair.Value.ToString())
//                    .AppendLine(";")
//                    .AppendLine();
//            }

//            //Create a single variable per layer, each layer is either
//            //a mesh or a group in the main scene
//            foreach (var layer in DrawableLayers)
//            {
//                var xeoglObject = layer.GetTjObject();

//                if (!string.IsNullOrEmpty(layer.LayerDescription))
//                    composer
//                        .AppendAtNewLine(
//                            layer.LayerDescription.PrefixTextLines("//")
//                        );

//                composer
//                    .AppendAtNewLine("const ")
//                    .Append(layer.LayerName)
//                    .Append("Layer = ")
//                    .Append(xeoglObject.ToString())
//                    .AppendLine(";")
//                    .AppendLine();
//            }

//            //Add camera code
//            composer
//                .AppendLineAtNewLine("var camera = scene.camera;")
//                .AppendLine()
//                .Append(Camera.ToString());

//            return composer.ToString();
//        }


//        /// <summary>
//        /// Clear all contents of drawing space without changing its current
//        /// properties
//        /// </summary>
//        /// <returns></returns>
//        public TjDrawingSpace Clear()
//        {
//            _lightsList.Clear();
//            _geometryDictionary.Clear();
//            _materialDictionary.Clear();

//            return ClearLayers();
//        }

//        /// <summary>
//        /// Clear all contents of white board and reset its
//        /// properties to their defaults except for its pixels width and height
//        /// </summary>
//        /// <returns></returns>
//        public TjDrawingSpace Reset()
//        {
//            AmbientLightColor = Color.BlanchedAlmond;
//            Camera.Reset();

//            return Clear();
//        }


//        public TjDrawingSpace ClearLights()
//        {
//            _lightsList.Clear();

//            return this;
//        }

//        public TjDrawingSpace AddLight(TjLight light)
//        {
//            _lightsList.Add(light);

//            return this;
//        }


//        public TjDrawingSpace ClearStoredGeometry()
//        {
//            _geometryDictionary.Clear();

//            return this;
//        }

//        public bool ContainsStoredGeometry(string variableName)
//        {
//            return _geometryDictionary.ContainsKey(variableName);
//        }

//        public TjGeometry GetStoredGeometry(string variableName)
//        {
//            _geometryDictionary.TryGetValue(variableName, out var geometry);

//            return geometry;
//        }

//        public TjDrawingSpace SetStoredGeometry(string variableName, TjGeometry geometry)
//        {
//            _geometryDictionary.Remove(variableName);

//            if (geometry is not null)
//                _geometryDictionary.Add(variableName, geometry);

//            return this;
//        }

//        public TjDrawingSpace SetStoredGeometry(string variableName, string geometryCode)
//        {
//            _geometryDictionary.Remove(variableName);

//            if (string.IsNullOrEmpty(geometryCode))
//                _geometryDictionary.Add(
//                    variableName, 
//                    TjCodeGeometry.Create(geometryCode)
//                );

//            return this;
//        }

//        public TjDrawingSpace RemoveStoredGeometry(string variableName)
//        {
//            _geometryDictionary.Remove(variableName);

//            return this;
//        }


//        public TjDrawingSpace ClearStoredMaterial()
//        {
//            _materialDictionary.Clear();

//            return this;
//        }

//        public bool ContainsStoredMaterial(string variableName)
//        {
//            return _materialDictionary.ContainsKey(variableName);
//        }

//        public TjMaterial GetStoredMaterial(string variableName)
//        {
//            _materialDictionary.TryGetValue(variableName, out var material);

//            return material;
//        }

//        public TjDrawingSpace SetStoredMaterial(string variableName, TjMaterial materials)
//        {
//            _materialDictionary.Remove(variableName);

//            if (!ReferenceEquals(materials, null))
//                _materialDictionary.Add(variableName, materials);

//            return this;
//        }

//        public TjDrawingSpace SetStoredMaterial(string variableName, string materialsCode)
//        {
//            _materialDictionary.Remove(variableName);

//            if (string.IsNullOrEmpty(materialsCode))
//                _materialDictionary.Add(
//                    variableName, 
//                    TjCodeMaterial.Create(materialsCode)
//                );

//            return this;
//        }

//        public TjDrawingSpace RemoveStoredMaterial(string variableName)
//        {
//            _materialDictionary.Remove(variableName);

//            return this;
//        }


//        public TjDrawingSpace ClearLayers()
//        {
//            _layersList.Clear();

//            ActiveLayer = new TjDrawingSpaceLayer(this, "layer0");
//            _layersList.Add(ActiveLayer);

//            return this;
//        }

//        public TjDrawingSpaceLayer AddFrontLayer(string layerName, bool setAsActiveLayer = true)
//        {
//            var layer = new TjDrawingSpaceLayer(this, layerName);

//            _layersList.Add(layer);

//            if (setAsActiveLayer)
//                ActiveLayer = layer;

//            return layer;
//        }

//        public TjDrawingSpace AddFrontLayers(params string[] layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                _layersList.Add(
//                    new TjDrawingSpaceLayer(this, layerName)
//                );

//            return this;
//        }

//        public TjDrawingSpace AddFrontLayers(IEnumerable<string> layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                _layersList.Add(
//                    new TjDrawingSpaceLayer(this, layerName)
//                );

//            return this;
//        }

//        public TjDrawingSpaceLayer AddBackLayer(string layerName, bool setAsActiveLayer = true)
//        {
//            var layer = new TjDrawingSpaceLayer(this, layerName);

//            _layersList.Insert(0, layer);

//            if (setAsActiveLayer)
//                ActiveLayer = layer;

//            return layer;
//        }

//        public TjDrawingSpace AddBackLayers(params string[] layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                _layersList.Insert(
//                    0,
//                    new TjDrawingSpaceLayer(this, layerName)
//                );

//            return this;
//        }

//        public TjDrawingSpace AddBackLayers(IEnumerable<string> layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                _layersList.Insert(
//                    0,
//                    new TjDrawingSpaceLayer(this, layerName)
//                );

//            return this;
//        }

//        public TjDrawingSpaceLayer AddLayer(int layerIndex, string layerName, bool setAsActiveLayer = true)
//        {
//            var layer = new TjDrawingSpaceLayer(this, layerName);

//            if (layerIndex < 0)
//                _layersList.Insert(0, layer);

//            else if (layerIndex >= _layersList.Count)
//                _layersList.Add(layer);

//            else
//                _layersList.Insert(layerIndex, layer);

//            if (setAsActiveLayer)
//                ActiveLayer = layer;

//            return layer;
//        }


//        public TjDrawingSpace RemoveEmptyLayers()
//        {
//            for (var i = _layersList.Count - 1; i >= 0; i++)
//                if (_layersList[i].IsEmpty)
//                    RemoveLayer(i);

//            return this;
//        }

//        public TjDrawingSpace RemoveLayer(int layerIndex)
//        {
//            if (ReferenceEquals(ActiveLayer, _layersList[layerIndex]))
//                ActiveLayer = null;

//            _layersList.RemoveAt(layerIndex);

//            //Make sure there is at least one layer in this drawing board
//            if (_layersList.Count == 0)
//            {
//                ActiveLayer = new TjDrawingSpaceLayer(this, "Layer 0");
//                _layersList.Add(ActiveLayer);
//            }

//            //Make sure the active layer is not null
//            if (ReferenceEquals(ActiveLayer, null))
//                ActiveLayer = _layersList[0];

//            return this;
//        }

//        public TjDrawingSpace RemoveLayer(string layerName)
//        {
//            return RemoveLayer(GetLayerIndex(layerName));
//        }

//        public TjDrawingSpace RemoveLayers(params string[] layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                RemoveLayer(GetLayerIndex(layerName));

//            return this;
//        }

//        public TjDrawingSpace RemoveLayers(IEnumerable<string> layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                RemoveLayer(GetLayerIndex(layerName));

//            return this;
//        }


//        public TjDrawingSpace ShowLayer(int layerIndex)
//        {
//            _layersList[layerIndex].IsVisible = true;

//            return this;
//        }

//        public TjDrawingSpace ShowLayer(string layerName)
//        {
//            return ShowLayer(GetLayerIndex(layerName));
//        }

//        public TjDrawingSpace ShowLayers(params string[] layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                ShowLayer(GetLayerIndex(layerName));

//            return this;
//        }

//        public TjDrawingSpace ShowLayers(IEnumerable<string> layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                ShowLayer(GetLayerIndex(layerName));

//            return this;
//        }


//        public TjDrawingSpace HideLayer(int layerIndex)
//        {
//            _layersList[layerIndex].IsVisible = false;

//            return this;
//        }

//        public TjDrawingSpace HideLayer(string layerName)
//        {
//            return HideLayer(GetLayerIndex(layerName));
//        }

//        public TjDrawingSpace HideLayers(params string[] layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                HideLayer(GetLayerIndex(layerName));

//            return this;
//        }

//        public TjDrawingSpace HideLayers(IEnumerable<string> layerNamesList)
//        {
//            foreach (var layerName in layerNamesList)
//                HideLayer(GetLayerIndex(layerName));

//            return this;
//        }


//        public int GetLayerIndex(string layerName)
//        {
//            return _layersList.FindIndex(layer => layer.LayerName == layerName);
//        }


//        public TjDrawingSpaceLayer SetActiveLayer(int layerIndex)
//        {
//            ActiveLayer = _layersList[layerIndex.Mod(_layersList.Count)];

//            return ActiveLayer;
//        }

//        public TjDrawingSpaceLayer SetActiveLayer(string layerName)
//        {
//            var layer = this[layerName];

//            if (!ReferenceEquals(layer, null))
//                ActiveLayer = layer;

//            return ActiveLayer;
//        }


//        public TjDrawingSpace SwapLayers(int layerIndex1, int layerIndex2)
//        {
//            _layersList.SwapItems(
//                layerIndex1.Mod(_layersList.Count),
//                layerIndex2.Mod(_layersList.Count)
//            );

//            return this;
//        }

//        public TjDrawingSpace SetLayerIndex(int oldLayerIndex, int newLayerIndex)
//        {
//            _layersList.SetItemIndex(
//                oldLayerIndex.Mod(_layersList.Count),
//                newLayerIndex
//            );

//            return this;
//        }

//        public TjDrawingSpace SetLayerAsBack(int oldLayerIndex)
//        {
//            _layersList.SetItemFirst(
//                oldLayerIndex.Mod(_layersList.Count)
//            );

//            return this;
//        }

//        public TjDrawingSpace SetLayerAsFront(int oldLayerIndex)
//        {
//            _layersList.SetItemLast(
//                oldLayerIndex.Mod(_layersList.Count)
//            );

//            return this;
//        }


//        public IEnumerator<TjDrawingSpaceLayer> GetEnumerator()
//        {
//            return _layersList.GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return _layersList.GetEnumerator();
//        }

//        public override string ToString()
//        {
//            return GetJavaScriptCode();
//        }
//    }