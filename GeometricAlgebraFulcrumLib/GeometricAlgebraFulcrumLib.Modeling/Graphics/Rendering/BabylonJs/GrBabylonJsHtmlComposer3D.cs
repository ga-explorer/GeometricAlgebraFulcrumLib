using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public class GrBabylonJsHtmlComposer3D
{
    private static readonly ParametricTextComposer BabylonjsCodeComposer
        = new ParametricTextComposer("!#", "#!", @"
var canvas = document.getElementById(""renderCanvas"");

!#js-canvas-size#!

function renderHtmlToCanvas( canvas, html ) {
	const ctx = canvas.getContext( '2d' );

	const svg = `
		<svg xmlns=""http://www.w3.org/2000/svg"" width=""${canvas.width}"" height=""${canvas.height}"">
		<foreignObject width=""100%"" height=""100%"">
			<div xmlns=""http://www.w3.org/1999/xhtml"">${html}</div>
		</foreignObject>
		</svg>`;
	
	const svgBlob = new Blob( [svg], { type: 'image/svg+xml;charset=utf-8' } );
	const svgObjectUrl = URL.createObjectURL( svgBlob );
	
	//await new Promise(res => setTimeout(res, 100));

	const tempImg = new Image();
	tempImg.addEventListener( 
		'load', 
		function() {
			ctx.drawImage( tempImg, 0, 0 );
			URL.revokeObjectURL( svgObjectUrl );
		} 
	);

	tempImg.src = svgObjectUrl;
}

function startRenderLoop(engine, canvas) {
    engine.runRenderLoop(function () {
        if (scenesToRender) {
            const scenesToRenderLength = scenesToRender.length;
            
            for (let i = 0; i < scenesToRenderLength; i++) {
                if (scenesToRender[i].activeCamera) { scenesToRender[i].render(); }
            }
        }
    });
}

var engine = null;
var scenes = [];
var scenesToRender = [];

var createDefaultEngine = function() { 
    return new BABYLON.Engine(canvas, true, { preserveDrawingBuffer: true, stencil: true,  disableWebGL2Support: false}); 
};

!#create-scenes-code#!
            
window.initFunction = async function() {
    var asyncEngineCreation = async function() {
        try {
            return createDefaultEngine();
        } catch(e) {
            console.log(""the available createEngine function failed. Creating the default engine instead"");
            return createDefaultEngine();
        }
    }

    window.engine = await asyncEngineCreation();
    if (!engine) throw 'engine should not be null.';
    startRenderLoop(engine, canvas);
    
    !#add-scenes-code#!
};

initFunction().then(() => {
    scenesToRender = scenes                    
});

window.addEventListener(""resize"", function () {
    engine.resize();
});
".Trim()
        );

    private static readonly string JsLibCodeOffline = @"
<!--  -->
<script src=""https://cdnjs.cloudflare.com/ajax/libs/dat-gui/0.6.2/dat.gui.min.js""></script>

<!--  -->
<script src=""https://assets.babylonjs.com/generated/Assets.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/ammo.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/cannon.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/Oimo.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/earcut.min.js""></script>

<!-- Babylon.js Core -->
<script src=""babylonjs/babylon.js""></script>

<!-- Babylon.js Supported Advanced Materials -->
<script src=""babylonjs/babylonjs.materials.min.js""></script>

<!-- Babylon.js Procedural Textures -->
<script src=""babylonjs/babylonjs.proceduralTextures.min.js""></script>

<!-- Babylon.js Post Processes -->
<script src=""babylonjs/babylonjs.postProcess.min.js""></script>

<!-- Babylon.js All Official Loaders (OBJ, STL, glTF) -->
<script src=""babylonjs/babylonjs.loaders.min.js""></script>

<!-- Babylon.js Scene/Mesh Serializers -->
<script src=""babylonjs/babylonjs.serializers.min.js""></script>

<!-- Babylon.js GUI -->
<script src=""babylonjs/babylon.gui.min.js""></script>

<!-- Babylon.js Inspector -->
<script src=""babylonjs/babylon.inspector.bundle.js""></script>

<!-- Babylon.js Viewer -->
<!-- script src=""babylonjs/babylon.viewer.js""></script -->

<!-- KaTeX Core -->
<!-- link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.css"" integrity=""sha384-Xi8rHCmBmhbuyyhbI88391ZKP2dmfnOl4rT9ZfRI7mLTdk1wblIUnrIq35nqwEvC"" crossorigin=""anonymous"" -->
<!-- script src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.js"" integrity=""sha384-X/XCfMm41VSsqRNQgDerQczD69XqmjOOOwYQvr/uuC+j4OPoNhVgjdGFwhvN02Ja"" crossorigin=""anonymous""></script -->
<link rel=""stylesheet"" href=""katex/katex.min.css"" integrity=""sha384-Xi8rHCmBmhbuyyhbI88391ZKP2dmfnOl4rT9ZfRI7mLTdk1wblIUnrIq35nqwEvC"" crossorigin=""anonymous"">
<script src=""katex/katex.min.js""></script>

<!-- KaTeX Auto-render extension -->
<!-- To automatically render math in text elements, include the auto-render extension: -->
<!-- script src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/contrib/auto-render.min.js"" integrity=""sha384-+XBljXPPiv+OzfbB3cVmLHf4hdUFHlWNZN5spNQ7rmHTXpd7WvJum6fIACpNNfIR"" crossorigin=""anonymous"" ></script -->
<script src=""katex/contrib/auto-render.min.js""></script>

<!-- html2canvas.js -->
<script src=""html2canvas/html2canvas.min.js""></script>
".Trim();
        
    private static readonly string JsLibCodeOnline = @"
<!--  -->
<script src=""https://cdnjs.cloudflare.com/ajax/libs/dat-gui/0.6.2/dat.gui.min.js""></script>

<!--  -->
<script src=""https://assets.babylonjs.com/generated/Assets.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/ammo.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/cannon.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/Oimo.js""></script>

<!--  -->
<script src=""https://preview.babylonjs.com/earcut.min.js""></script>

<!-- Babylon.js Core -->
<script src=""https://cdn.babylonjs.com/babylon.js""></script>

<!-- Babylon.js Supported Advanced Materials -->
<script src=""https://cdn.babylonjs.com/materialsLibrary/babylonjs.materials.min.js""></script>

<!-- Babylon.js Procedural Textures -->
<script src=""https://cdn.babylonjs.com/proceduralTexturesLibrary/babylonjs.proceduralTextures.min.js""></script>

<!-- Babylon.js Post Processes -->
<script src=""https://cdn.babylonjs.com/postProcessesLibrary/babylonjs.postProcess.min.js""></script>

<!-- Babylon.js All Official Loaders (OBJ, STL, glTF) -->
<script src=""https://cdn.babylonjs.com/loaders/babylonjs.loaders.min.js""></script>

<!-- Babylon.js Scene/Mesh Serializers -->
<script src=""https://cdn.babylonjs.com/serializers/babylonjs.serializers.min.js""></script>

<!-- Babylon.js GUI -->
<script src=""https://cdn.babylonjs.com/gui/babylon.gui.min.js""></script>

<!-- Babylon.js Inspector -->
<script src=""https://cdn.babylonjs.com/inspector/babylon.inspector.bundle.js""></script>

<!-- Babylon.js Viewer -->
<script src=""https://cdn.babylonjs.com/viewer/babylon.viewer.js""></script>

<!-- KaTeX Core -->
<link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.css"" integrity=""sha384-Xi8rHCmBmhbuyyhbI88391ZKP2dmfnOl4rT9ZfRI7mLTdk1wblIUnrIq35nqwEvC"" crossorigin=""anonymous"">
<script src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.js"" integrity=""sha384-X/XCfMm41VSsqRNQgDerQczD69XqmjOOOwYQvr/uuC+j4OPoNhVgjdGFwhvN02Ja"" crossorigin=""anonymous""></script>

<!-- KaTeX Auto-render extension -->
<!-- To automatically render math in text elements, include the auto-render extension: -->
<script src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/contrib/auto-render.min.js"" integrity=""sha384-+XBljXPPiv+OzfbB3cVmLHf4hdUFHlWNZN5spNQ7rmHTXpd7WvJum6fIACpNNfIR"" crossorigin=""anonymous"" ></script>

<!-- html2canvas.js -->
<script src=""https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"" integrity=""sha512-BNaRQnYJYiPSqHHDb58B0yaPfCu+Wgds8Gp/gU33kqBtgNS4tSPHuGibyoeqMV/TJlSKda6FXzoEyYGjTe+vXA=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer""></script>
".Trim();

        
    private static readonly ParametricTextComposer HtmlCodeComposer 
        = new ParametricTextComposer("!#", "#!", @"
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />

        <title>!#page-title#!</title>
        
        !#js-lib-code#!
	    
        <script>
          document.addEventListener(""DOMContentLoaded"", function() {
            renderMathInElement(document.body, {
              delimiters: 
		      [
                {left: ""$$"", right: ""$$"", display: true},
			    {left: '$', right: '$', display: false},
                {left: ""\\("", right: ""\\)"", display: false},
                {left: ""\\begin{equation}"", right: ""\\end{equation}"", display: true},
                {left: ""\\begin{align}"", right: ""\\end{align}"", display: true},
                {left: ""\\begin{alignat}"", right: ""\\end{alignat}"", display: true},
                {left: ""\\begin{gather}"", right: ""\\end{gather}"", display: true},
                {left: ""\\begin{CD}"", right: ""\\end{CD}"", display: true},
                {left: ""\\["", right: ""\\]"", display: true}
              ],
              throwOnError : false
            });
          });
        </script>

        <style>
            html, body {
                overflow: auto;
                width: 100%;
                height: 100%;
                margin: 0;
                padding: 0;
            }

            !#css-renderCanvas-code#!

            !#css-textDiv-code#!
        </style>
    </head>
    <body>
        <canvas id=""renderCanvas""> </canvas>

        <script>
            !#babylonjs-code#!
        </script>
    </body>
</html>
".Trim()
        );


    private readonly Dictionary<string, GrBabylonJsSceneComposer3D> _sceneComposerList
        = new Dictionary<string, GrBabylonJsSceneComposer3D>();


    public bool OfflineJavaScriptLibraries { get; set; } = false;

    public bool CanvasFullScreen { get; set; } = true;

    public int CanvasWidth { get; set; } = 1280;

    public int CanvasHeight { get; set; } = 720;

    public string HtmlPageTitle { get; set; } = "Babylon.js Scene";

    public double LaTeXScalingFactor { get; set; }
        = 1 / 75d;

    public WclHtmlImageDataUrlCache ImageCache { get; }
        = new WclHtmlImageDataUrlCache();

    public GrBabylonJsSceneComposer3D this[string sceneName]
    {
        get => _sceneComposerList[sceneName];
        set
        {
            if (_sceneComposerList.ContainsKey(sceneName))
                _sceneComposerList[sceneName] = value;
            else
                _sceneComposerList.Add(sceneName, value);
        }
    }

    public GrBabylonJsSceneComposer3D ActiveSceneComposer { get; private set; }

    public GrBabylonJsSceneComposer3D FirstSceneComposer 
        => _sceneComposerList.First().Value;

    public GrBabylonJsScene FirstScene
        => _sceneComposerList.First().Value.SceneObject;

    public GrBabylonJsSceneComposer3D LastSceneComposer
        => _sceneComposerList.Last().Value;

    public GrBabylonJsScene LastScene
        => _sceneComposerList.Last().Value.SceneObject;

    public int SceneComposerCount
        => _sceneComposerList.Count;

    public IEnumerable<string> SceneNames
        => _sceneComposerList.Keys;

    public IEnumerable<GrBabylonJsSceneComposer3D> SceneComposers 
        => _sceneComposerList.Values;

    public IEnumerable<GrBabylonJsScene> Scenes
        => _sceneComposerList.Values.Select(c => c.SceneObject);
        

    public GrBabylonJsHtmlComposer3D()
        : this(new GrBabylonJsSceneComposer3D())
    {
    }

    public GrBabylonJsHtmlComposer3D(string constName)
        : this(new GrBabylonJsSceneComposer3D(constName))
    {
    }

    public GrBabylonJsHtmlComposer3D(string constName, GrBabylonJsSnapshotSpecs snapshotSpecs)
        : this(new GrBabylonJsSceneComposer3D(constName, snapshotSpecs))
    {
    }

    public GrBabylonJsHtmlComposer3D(GrBabylonJsSceneComposer3D sceneComposer)
    {
        ActiveSceneComposer = sceneComposer;

        _sceneComposerList.Add(
            ActiveSceneComposer.SceneObject.ConstName,
            ActiveSceneComposer
        );
    }


    public GrBabylonJsSceneComposer3D GetSceneComposer(string constName)
    {
        return _sceneComposerList[constName];
    }

    public GrBabylonJsScene GetScene(string constName)
    {
        return _sceneComposerList[constName].SceneObject;
    }

    public GrBabylonJsHtmlComposer3D SetActiveSceneComposer(string constName)
    {
        ActiveSceneComposer = _sceneComposerList[constName];

        return this;
    }

    public GrBabylonJsHtmlComposer3D ClearSceneComposers()
    {
        _sceneComposerList.Clear();

        AddSceneComposer(
            new GrBabylonJsSceneComposer3D(),
            true
        );

        return this;
    }

    public GrBabylonJsSceneComposer3D AddSceneComposer(GrBabylonJsSceneComposer3D sceneComposer, bool setActive = true)
    {
        _sceneComposerList.Add(
            sceneComposer.SceneObject.ConstName, 
            sceneComposer
        );

        if (setActive)
            ActiveSceneComposer = sceneComposer;

        return sceneComposer;
    }

    public GrBabylonJsSceneComposer3D AddSceneComposer(string constName, bool setActive = true)
    {
        var sceneComposer = new GrBabylonJsSceneComposer3D(constName);

        return AddSceneComposer(sceneComposer, setActive);
    }

    public GrBabylonJsSceneComposer3D AddSceneComposer(string constName, GrBabylonJsSnapshotSpecs snapshotSpecs, bool setActive = true)
    {
        var sceneComposer = new GrBabylonJsSceneComposer3D(constName, snapshotSpecs);

        return AddSceneComposer(sceneComposer, setActive);
    }


    private string GetJsCanvasSizeCode()
    {
        if (CanvasFullScreen)
            return @"
canvas.width  = canvas.clientWidth;
canvas.height = canvas.clientHeight;
".Trim();

        return $@"
canvas.width  = {CanvasWidth};
canvas.height = {CanvasHeight};
".Trim();
    }

    private string GetCssTextDivCode()
    {
        return @"
#textDiv {
	position: absolute;
	top: 0px;
	left: 0px;
	margin: 10px;
	padding: 10px;
	touch-action: none;
	background-color: rgba(255, 255, 255, 0.05);
}
".Trim();
    }

    private string GetCssRenderCanvasCode()
    {
        if (CanvasFullScreen)
        {
            return @"
#renderCanvas {
    position: absolute;
    top: 0px;
    left: 0px;
    width: 100%;
    height: 100%;
    touch-action: none;
}
".Trim();
        }

        return $@"
#renderCanvas {{
    position: absolute;
    top: 0px;
    left: 0px;
	width: {CanvasWidth}px;
	height: {CanvasHeight}px;
	touch-action: none;
}}
".Trim();
    }

    public string GetCreateScenesCode()
    {
        var lineSeparator = 
            Environment.NewLine + Environment.NewLine;

        return _sceneComposerList
            .Values
            .Select(c => c.GetCreateSceneCode())
            .Concatenate(lineSeparator);
    }

    public string GetAddScenesCode()
    {
        var lineSeparator = 
            Environment.NewLine;

        return _sceneComposerList
            .Values
            .Select(c => c.GetAddSceneCode())
            .Concatenate(lineSeparator);
    }

    public string GetBabylonJsCode()
    {
        return GetBabylonJsCode(
            GetCreateScenesCode(),
            GetAddScenesCode()
        );
    }

    public string GetBabylonJsCode(string createScenesCode, string addScenesCode)
    {
        var canvasSizeCode =
            GetJsCanvasSizeCode();

        return BabylonjsCodeComposer.GenerateText(
            new Dictionary<string, string>
            {
                {"js-canvas-size", canvasSizeCode},
                {"create-scenes-code", createScenesCode},
                {"add-scenes-code", addScenesCode}
            }
        );
    }

    public string GetHtmlCode(string createScenesCode, string addScenesCode)
    {
        var jsLibCode = OfflineJavaScriptLibraries
            ? JsLibCodeOffline
            : JsLibCodeOnline;

        var renderCanvasCode = 
            GetCssRenderCanvasCode();

        var textDivCode =
            GetCssTextDivCode();

        var babylonJsCode = 
            GetBabylonJsCode(createScenesCode, addScenesCode);

        return HtmlCodeComposer.GenerateText(
            new Dictionary<string, string>
            {
                {"page-title", HtmlPageTitle},
                {"js-lib-code", jsLibCode},
                {"css-renderCanvas-code", renderCanvasCode},
                {"css-textDiv-code", textDivCode},
                {"babylonjs-code", babylonJsCode}
            }
        );
    }

    public string GetHtmlCode()
    {
        return GetHtmlCode(
            GetCreateScenesCode(),
            GetAddScenesCode()
        ).RemoveRepeatedEmptyLines();
    }

    public TextFilesComposer GetCodeFiles()
    {
        var filesComposer = new TextFilesComposer();
            
        var fileComposer = 
            filesComposer.InitializeFile("index.html").ActiveFileComposer;

        fileComposer.TextComposer.Append(
            GetHtmlCode(
                GetCreateScenesCode(),
                GetAddScenesCode()
            )
        );

        filesComposer.FinalizeAllFiles();

        return filesComposer;
    }


}