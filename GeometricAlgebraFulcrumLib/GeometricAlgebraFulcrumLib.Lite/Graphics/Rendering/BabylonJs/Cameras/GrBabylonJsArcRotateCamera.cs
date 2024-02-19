using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Cameras;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.ArcRotateCamera
/// </summary>
public sealed class GrBabylonJsArcRotateCamera :
    GrBabylonJsTargetCamera
{
    public sealed class ArcRotateCameraProperties :
        TargetCameraProperties
    {
        public GrBabylonJsBooleanValue? AllowUpsideDown
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("allowUpsideDown");
            set => SetAttributeValue("allowUpsideDown", value);
        }

        public GrBabylonJsBooleanValue? CheckCollisions
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("checkCollisions");
            set => SetAttributeValue("checkCollisions", value);
        }

        public GrBabylonJsBooleanValue? MapPanning
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("mapPanning");
            set => SetAttributeValue("mapPanning", value);
        }

        public GrBabylonJsVector3Value? PanningAxis
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("panningAxis");
            set => SetAttributeValue("panningAxis", value);
        }

        public GrBabylonJsFloat32Value? PanningDistanceLimit
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("panningDistanceLimit");
            set => SetAttributeValue("panningDistanceLimit", value);
        }

        public GrBabylonJsFloat32Value? PanningInertia
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("panningInertia");
            set => SetAttributeValue("panningInertia", value);
        }

        public GrBabylonJsFloat32Value? PinchToPanMaxDistance
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pinchToPanMaxDistance");
            set => SetAttributeValue("pinchToPanMaxDistance", value);
        }

        public GrBabylonJsVector3Value? PanningOriginTarget
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("panningOriginTarget");
            set => SetAttributeValue("panningOriginTarget", value);
        }

        public GrBabylonJsVector3Value? CollisionRadius
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("collisionRadius");
            set => SetAttributeValue("collisionRadius", value);
        }

        public GrBabylonJsFloat32Value? Alpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("alpha");
            set => SetAttributeValue("alpha", value);
        }

        public GrBabylonJsFloat32Value? Beta
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("beta");
            set => SetAttributeValue("beta", value);
        }

        public GrBabylonJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radius");
            set => SetAttributeValue("radius", value);
        }

        public GrBabylonJsFloat32Value? InertialAlphaOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("inertialAlphaOffset");
            set => SetAttributeValue("inertialAlphaOffset", value);
        }

        public GrBabylonJsFloat32Value? InertialBetaOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("inertialBetaOffset");
            set => SetAttributeValue("inertialBetaOffset", value);
        }

        public GrBabylonJsFloat32Value? InertialPanningX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("inertialPanningX");
            set => SetAttributeValue("inertialPanningX", value);
        }

        public GrBabylonJsFloat32Value? InertialPanningY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("inertialPanningY");
            set => SetAttributeValue("inertialPanningY", value);
        }

        public GrBabylonJsFloat32Value? InertialRadiusOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("inertialRadiusOffset");
            set => SetAttributeValue("inertialRadiusOffset", value);
        }

        public GrBabylonJsFloat32Value? LowerAlphaLimit
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lowerAlphaLimit");
            set => SetAttributeValue("lowerAlphaLimit", value);
        }

        public GrBabylonJsFloat32Value? LowerBetaLimit
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lowerBetaLimit");
            set => SetAttributeValue("lowerBetaLimit", value);
        }

        public GrBabylonJsFloat32Value? LowerRadiusLimit
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lowerRadiusLimit");
            set => SetAttributeValue("lowerRadiusLimit", value);
        }

        public GrBabylonJsFloat32Value? UpperAlphaLimit
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("upperAlphaLimit");
            set => SetAttributeValue("upperAlphaLimit", value);
        }

        public GrBabylonJsFloat32Value? UpperBetaLimit
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("upperBetaLimit");
            set => SetAttributeValue("upperBetaLimit", value);
        }

        public GrBabylonJsFloat32Value? UpperRadiusLimit
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("upperRadiusLimit");
            set => SetAttributeValue("upperRadiusLimit", value);
        }

        public GrBabylonJsVector2Value? TargetScreenOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("targetScreenOffset");
            set => SetAttributeValue("targetScreenOffset", value);
        }

        public GrBabylonJsFloat32Value? ZoomOnFactor
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("zoomOnFactor");
            set => SetAttributeValue("zoomOnFactor", value);
        }

        public GrBabylonJsFloat32Value? AngularSensibilityX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("angularSensibilityX");
            set => SetAttributeValue("angularSensibilityX", value);
        }

        public GrBabylonJsFloat32Value? AngularSensibilityY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("angularSensibilityY");
            set => SetAttributeValue("angularSensibilityY", value);
        }

        public GrBabylonJsFloat32Value? PanningSensibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("panningSensibility");
            set => SetAttributeValue("panningSensibility", value);
        }

        public GrBabylonJsFloat32Value? PinchDeltaPercentage
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pinchDeltaPercentage");
            set => SetAttributeValue("pinchDeltaPercentage", value);
        }

        public GrBabylonJsFloat32Value? PinchPrecision
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pinchPrecision");
            set => SetAttributeValue("pinchPrecision", value);
        }

        public GrBabylonJsBooleanValue? UseAutoRotationBehavior
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useAutoRotationBehavior");
            set => SetAttributeValue("useAutoRotationBehavior", value);
        }

        public GrBabylonJsBooleanValue? UseBouncingBehavior
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useBouncingBehavior");
            set => SetAttributeValue("useBouncingBehavior", value);
        }

        public GrBabylonJsBooleanValue? UseFramingBehavior
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useFramingBehavior");
            set => SetAttributeValue("useFramingBehavior", value);
        }

        public GrBabylonJsBooleanValue? UseNaturalPinchZoom
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useNaturalPinchZoom");
            set => SetAttributeValue("useNaturalPinchZoom", value);
        }

        public GrBabylonJsFloat32Value? WheelDeltaPercentage
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("wheelDeltaPercentage");
            set => SetAttributeValue("wheelDeltaPercentage", value);
        }

        public GrBabylonJsFloat32Value? WheelPrecision
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("wheelPrecision");
            set => SetAttributeValue("wheelPrecision", value);
        }

        public GrBabylonJsBooleanValue? ZoomToMouseLocation
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("zoomToMouseLocation");
            set => SetAttributeValue("zoomToMouseLocation", value);
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


        public ArcRotateCameraProperties()
        {
        }

        public ArcRotateCameraProperties(ArcRotateCameraProperties properties)
        {
            SetAttributeValues(properties);
        }
            
    }


    protected override string ConstructorName
        => "new BABYLON.ArcRotateCamera";

    public ArcRotateCameraProperties Properties { get; private set; }
        = new ArcRotateCameraProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public GrBabylonJsFloat32Value Alpha { get; set; }

    public GrBabylonJsFloat32Value Beta { get; set; }

    public GrBabylonJsFloat32Value Radius { get; set; }

    public GrBabylonJsVector3Value Target { get; set; }

    public GrBabylonJsBooleanValue? SetActiveOnSceneIfNoneActive { get; set; }


    public GrBabylonJsArcRotateCamera(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsArcRotateCamera(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsArcRotateCamera SetProperties(ArcRotateCameraProperties properties)
    {
        Properties = new ArcRotateCameraProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return Alpha.GetCode();
        yield return Beta.GetCode();
        yield return Radius.GetCode();
        yield return Target.GetCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return SceneVariableName;

        if (SetActiveOnSceneIfNoneActive is null || SetActiveOnSceneIfNoneActive.IsEmpty) yield break;
        yield return SetActiveOnSceneIfNoneActive.GetCode();
    }

}