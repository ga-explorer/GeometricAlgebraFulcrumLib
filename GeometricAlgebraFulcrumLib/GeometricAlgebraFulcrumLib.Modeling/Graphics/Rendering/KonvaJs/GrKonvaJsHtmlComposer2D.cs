//using GraphicsComposerLib.Rendering.KonvaJs.Containers;
//using GeometricAlgebraFulcrumLib.Utilities.Text;
//using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
//using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
//using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;
//using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

//namespace GraphicsComposerLib.Rendering.KonvaJs
//{
//    public class GrKonvaJsHtmlComposer2D
//    {
//        private static readonly ParametricTextComposer KonvajsCodeComposer
//            = new ParametricTextComposer("!#", "#!", @"
//var div = document.getElementById(""renderDiv"");

//!#js-div-size#!

//function renderHtmlToDiv( div, html ) {
//	const ctx = div.getContext( '2d' );

//	const svg = `
//		<svg xmlns=""http://www.w3.org/2000/svg"" width=""${div.width}"" height=""${div.height}"">
//		<foreignObject width=""100%"" height=""100%"">
//			<div xmlns=""http://www.w3.org/1999/xhtml"">${html}</div>
//		</foreignObject>
//		</svg>`;
	
//	const svgBlob = new Blob( [svg], { type: 'image/svg+xml;charset=utf-8' } );
//	const svgObjectUrl = URL.createObjectURL( svgBlob );
	
//	//await new Promise(res => setTimeout(res, 100));

//	const tempImg = new Image();
//	tempImg.addEventListener( 
//		'load', 
//		function() {
//			ctx.drawImage( tempImg, 0, 0 );
//			URL.revokeObjectURL( svgObjectUrl );
//		} 
//	);

//	tempImg.src = svgObjectUrl;
//}

//function startRenderLoop(engine, div) {
//    engine.runRenderLoop(function () {
//        if (scenesToRender) {
//            const scenesToRenderLength = scenesToRender.length;
            
//            for (let i = 0; i < scenesToRenderLength; i++) {
//                if (scenesToRender[i].activeCamera) { scenesToRender[i].render(); }
//            }
//        }
//    });
//}

//var engine = null;
//var scenes = [];
//var scenesToRender = [];

//var createDefaultEngine = function() { 
//    return new BABYLON.Engine(div, true, { preserveDrawingBuffer: true, stencil: true,  disableWebGL2Support: false}); 
//};

//!#create-scenes-code#!
            
//window.initFunction = async function() {
//    var asyncEngineCreation = async function() {
//        try {
//            return createDefaultEngine();
//        } catch(e) {
//            console.log(""the available createEngine function failed. Creating the default engine instead"");
//            return createDefaultEngine();
//        }
//    }

//    window.engine = await asyncEngineCreation();
//    if (!engine) throw 'engine should not be null.';
//    startRenderLoop(engine, div);
    
//    !#add-scenes-code#!
//};

//initFunction().then(() => {
//    scenesToRender = scenes                    
//});

//window.addEventListener(""resize"", function () {
//    engine.resize();
//});
//".Trim()
//            );

//        private static readonly string JsLibCodeOffline = @"
//<script src=""./konva.min.js""></script>
//".Trim();

//        private static readonly string JsLibCodeOnline = @"
//<script src=""https://unpkg.com/konva@9/konva.min.js""></script>
//".Trim();


//        private static readonly ParametricTextComposer HtmlCodeComposer
//            = new ParametricTextComposer("!#", "#!", @"
//<!DOCTYPE html>
//<html>

//<head>
//    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />

//    <title>Konva.js code</title>

//    !#js-lib-code#!

//    <style>
//        html,
//        body {
//            overflow: auto;
//            width: 100%;
//            height: 100%;
//            margin: 0;
//            padding: 0;
//        }

//        !#css-renderDiv-code#!

//        !#css-textDiv-code#!
//    </style>
//</head>

//<body>
//    <div id=""renderDiv""> </div>

//    <script>
//        !#konvajs-code#!
//    </script>
//</body>

//</html>
//".Trim()
//            );


//        private readonly Dictionary<string, GrKonvaJsSceneComposer2D> _sceneComposerList
//            = new Dictionary<string, GrKonvaJsSceneComposer2D>();


//        public bool OfflineJavaScriptLibraries { get; set; } = false;

//        public bool DivFullScreen { get; set; } = true;

//        public int DivWidth { get; set; } = 1280;

//        public int DivHeight { get; set; } = 720;

//        public double LaTeXScalingFactor { get; set; }
//            = 1 / 75d;

//        public WclHtmlImageDataUrlCache ImageCache { get; }
//            = new WclHtmlImageDataUrlCache();

//        public GrKonvaJsSceneComposer2D this[string sceneName]
//        {
//            get => _sceneComposerList[sceneName];
//            set
//            {
//                if (_sceneComposerList.ContainsKey(sceneName))
//                    _sceneComposerList[sceneName] = value;
//                else
//                    _sceneComposerList.Add(sceneName, value);
//            }
//        }

//        public GrKonvaJsSceneComposer2D ActiveSceneComposer { get; private set; }

//        public GrKonvaJsSceneComposer2D FirstSceneComposer
//            => _sceneComposerList.First().Value;

//        public GrKonvaJsStage FirstScene
//            => _sceneComposerList.First().Value.SceneObject;

//        public GrKonvaJsSceneComposer2D LastSceneComposer
//            => _sceneComposerList.Last().Value;

//        public GrKonvaJsStage LastScene
//            => _sceneComposerList.Last().Value.SceneObject;

//        public int SceneComposerCount
//            => _sceneComposerList.Count;

//        public IEnumerable<string> SceneNames
//            => _sceneComposerList.Keys;

//        public IEnumerable<GrKonvaJsSceneComposer2D> SceneComposers
//            => _sceneComposerList.Values;

//        public IEnumerable<GrKonvaJsStage> Scenes
//            => _sceneComposerList.Values.Select(c => c.SceneObject);


//        public GrKonvaJsHtmlComposer2D()
//            : this(new GrKonvaJsSceneComposer2D())
//        {
//        }

//        public GrKonvaJsHtmlComposer2D(string constName)
//            : this(new GrKonvaJsSceneComposer2D(constName))
//        {
//        }

//        //public GrKonvaJsHtmlComposer2D(string constName, GrKonvaJsSnapshotSpecs snapshotSpecs)
//        //    : this(new GrKonvaJsSceneComposer2D(constName, snapshotSpecs))
//        //{
//        //}

//        public GrKonvaJsHtmlComposer2D(GrKonvaJsSceneComposer2D sceneComposer)
//        {
//            ActiveSceneComposer = sceneComposer;

//            _sceneComposerList.Add(
//                ActiveSceneComposer.SceneObject.ConstName,
//                ActiveSceneComposer
//            );
//        }


//        public GrKonvaJsSceneComposer2D GetSceneComposer(string constName)
//        {
//            return _sceneComposerList[constName];
//        }

//        public GrKonvaJsStage GetScene(string constName)
//        {
//            return _sceneComposerList[constName].SceneObject;
//        }

//        public GrKonvaJsHtmlComposer2D SetActiveSceneComposer(string constName)
//        {
//            ActiveSceneComposer = _sceneComposerList[constName];

//            return this;
//        }

//        public GrKonvaJsHtmlComposer2D ClearSceneComposers()
//        {
//            _sceneComposerList.Clear();

//            AddSceneComposer(
//                new GrKonvaJsSceneComposer2D(),
//                true
//            );

//            return this;
//        }

//        public GrKonvaJsSceneComposer2D AddSceneComposer(GrKonvaJsSceneComposer2D sceneComposer, bool setActive = true)
//        {
//            _sceneComposerList.Add(
//                sceneComposer.SceneObject.ConstName,
//                sceneComposer
//            );

//            if (setActive)
//                ActiveSceneComposer = sceneComposer;

//            return sceneComposer;
//        }

//        public GrKonvaJsSceneComposer2D AddSceneComposer(string constName, bool setActive = true)
//        {
//            var sceneComposer = new GrKonvaJsSceneComposer2D(constName);

//            return AddSceneComposer(sceneComposer, setActive);
//        }

//        //public GrKonvaJsSceneComposer2D AddSceneComposer(string constName, GrKonvaJsSnapshotSpecs snapshotSpecs, bool setActive = true)
//        //{
//        //    var sceneComposer = new GrKonvaJsSceneComposer2D(constName, snapshotSpecs);

//        //    return AddSceneComposer(sceneComposer, setActive);
//        //}


//        private string GetJsDivSizeCode()
//        {
//            if (DivFullScreen)
//                return @"
//div.width  = div.clientWidth;
//div.height = div.clientHeight;
//".Trim();

//            return $@"
//div.width  = {DivWidth};
//div.height = {DivHeight};
//".Trim();
//        }

//        private string GetCssTextDivCode()
//        {
//            return @"
//#textDiv {
//	position: absolute;
//	top: 0px;
//	left: 0px;
//	margin: 10px;
//	padding: 10px;
//	touch-action: none;
//	background-color: rgba(255, 255, 255, 0.05);
//}
//".Trim();
//        }

//        private string GetCssRenderDivCode()
//        {
//            if (DivFullScreen)
//            {
//                return @"
//#renderDiv {
//    position: absolute;
//    top: 0px;
//    left: 0px;
//    width: 100%;
//    height: 100%;
//    touch-action: none;
//}
//".Trim();
//            }

//            return $@"
//#renderDiv {{
//    position: absolute;
//    top: 0px;
//    left: 0px;
//	width: {DivWidth}px;
//	height: {DivHeight}px;
//	touch-action: none;
//}}
//".Trim();
//        }

//        public string GetCreateScenesCode()
//        {
//            var lineSeparator =
//                Environment.NewLine + Environment.NewLine;

//            return _sceneComposerList
//                .Values
//                .Select(c => c.GetCreateSceneCode())
//                .Concatenate(lineSeparator);
//        }

//        public string GetAddScenesCode()
//        {
//            var lineSeparator =
//                Environment.NewLine;

//            return _sceneComposerList
//                .Values
//                .Select(c => c.GetAddSceneCode())
//                .Concatenate(lineSeparator);
//        }

//        public string GetKonvaJsCode()
//        {
//            return GetKonvaJsCode(
//                GetCreateScenesCode(),
//                GetAddScenesCode()
//            );
//        }

//        public string GetKonvaJsCode(string createScenesCode, string addScenesCode)
//        {
//            var divSizeCode =
//                GetJsDivSizeCode();

//            return KonvajsCodeComposer.GenerateText(
//                new Dictionary<string, string>
//                {
//                    {"js-div-size", divSizeCode},
//                    {"create-scenes-code", createScenesCode},
//                    {"add-scenes-code", addScenesCode}
//                }
//            );
//        }

//        public string GetHtmlCode(string createScenesCode, string addScenesCode)
//        {
//            var jsLibCode = OfflineJavaScriptLibraries
//                ? JsLibCodeOffline
//                : JsLibCodeOnline;

//            var renderDivCode =
//                GetCssRenderDivCode();

//            var textDivCode =
//                GetCssTextDivCode();

//            var konvaJsCode =
//                GetKonvaJsCode(createScenesCode, addScenesCode);

//            return HtmlCodeComposer.GenerateText(
//                new Dictionary<string, string>
//                {
//                    {"js-lib-code", jsLibCode},
//                    {"css-renderDiv-code", renderDivCode},
//                    {"css-textDiv-code", textDivCode},
//                    {"konvajs-code", konvaJsCode}
//                }
//            );
//        }

//        public string GetHtmlCode()
//        {
//            return GetHtmlCode(
//                GetCreateScenesCode(),
//                GetAddScenesCode()
//            ).RemoveRepeatedEmptyLines();
//        }

//        public TextFilesComposer GetCodeFiles()
//        {
//            var filesComposer = new TextFilesComposer();

//            var fileComposer =
//                filesComposer.InitalizeFile("index.html").ActiveFileComposer;

//            fileComposer.TextComposer.Append(
//                GetHtmlCode(
//                    GetCreateScenesCode(),
//                    GetAddScenesCode()
//                )
//            );

//            filesComposer.FinalizeAllFiles();

//            return filesComposer;
//        }


//    }
//}
