using System.Collections;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GraphicsComposerLib.Rendering.Xeogl.Cameras;
using GraphicsComposerLib.Rendering.Xeogl.Geometry;
using GraphicsComposerLib.Rendering.Xeogl.Lights;
using GraphicsComposerLib.Rendering.Xeogl.Materials;
using GraphicsComposerLib.Rendering.Xeogl.Scenes;
using SixLabors.ImageSharp;
using TextComposerLib.Code.JavaScript;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Xeogl.DrawingSpace
{
    public sealed class XeoglDrawingSpace : 
        IReadOnlyList<XeoglDrawingSpaceLayer>, 
        IJsCodeHtmlPageGenerator
    {
        private readonly List<string> _javascriptIncludes 
            = new List<string>()
            {
                "js/xeogl/xeogl.js",
                "js/xeogl/geometry/vectorTextGeometry.js",
                "js/xeogl/helpers/axisHelper.js"
            };


        private const string HtmlTemplateText1 = @"
<!DOCTYPE html>
<html lang=""en"">
    <head>
        <title>#page-title#</title>
        <meta charset=""utf-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui"">
        
        #javascript-includes#
		
        <link href=""css/styles.css"" rel=""stylesheet""/>
    </head>
    <body>
        <script>
            function init() {
                #xeogl-script#
            };

            window.onload = init;
        </script>
    </body>
</html>
";

        private const string HtmlTemplateText2 = @"
<!DOCTYPE html>
<html>
    <head>
        <title>#page-title#</title>

        #javascript-includes#

        <style>
            body{
                /* set margin to 0 and overflow to hidden, to use the complete page */
                margin: 0;
                overflow: hidden;
            }
        </style>
    </head>
    <body>
        <!-- Div which will hold the Output -->
        <div id = ""WebGL-output"" >
        </ div >

        <!--Javascript code -->
        <script>
            // once everything is loaded, we run our WebGL stuff.
            function init()
            {
                #xeogl-script#
            };

            window.onload = init;
        </script>
    </body>
</html>
";


        public static XeoglDrawingSpace Create()
        {
            return new XeoglDrawingSpace();
        }


        private readonly List<XeoglDrawingSpaceLayer> _layersList
            = new List<XeoglDrawingSpaceLayer>();

        private readonly List<XeoglLight> _lightsList
            = new List<XeoglLight>();

        private readonly Dictionary<string, XeoglGeometry> _geometryDictionary
            = new ADictionary<string, XeoglGeometry>();

        private readonly Dictionary<string, XeoglMaterial> _materialDictionary
            = new ADictionary<string, XeoglMaterial>();


        public XeoglCamera Camera { get; }
            = new XeoglCamera();

        public XeoglDrawingSpaceLayer ActiveLayer { get; private set; }

        public XeoglDrawingSpaceLayer BackLayer
            => _layersList[0];

        public XeoglDrawingSpaceLayer FrontLayer
            => _layersList[^1];

        public IEnumerable<XeoglDrawingSpaceLayer> Layers
            => _layersList;

        public IEnumerable<XeoglDrawingSpaceLayer> DrawableLayers
            => _layersList.Where(r => r.IsVisible && !r.IsEmpty);

        public int Count
            => _layersList.Count;

        public XeoglDrawingSpaceLayer this[int layerIndex]
            => _layersList[layerIndex.Mod(_layersList.Count)];

        public XeoglDrawingSpaceLayer this[string layerName]
            => _layersList.FirstOrDefault(layer => layer.LayerName == layerName);

        public Color AmbientLightColor { get; set; }
            = Color.BlanchedAlmond;

        public double AmbientLightIntensity { get; set; }
            = 0.75;


        public string HtmlTemplateText 
            => HtmlTemplateText1;

        public string HtmlPageTitle { get; set; } 
            = "xeogl";

        public IEnumerable<string> JavaScriptIncludes 
            => _javascriptIncludes;


        public XeoglDrawingSpace()
        {
            Reset();
        }


        public XeoglScene GetXeoglScene()
        {
            var scene = new XeoglScene
            {
                BackgroundColor = AmbientLightColor
            };

            scene.Add(
                DrawableLayers.Select(r => r.GetXeoglObject())
            );

            return scene;
        }

        public string GetJavaScriptCode()
        {
            var composer = new LinearTextComposer();

            //Initialize scene and add light sources
            composer
                .AppendLine("var scene = xeogl.getDefaultScene();")
                .AppendLine("scene.clearLights();")
                .AppendLine();

            var ambientLight = new XeoglAmbientLight()
            {
                LightColor = AmbientLightColor,
                LightIntensity = AmbientLightIntensity
            };

            composer
                .Append(ambientLight.ToString())
                .AppendLine(";")
                .AppendLine();

            foreach (var light in _lightsList)
            {
                composer
                    .Append(light.ToString())
                    .AppendLine(";")
                    .AppendLine();
            }

            //Create variables for stored geometry elements
            foreach (var pair in _geometryDictionary)
            {
                composer
                    .AppendAtNewLine("const ")
                    .Append(pair.Key)
                    .Append("Geometry = ")
                    .Append(pair.Value.ToString())
                    .AppendLine(";")
                    .AppendLine();
            }

            //Create variables for stored material elements
            foreach (var pair in _materialDictionary)
            {
                composer
                    .AppendAtNewLine("const ")
                    .Append(pair.Key)
                    .Append("Material = ")
                    .Append(pair.Value.ToString())
                    .AppendLine(";")
                    .AppendLine();
            }

            //Create a single variable per layer, each layer is either
            //a mesh or a group in the main xeogl scene
            foreach (var layer in DrawableLayers)
            {
                var xeoglObject = layer.GetXeoglObject();

                if (!string.IsNullOrEmpty(layer.LayerDescription))
                    composer
                        .AppendAtNewLine(
                            layer.LayerDescription.PrefixTextLines("//")
                        );

                composer
                    .AppendAtNewLine("const ")
                    .Append(layer.LayerName)
                    .Append("Layer = ")
                    .Append(xeoglObject.ToString())
                    .AppendLine(";")
                    .AppendLine();
            }

            //Add camera code
            composer
                .AppendLineAtNewLine("var camera = scene.camera;")
                .AppendLine()
                .Append(Camera.ToString());

            return composer.ToString();
        }


        /// <summary>
        /// Clear all contents of white board without changing its current
        /// properties
        /// </summary>
        /// <returns></returns>
        public XeoglDrawingSpace Clear()
        {
            _lightsList.Clear();
            _geometryDictionary.Clear();
            _materialDictionary.Clear();

            return ClearLayers();
        }

        /// <summary>
        /// Clear all contents of white board and reset its
        /// properties to their defaults except for its pixels width and height
        /// </summary>
        /// <returns></returns>
        public XeoglDrawingSpace Reset()
        {
            AmbientLightColor = Color.BlanchedAlmond;
            Camera.Reset();

            return Clear();
        }


        public XeoglDrawingSpace ClearLights()
        {
            _lightsList.Clear();

            return this;
        }

        public XeoglDrawingSpace AddLight(XeoglLight light)
        {
            _lightsList.Add(light);

            return this;
        }


        public XeoglDrawingSpace ClearStoredGeometry()
        {
            _geometryDictionary.Clear();

            return this;
        }

        public bool ContainsStoredGeometry(string variableName)
        {
            return _geometryDictionary.ContainsKey(variableName);
        }

        public XeoglGeometry GetStoredGeometry(string variableName)
        {
            _geometryDictionary.TryGetValue(variableName, out var geometry);

            return geometry;
        }

        public XeoglDrawingSpace SetStoredGeometry(string variableName, XeoglGeometry geometry)
        {
            _geometryDictionary.Remove(variableName);

            if (!ReferenceEquals(geometry, null))
                _geometryDictionary.Add(variableName, geometry);

            return this;
        }

        public XeoglDrawingSpace SetStoredGeometry(string variableName, string geometryCode)
        {
            _geometryDictionary.Remove(variableName);

            if (string.IsNullOrEmpty(geometryCode))
                _geometryDictionary.Add(
                    variableName, 
                    XeoglCodeGeometry.Create(geometryCode)
                );

            return this;
        }

        public XeoglDrawingSpace RemoveStoredGeometry(string variableName)
        {
            _geometryDictionary.Remove(variableName);

            return this;
        }


        public XeoglDrawingSpace ClearStoredMaterial()
        {
            _materialDictionary.Clear();

            return this;
        }

        public bool ContainsStoredMaterial(string variableName)
        {
            return _materialDictionary.ContainsKey(variableName);
        }

        public XeoglMaterial GetStoredMaterial(string variableName)
        {
            _materialDictionary.TryGetValue(variableName, out var materials);

            return materials;
        }

        public XeoglDrawingSpace SetStoredMaterial(string variableName, XeoglMaterial materials)
        {
            _materialDictionary.Remove(variableName);

            if (!ReferenceEquals(materials, null))
                _materialDictionary.Add(variableName, materials);

            return this;
        }

        public XeoglDrawingSpace SetStoredMaterial(string variableName, string materialsCode)
        {
            _materialDictionary.Remove(variableName);

            if (string.IsNullOrEmpty(materialsCode))
                _materialDictionary.Add(
                    variableName, 
                    XeoglCodeMaterial.Create(materialsCode)
                );

            return this;
        }

        public XeoglDrawingSpace RemoveStoredMaterial(string variableName)
        {
            _materialDictionary.Remove(variableName);

            return this;
        }


        public XeoglDrawingSpace ClearLayers()
        {
            _layersList.Clear();

            ActiveLayer = new XeoglDrawingSpaceLayer(this, "layer0");
            _layersList.Add(ActiveLayer);

            return this;
        }

        public XeoglDrawingSpaceLayer AddFrontLayer(string layerName, bool setAsActiveLayer = true)
        {
            var layer = new XeoglDrawingSpaceLayer(this, layerName);

            _layersList.Add(layer);

            if (setAsActiveLayer)
                ActiveLayer = layer;

            return layer;
        }

        public XeoglDrawingSpace AddFrontLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Add(
                    new XeoglDrawingSpaceLayer(this, layerName)
                );

            return this;
        }

        public XeoglDrawingSpace AddFrontLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Add(
                    new XeoglDrawingSpaceLayer(this, layerName)
                );

            return this;
        }

        public XeoglDrawingSpaceLayer AddBackLayer(string layerName, bool setAsActiveLayer = true)
        {
            var layer = new XeoglDrawingSpaceLayer(this, layerName);

            _layersList.Insert(0, layer);

            if (setAsActiveLayer)
                ActiveLayer = layer;

            return layer;
        }

        public XeoglDrawingSpace AddBackLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Insert(
                    0,
                    new XeoglDrawingSpaceLayer(this, layerName)
                );

            return this;
        }

        public XeoglDrawingSpace AddBackLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                _layersList.Insert(
                    0,
                    new XeoglDrawingSpaceLayer(this, layerName)
                );

            return this;
        }

        public XeoglDrawingSpaceLayer AddLayer(int layerIndex, string layerName, bool setAsActiveLayer = true)
        {
            var layer = new XeoglDrawingSpaceLayer(this, layerName);

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


        public XeoglDrawingSpace RemoveEmptyLayers()
        {
            for (var i = _layersList.Count - 1; i >= 0; i++)
                if (_layersList[i].IsEmpty)
                    RemoveLayer(i);

            return this;
        }

        public XeoglDrawingSpace RemoveLayer(int layerIndex)
        {
            if (ReferenceEquals(ActiveLayer, _layersList[layerIndex]))
                ActiveLayer = null;

            _layersList.RemoveAt(layerIndex);

            //Make sure there is at least one layer in this drawing board
            if (_layersList.Count == 0)
            {
                ActiveLayer = new XeoglDrawingSpaceLayer(this, "Layer 0");
                _layersList.Add(ActiveLayer);
            }

            //Make sure the active layer is not null
            if (ReferenceEquals(ActiveLayer, null))
                ActiveLayer = _layersList[0];

            return this;
        }

        public XeoglDrawingSpace RemoveLayer(string layerName)
        {
            return RemoveLayer(GetLayerIndex(layerName));
        }

        public XeoglDrawingSpace RemoveLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                RemoveLayer(GetLayerIndex(layerName));

            return this;
        }

        public XeoglDrawingSpace RemoveLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                RemoveLayer(GetLayerIndex(layerName));

            return this;
        }


        public XeoglDrawingSpace ShowLayer(int layerIndex)
        {
            _layersList[layerIndex].IsVisible = true;

            return this;
        }

        public XeoglDrawingSpace ShowLayer(string layerName)
        {
            return ShowLayer(GetLayerIndex(layerName));
        }

        public XeoglDrawingSpace ShowLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                ShowLayer(GetLayerIndex(layerName));

            return this;
        }

        public XeoglDrawingSpace ShowLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                ShowLayer(GetLayerIndex(layerName));

            return this;
        }


        public XeoglDrawingSpace HideLayer(int layerIndex)
        {
            _layersList[layerIndex].IsVisible = false;

            return this;
        }

        public XeoglDrawingSpace HideLayer(string layerName)
        {
            return HideLayer(GetLayerIndex(layerName));
        }

        public XeoglDrawingSpace HideLayers(params string[] layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                HideLayer(GetLayerIndex(layerName));

            return this;
        }

        public XeoglDrawingSpace HideLayers(IEnumerable<string> layerNamesList)
        {
            foreach (var layerName in layerNamesList)
                HideLayer(GetLayerIndex(layerName));

            return this;
        }


        public int GetLayerIndex(string layerName)
        {
            return _layersList.FindIndex(layer => layer.LayerName == layerName);
        }


        public XeoglDrawingSpaceLayer SetActiveLayer(int layerIndex)
        {
            ActiveLayer = _layersList[layerIndex.Mod(_layersList.Count)];

            return ActiveLayer;
        }

        public XeoglDrawingSpaceLayer SetActiveLayer(string layerName)
        {
            var layer = this[layerName];

            if (!ReferenceEquals(layer, null))
                ActiveLayer = layer;

            return ActiveLayer;
        }


        public XeoglDrawingSpace SwapLayers(int layerIndex1, int layerIndex2)
        {
            _layersList.SwapItems(
                layerIndex1.Mod(_layersList.Count),
                layerIndex2.Mod(_layersList.Count)
            );

            return this;
        }

        public XeoglDrawingSpace SetLayerIndex(int oldLayerIndex, int newLayerIndex)
        {
            _layersList.SetItemIndex(
                oldLayerIndex.Mod(_layersList.Count),
                newLayerIndex
            );

            return this;
        }

        public XeoglDrawingSpace SetLayerAsBack(int oldLayerIndex)
        {
            _layersList.SetItemFirst(
                oldLayerIndex.Mod(_layersList.Count)
            );

            return this;
        }

        public XeoglDrawingSpace SetLayerAsFront(int oldLayerIndex)
        {
            _layersList.SetItemLast(
                oldLayerIndex.Mod(_layersList.Count)
            );

            return this;
        }


        public IEnumerator<XeoglDrawingSpaceLayer> GetEnumerator()
        {
            return _layersList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _layersList.GetEnumerator();
        }

        public override string ToString()
        {
            return GetJavaScriptCode();
        }
    }
}
