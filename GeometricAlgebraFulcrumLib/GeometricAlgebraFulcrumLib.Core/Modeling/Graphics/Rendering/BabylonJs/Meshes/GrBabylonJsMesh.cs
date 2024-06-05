using System.Text;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public abstract class GrBabylonJsMesh :
    GrBabylonJsObject
{
    public class MeshProperties :
        GrBabylonJsObjectProperties
    {
        public GrBabylonJsMaterialValue? Material
        {
            get => GetAttributeValueOrNull<GrBabylonJsMaterialValue>("material");
            set => SetAttributeValue("material", value);
        }

        public GrBabylonJsVector3Value? Position
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("position");
            set => SetAttributeValue("position", value);
        }

        public GrBabylonJsVector3Value? Scaling
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("scaling");
            set => SetAttributeValue("scaling", value);
        }

        public GrBabylonJsQuaternionValue? RotationQuaternion
        {
            get => GetAttributeValueOrNull<GrBabylonJsQuaternionValue>("rotationQuaternion");
            set => SetAttributeValue("rotationQuaternion", value);
        }

        public GrBabylonJsColor3Value? EdgesColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("edgesColor");
            set => SetAttributeValue("edgesColor", value);
        }

        public GrBabylonJsFloat32Value? EdgesWidth
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("edgesWidth");
            set => SetAttributeValue("edgesWidth", value);
        }

        public GrBabylonJsBooleanValue? RenderOutline
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderOutline");
            set => SetAttributeValue("renderOutline", value);
        }

        public GrBabylonJsColor3Value? OutlineColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("outlineColor");
            set => SetAttributeValue("outlineColor", value);
        }

        public GrBabylonJsFloat32Value? OutlineWidth
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("outlineWidth");
            set => SetAttributeValue("outlineWidth", value);
        }

        public GrBabylonJsBooleanValue? RenderOverlay
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderOverlay");
            set => SetAttributeValue("renderOverlay", value);
        }

        public GrBabylonJsColor3Value? OverlayColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("overlayColor");
            set => SetAttributeValue("overlayColor", value);
        }

        public GrBabylonJsFloat32Value? OverlayAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("overlayAlpha");
            set => SetAttributeValue("overlayAlpha", value);
        }

        public GrBabylonJsInt32Value? AlphaIndex
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("alphaIndex");
            set => SetAttributeValue("alphaIndex", value);
        }

        public GrBabylonJsBooleanValue? ShowBoundingBox
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("showBoundingBox");
            set => SetAttributeValue("showBoundingBox", value);
        }

        public GrBabylonJsFloat32Value? Visibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("visibility");
            set => SetAttributeValue("visibility", value);
        }

        public GrBabylonJsBooleanValue? IsVisible
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isVisible");
            set => SetAttributeValue("isVisible", value);
        }

        public GrBabylonJsBooleanValue? IsPickable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isPickable");
            set => SetAttributeValue("isPickable", value);
        }

        public GrBabylonJsBooleanValue? IsNearPickable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isNearPickable");
            set => SetAttributeValue("isNearPickable", value);
        }

        public GrBabylonJsBooleanValue? IsNearGrabbable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isNearGrabbable");
            set => SetAttributeValue("isNearGrabbable", value);
        }

        public GrBabylonJsBillboardModeValue? BillboardMode
        {
            get => GetAttributeValueOrNull<GrBabylonJsBillboardModeValue>("billboardMode");
            set => SetAttributeValue("billboardMode", value);
        }


        public MeshProperties()
        {
        }
            
        public MeshProperties(MeshProperties properties)
        {
            SetAttributeValues(properties);
        }
    }

    public GrBabylonJsSceneValue ParentScene { get; set; }

    public string SceneVariableName
        => ParentScene.Value.ConstName;

    public MeshProperties Properties { get; protected set; }
        = new MeshProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public IAffineMap3D PreTransformMap { get; set; }
        = IdentityMap3D.DefaultMap;


    protected GrBabylonJsMesh(string constName)
        : base(constName)
    {
    }

    protected GrBabylonJsMesh(string constName, GrBabylonJsSceneValue scene)
        : base(constName)
    {
        ParentScene = scene;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        var optionsCode =
            ObjectOptions is null
                ? "{}"
                : ObjectOptions.GetCode();

        yield return optionsCode;

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;
    }

    public override string GetCode()
    {
        var composer = new StringBuilder();

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();

        if (!string.IsNullOrEmpty(ConstName))
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            composer.Append($"{declarationKeyword} {ConstName} = ");
        }

        composer
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);

        if (!string.IsNullOrEmpty(ConstName) && PreTransformMap is not IdentityMap3D)
        {
            var matrixCode =
                PreTransformMap.GetBabylonJsMatrixCode();

            composer.AppendLine(
                $"{ConstName}.setPreTransformMatrix({matrixCode});"
            );
        }

        return composer.ToString();
    }

}