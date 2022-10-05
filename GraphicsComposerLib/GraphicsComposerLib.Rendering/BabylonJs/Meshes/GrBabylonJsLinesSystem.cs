using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLineSystem-2
/// </summary>
public sealed class GrBabylonJsLineSystem :
    GrBabylonJsLinesMesh
{
    public sealed class LineSystemOptions :
        GrBabylonJsObjectOptions
    {
        //instance?: Nullable<LinesMesh>;

        public GrBabylonJsVector3ArrayArrayValue? Lines { get; set; }

        public GrBabylonJsColor4ArrayArrayValue? Colors { get; set; }

        public GrBabylonJsMaterialValue? Material { get; set; }

        public GrBabylonJsBooleanValue? UseVertexAlpha { get; set; }

        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Lines.GetNameValueCodePair("lines");
            yield return Colors.GetNameValueCodePair("colors");
            yield return Material.GetNameValueCodePair("material");
            yield return UseVertexAlpha.GetNameValueCodePair("useVertexAlpha");
            yield return Updateable.GetNameValueCodePair("updateable");
        }
    }
    
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateLineSystem";

    public LineSystemOptions? Options { get; private set; }
        = new LineSystemOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsLineSystem(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsLineSystem(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsLineSystem SetOptions(LineSystemOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsLineSystem SetProperties(LinesMeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}