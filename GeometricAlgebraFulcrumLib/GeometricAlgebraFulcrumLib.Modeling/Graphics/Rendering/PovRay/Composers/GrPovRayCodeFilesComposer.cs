//using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
//using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

//namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;

//public class GrPovRayCodeFilesComposer
//{
//    private readonly Dictionary<string, GrPovRaySceneComposer> _sceneComposerList
//        = new Dictionary<string, GrPovRaySceneComposer>();


//    public GrPovRayOptions DefaultSceneOptions { get; } 
//        = new GrPovRayOptions();

//    public double LaTeXScalingFactor { get; set; }
//        = 1 / 75d;

//    public WclHtmlImageDataUrlCache ImageCache { get; }
//        = new WclHtmlImageDataUrlCache();

//    public GrPovRaySceneComposer this[string sceneName]
//    {
//        get => _sceneComposerList[sceneName];
//        set
//        {
//            if (_sceneComposerList.ContainsKey(sceneName))
//                _sceneComposerList[sceneName] = value;
//            else
//                _sceneComposerList.Add(sceneName, value);
//        }
//    }

//    public GrPovRaySceneComposer ActiveSceneComposer { get; private set; }

//    public GrPovRaySceneComposer FirstSceneComposer
//        => _sceneComposerList.First().Value;

//    public GrPovRayScene FirstScene
//        => _sceneComposerList.First().Value.SceneObject;

//    public GrPovRaySceneComposer LastSceneComposer
//        => _sceneComposerList.Last().Value;

//    public GrPovRayScene LastScene
//        => _sceneComposerList.Last().Value.SceneObject;

//    public int SceneComposerCount
//        => _sceneComposerList.Count;

//    public IEnumerable<string> SceneNames
//        => _sceneComposerList.Keys;

//    public IEnumerable<GrPovRaySceneComposer> SceneComposers
//        => _sceneComposerList.Values;

//    public IEnumerable<GrPovRayScene> Scenes
//        => _sceneComposerList.Values.Select(c => c.SceneObject);


//    public GrPovRayCodeFilesComposer()
//        : this(new GrPovRaySceneComposer())
//    {
//    }
    
//    public GrPovRayCodeFilesComposer(GrPovRayOptions sceneOptions)
//        : this(new GrPovRaySceneComposer(sceneOptions))
//    {
//    }

//    public GrPovRayCodeFilesComposer(string sceneName)
//        : this(new GrPovRaySceneComposer(sceneName))
//    {
//    }

//    public GrPovRayCodeFilesComposer(string sceneName, GrPovRayOptions sceneOptions)
//        : this(new GrPovRaySceneComposer(sceneName, sceneOptions))
//    {
//    }

//    public GrPovRayCodeFilesComposer(GrPovRaySceneComposer sceneComposer)
//    {
//        ActiveSceneComposer = sceneComposer;

//        _sceneComposerList.Add(
//            ActiveSceneComposer.SceneObject.SceneName,
//            ActiveSceneComposer
//        );
//    }


//    public GrPovRaySceneComposer GetSceneComposer(string sceneName)
//    {
//        return _sceneComposerList[sceneName];
//    }

//    public GrPovRayScene GetSceneObject(string sceneName)
//    {
//        return _sceneComposerList[sceneName].SceneObject;
//    }
    
//    public GrPovRayOptions GetSceneOptions(string sceneName)
//    {
//        return _sceneComposerList[sceneName].SceneObject.SceneOptions;
//    }

//    public GrPovRayCodeFilesComposer SetActiveSceneComposer(string sceneName)
//    {
//        ActiveSceneComposer = _sceneComposerList[sceneName];

//        return this;
//    }

//    public GrPovRayCodeFilesComposer ClearSceneComposers()
//    {
//        _sceneComposerList.Clear();

//        AddSceneComposer(
//            new GrPovRaySceneComposer(),
//            true
//        );

//        return this;
//    }

//    public GrPovRaySceneComposer AddSceneComposer(GrPovRaySceneComposer sceneComposer, bool setActive = true)
//    {
//        _sceneComposerList.Add(
//            sceneComposer.SceneObject.SceneName,
//            sceneComposer
//        );

//        if (setActive)
//            ActiveSceneComposer = sceneComposer;

//        return sceneComposer;
//    }

//    public GrPovRaySceneComposer AddSceneComposer(string sceneName, bool setActive = true)
//    {
//        var sceneComposer = new GrPovRaySceneComposer(sceneName);

//        return AddSceneComposer(sceneComposer, setActive);
//    }

//    public GrPovRaySceneComposer AddSceneComposer(string sceneName, GrPovRayOptions sceneOptions, bool setActive = true)
//    {
//        var sceneComposer = new GrPovRaySceneComposer(sceneName, sceneOptions);

//        return AddSceneComposer(sceneComposer, setActive);
//    }


//    public IEnumerable<KeyValuePair<string, string>> GetScenesCode()
//    {
//        return _sceneComposerList.Select(p => 
//            new KeyValuePair<string, string>(
//                p.Key, 
//                p.Value.SceneObject.GetPovRayCode()
//            )
//        );
//    }
    
//    public IEnumerable<KeyValuePair<string, string>> GetScenesOptionsCode()
//    {
//        return _sceneComposerList.Select(p => 
//            new KeyValuePair<string, string>(
//                p.Key, 
//                p.Value.SceneObject.SceneOptions.GetPovRayCode()
//            )
//        );
//    }

//    public TextFilesComposer GetCodeFiles(TextFilesComposer filesComposer)
//    {
//        foreach (var (fileName, code) in GetScenesCode())
//        {
//            var fileComposer =
//                filesComposer.InitializeFile(fileName + ".pov").ActiveFileComposer;

//            fileComposer.TextComposer.Append(code);
//        }
        
//        foreach (var (fileName, code) in GetScenesOptionsCode())
//        {
//            var fileComposer =
//                filesComposer.InitializeFile(fileName + ".ini").ActiveFileComposer;

//            fileComposer.TextComposer.Append(code);
//        }

//        return filesComposer;
//    }


//}