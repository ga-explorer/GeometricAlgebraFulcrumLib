using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#ExtrudeShapeCustom-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/custom_extrude
/// </summary>
public sealed class GrBabylonJsExtrudeShapeCustom :
    GrBabylonJsMesh
{
    public sealed class ExtrudeShapeCustomOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayValue? Path
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("path");
            set => SetAttributeValue("path", value);
        }

        public GrBabylonJsVector3ArrayValue? Shape
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("shape");
            set => SetAttributeValue("shape", value);
        }

        public GrBabylonJsMeshValue? Instance
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshValue>("instance");
            set => SetAttributeValue("instance", value);
        }

        public GrBabylonJsCodeValue? RotationFunction
        {
            get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("rotationFunction");
            set => SetAttributeValue("rotationFunction", value);
        }

        public GrBabylonJsCodeValue? ScaleFunction
        {
            get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("scaleFunction");
            set => SetAttributeValue("scaleFunction", value);
        }

        public GrBabylonJsBooleanValue? AdjustFrame
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("adjustFrame");
            set => SetAttributeValue("adjustFrame", value);
        }

        public GrBabylonJsVector3Value? FirstNormal
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("firstNormal");
            set => SetAttributeValue("firstNormal", value);
        }

        public GrBabylonJsBooleanValue? CloseShape
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("closeShape");
            set => SetAttributeValue("closeShape", value);
        }

        public GrBabylonJsBooleanValue? ClosePath
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("closePath");
            set => SetAttributeValue("closePath", value);
        }

        public GrBabylonJsBooleanValue? RibbonCloseArray
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("ribbonCloseArray");
            set => SetAttributeValue("ribbonCloseArray", value);
        }

        public GrBabylonJsBooleanValue? RibbonClosePath
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("ribbonClosePath");
            set => SetAttributeValue("ribbonClosePath", value);
        }

        public GrBabylonJsMeshOrientationValue? SideOrientation
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
            set => SetAttributeValue("sideOrientation", value);
        }

        public GrBabylonJsMeshCapValue? Cap
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshCapValue>("cap");
            set => SetAttributeValue("cap", value);
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

        public GrBabylonJsBooleanValue? InvertUv
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertUV");
            set => SetAttributeValue("invertUV", value);
        }

        public GrBabylonJsBooleanValue? Updatable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
            set => SetAttributeValue("updatable", value);
        }

        public ExtrudeShapeCustomOptions()
        {
        }

        public ExtrudeShapeCustomOptions(ExtrudeShapeCustomOptions options)
        {
            SetAttributeValues(options);
        }
    }

    protected override string ConstructorName
        => "BABYLON.MeshBuilder.ExtrudeShapeCustom";

    public ExtrudeShapeCustomOptions Options { get; private set; }
        = new ExtrudeShapeCustomOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsExtrudeShapeCustom(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsExtrudeShapeCustom(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsExtrudeShapeCustom SetOptions(ExtrudeShapeCustomOptions options)
    {
        Options = new ExtrudeShapeCustomOptions(options);

        return this;
    }

    public GrBabylonJsExtrudeShapeCustom SetProperties(MeshProperties properties)
    {
        Properties = new MeshProperties(properties);

        return this;
    }
}