using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateBox-2
/// </summary>
public sealed class GrBabylonJsBox :
    GrBabylonJsMesh
{
    public sealed class BoxOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsFloat32Value? BottomBaseAt
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("bottomBaseAt");
            set => SetAttributeValue("bottomBaseAt", value);
        }

        public GrBabylonJsFloat32Value? TopBaseAt
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("topBaseAt");
            set => SetAttributeValue("topBaseAt", value);
        }

        public GrBabylonJsFloat32Value? Width
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("width");
            set => SetAttributeValue("width", value);
        }

        public GrBabylonJsFloat32Value? Height
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
            set => SetAttributeValue("height", value);
        }

        public GrBabylonJsFloat32Value? Depth
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("depth");
            set => SetAttributeValue("depth", value);
        }

        public GrBabylonJsFloat32Value? Size
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("size");
            set => SetAttributeValue("size", value);
        }

        public GrBabylonJsBooleanValue? Wrap
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("wrap");
            set => SetAttributeValue("wrap", value);
        }

        public GrBabylonJsColor4ArrayValue? FaceColors
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("faceColors");
            set => SetAttributeValue("faceColors", value);
        }

        public GrBabylonJsVector4ArrayValue? FaceUv
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector4ArrayValue>("faceUV");
            set => SetAttributeValue("faceUV", value);
        }

        public GrBabylonJsMeshOrientationValue? SideOrientation
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
            set => SetAttributeValue("sideOrientation", value);
        }

        public GrBabylonJsVector4Value? FrontUVs
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector4Value>("frontUVs");
            set => SetAttributeValue("frontUVs", value);
        }

        public GrBabylonJsVector4Value? BackUVs
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector4Value>("backUVs");
            set => SetAttributeValue("backUVs", value);
        }

        public GrBabylonJsBooleanValue? Updatable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
            set => SetAttributeValue("updatable", value);
        }


        public BoxOptions()
        {
        }
            
        public BoxOptions(BoxOptions options)
        {
            SetAttributeValues(options);
        }
    }


    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateBox";

    public BoxOptions Options { get; private set; }
        = new BoxOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsBox(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsBox(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsBox SetOptions(BoxOptions options)
    {
        Options = new BoxOptions(options);

        return this;
    }

    public GrBabylonJsBox SetProperties(MeshProperties properties)
    {
        Properties = new MeshProperties(properties);

        return this;
    }
}