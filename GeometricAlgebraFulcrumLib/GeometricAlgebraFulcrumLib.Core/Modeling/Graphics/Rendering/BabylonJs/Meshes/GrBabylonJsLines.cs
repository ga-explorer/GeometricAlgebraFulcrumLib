using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLines-2
/// </summary>
public sealed class GrBabylonJsLines :
    GrBabylonJsLinesMesh
{
    public sealed class LinesOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayValue? Points
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("points");
            set => SetAttributeValue("points", value);
        }

        public GrBabylonJsColor4ArrayValue? Colors
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("colors");
            set => SetAttributeValue("colors", value);
        }

        public GrBabylonJsMaterialValue? Material
        {
            get => GetAttributeValueOrNull<GrBabylonJsMaterialValue>("material");
            set => SetAttributeValue("material", value);
        }

        public GrBabylonJsBooleanValue? UseVertexAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useVertexAlpha");
            set => SetAttributeValue("useVertexAlpha", value);
        }

        public GrBabylonJsBooleanValue? Updatable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
            set => SetAttributeValue("updatable", value);
        }

        public GrBabylonJsLinesMeshValue? Instance
        {
            get => GetAttributeValueOrNull<GrBabylonJsLinesMeshValue>("instance");
            set => SetAttributeValue("instance", value);
        }


        public LinesOptions()
        {
        }

        public LinesOptions(LinesOptions options)
        {
            SetAttributeValues(options);
        }
    }
    
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateLines";

    public LinesOptions Options { get; private set; }
        = new LinesOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsLines(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsLines(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsLines SetOptions(LinesOptions options)
    {
        Options = new LinesOptions(options);

        return this;
    }

    public GrBabylonJsLines SetProperties(LinesMeshProperties properties)
    {
        Properties = new LinesMeshProperties(properties);

        return this;
    }
}