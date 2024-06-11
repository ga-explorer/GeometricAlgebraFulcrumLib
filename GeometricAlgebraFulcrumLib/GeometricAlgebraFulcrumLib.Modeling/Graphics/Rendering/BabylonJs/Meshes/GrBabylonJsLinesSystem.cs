using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLineSystem-2
/// </summary>
public sealed class GrBabylonJsLineSystem :
    GrBabylonJsLinesMesh
{
    public sealed class LineSystemOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayArrayValue? Lines
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayArrayValue>("lines");
            set => SetAttributeValue("lines", value);
        }

        public GrBabylonJsColor4ArrayArrayValue? Colors
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayArrayValue>("colors");
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

        public GrBabylonJsLinesMeshValue? Instance
        {
            get => GetAttributeValueOrNull<GrBabylonJsLinesMeshValue>("instance");
            set => SetAttributeValue("instance", value);
        }

        public GrBabylonJsBooleanValue? Updatable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
            set => SetAttributeValue("updatable", value);
        }


        public LineSystemOptions()
        {
        }

        public LineSystemOptions(LineSystemOptions options)
        {
            SetAttributeValues(options);
        }
    }
    
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateLineSystem";

    public LineSystemOptions Options { get; private set; }
        = new LineSystemOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
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
        Options = new LineSystemOptions(options);

        return this;
    }

    public GrBabylonJsLineSystem SetProperties(LinesMeshProperties properties)
    {
        Properties = new LinesMeshProperties(properties);

        return this;
    }
}