using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Cameras;

public sealed class GrBabylonJsUniversalCamera :
    GrBabylonJsTargetCamera
{
    public sealed class UniversalCameraProperties :
        TargetCameraProperties
    {
        public GrBabylonJsBooleanValue? ApplyGravity
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("applyGravity");
            set => SetAttributeValue("applyGravity", value);
        }

        public GrBabylonJsBooleanValue? CheckCollisions
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("checkCollisions");
            set => SetAttributeValue("checkCollisions", value);
        }

        public GrBabylonJsVector3Value? Ellipsoid
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("ellipsoid");
            set => SetAttributeValue("ellipsoid", value);
        }

        public GrBabylonJsVector3Value? EllipsoidOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("ellipsoidOffset");
            set => SetAttributeValue("ellipsoidOffset", value);
        }

        public GrBabylonJsFloat32Value? AngularSensibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("angularSensibility");
            set => SetAttributeValue("angularSensibility", value);
        }

        public GrBabylonJsFloat32Value? TouchAngularSensibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("touchAngularSensibility");
            set => SetAttributeValue("touchAngularSensibility", value);
        }

        public GrBabylonJsFloat32Value? TouchMoveSensibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("touchMoveSensibility");
            set => SetAttributeValue("touchMoveSensibility", value);
        }

        public GrBabylonJsFloat32Value? GamePadAngularSensibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("gamePadAngularSensibility");
            set => SetAttributeValue("gamePadAngularSensibility", value);
        }

        public GrBabylonJsFloat32Value? GamePadMoveSensibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("gamePadMoveSensibility");
            set => SetAttributeValue("gamePadMoveSensibility", value);
        }

        public GrBabylonJsFloat32Value? CollisionMask
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("collisionMask");
            set => SetAttributeValue("collisionMask", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysDown
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysDown");
            set => SetAttributeValue("keysDown", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysLeft
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysLeft");
            set => SetAttributeValue("keysLeft", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysRight
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysRight");
            set => SetAttributeValue("keysRight", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysUp
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysUp");
            set => SetAttributeValue("keysUp", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysRotateLeft
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysRotateLeft");
            set => SetAttributeValue("keysRotateLeft", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysRotateRight
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysRotateRight");
            set => SetAttributeValue("keysRotateRight", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysDownward
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysDownward");
            set => SetAttributeValue("keysDownward", value);
        }

        public GrBabylonJsInt32ArrayValue? KeysUpward
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("keysUpward");
            set => SetAttributeValue("keysUpward", value);
        }


        public UniversalCameraProperties()
        {
        }

        public UniversalCameraProperties(UniversalCameraProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.UniversalCamera";

    public UniversalCameraProperties Properties { get; private set; }
        = new UniversalCameraProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public GrBabylonJsVector3Value Position { get; set; }


    public GrBabylonJsUniversalCamera(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsUniversalCamera(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsUniversalCamera SetProperties(UniversalCameraProperties properties)
    {
        Properties = new UniversalCameraProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Position.GetCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return SceneVariableName;
    }


}