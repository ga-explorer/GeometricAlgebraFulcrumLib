using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Cameras;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.ArcRotateCamera
/// </summary>
public sealed class GrBabylonJsArcRotateCamera :
    GrBabylonJsTargetCamera
{
    public sealed class ArcRotateCameraProperties :
        TargetCameraProperties
    {
        public GrBabylonJsBooleanValue? AllowUpsideDown { get; set; }

        public GrBabylonJsBooleanValue? CheckCollisions { get; set; }
        
        public GrBabylonJsBooleanValue? MapPanning { get; set; }

        public GrBabylonJsVector3Value? PanningAxis { get; set; }

        public GrBabylonJsFloat32Value? PanningDistanceLimit { get; set; }

        public GrBabylonJsFloat32Value? PanningInertia { get; set; }

        public GrBabylonJsFloat32Value? PinchToPanMaxDistance { get; set; }

        public GrBabylonJsVector3Value? PanningOriginTarget { get; set; }

        public GrBabylonJsVector3Value? CollisionRadius { get; set; }

        public GrBabylonJsFloat32Value? Alpha { get; set; }

        public GrBabylonJsFloat32Value? Beta { get; set; }

        public GrBabylonJsFloat32Value? Radius { get; set; }

        public GrBabylonJsFloat32Value? InertialAlphaOffset { get; set; }

        public GrBabylonJsFloat32Value? InertialBetaOffset { get; set; }

        public GrBabylonJsFloat32Value? InertialPanningX { get; set; }

        public GrBabylonJsFloat32Value? InertialPanningY { get; set; }

        public GrBabylonJsFloat32Value? InertialRadiusOffset { get; set; }

        public GrBabylonJsFloat32Value? LowerAlphaLimit { get; set; }

        public GrBabylonJsFloat32Value? LowerBetaLimit { get; set; }

        public GrBabylonJsFloat32Value? LowerRadiusLimit { get; set; }

        public GrBabylonJsFloat32Value? UpperAlphaLimit { get; set; }

        public GrBabylonJsFloat32Value? UpperBetaLimit { get; set; }

        public GrBabylonJsFloat32Value? UpperRadiusLimit { get; set; }
        
        public GrBabylonJsVector2Value? TargetScreenOffset { get; set; }

        public GrBabylonJsFloat32Value? ZoomOnFactor { get; set; }

        public GrBabylonJsFloat32Value? AngularSensibilityX { get; set; }

        public GrBabylonJsFloat32Value? AngularSensibilityY { get; set; }

        public GrBabylonJsFloat32Value? PanningSensibility { get; set; }

        public GrBabylonJsFloat32Value? PinchDeltaPercentage { get; set; }
        
        public GrBabylonJsFloat32Value? PinchPrecision { get; set; }

        public GrBabylonJsBooleanValue? UseAutoRotationBehavior { get; set; }

        public GrBabylonJsBooleanValue? UseBouncingBehavior { get; set; }

        public GrBabylonJsBooleanValue? UseFramingBehavior { get; set; }

        public GrBabylonJsBooleanValue? UseNaturalPinchZoom { get; set; }

        public GrBabylonJsFloat32Value? WheelDeltaPercentage { get; set; }

        public GrBabylonJsFloat32Value? WheelPrecision { get; set; }
        
        public GrBabylonJsBooleanValue? ZoomToMouseLocation { get; set; }

        public GrBabylonJsInt32ArrayValue? KeysDown { get; set; }

        public GrBabylonJsInt32ArrayValue? KeysLeft { get; set; }

        public GrBabylonJsInt32ArrayValue? KeysRight { get; set; }

        public GrBabylonJsInt32ArrayValue? KeysUp { get; set; }

        
        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return AllowUpsideDown.GetNameValueCodePair("allowUpsideDown");
            yield return CheckCollisions.GetNameValueCodePair("checkCollisions");
            yield return MapPanning.GetNameValueCodePair("mapPanning");
            yield return PanningAxis.GetNameValueCodePair("panningAxis");
            yield return PanningDistanceLimit.GetNameValueCodePair("panningDistanceLimit");
            yield return PanningInertia.GetNameValueCodePair("panningInertia");
            yield return PinchToPanMaxDistance.GetNameValueCodePair("pinchToPanMaxDistance");
            yield return PanningOriginTarget.GetNameValueCodePair("panningOriginTarget");
            yield return CollisionRadius.GetNameValueCodePair("collisionRadius");
            yield return Alpha.GetNameValueCodePair("alpha");
            yield return Beta.GetNameValueCodePair("beta");
            yield return Radius.GetNameValueCodePair("radius");
            yield return InertialAlphaOffset.GetNameValueCodePair("inertialAlphaOffset");
            yield return InertialBetaOffset.GetNameValueCodePair("inertialBetaOffset");
            yield return InertialPanningX.GetNameValueCodePair("inertialPanningX");
            yield return InertialPanningY.GetNameValueCodePair("inertialPanningY");
            yield return InertialRadiusOffset.GetNameValueCodePair("inertialRadiusOffset");
            yield return LowerAlphaLimit.GetNameValueCodePair("lowerAlphaLimit");
            yield return LowerBetaLimit.GetNameValueCodePair("lowerBetaLimit");
            yield return LowerRadiusLimit.GetNameValueCodePair("lowerRadiusLimit");
            yield return UpperAlphaLimit.GetNameValueCodePair("upperAlphaLimit");
            yield return UpperBetaLimit.GetNameValueCodePair("upperBetaLimit");
            yield return UpperRadiusLimit.GetNameValueCodePair("upperRadiusLimit");
            yield return TargetScreenOffset.GetNameValueCodePair("targetScreenOffset");
            yield return ZoomOnFactor.GetNameValueCodePair("zoomOnFactor");
            yield return AngularSensibilityX.GetNameValueCodePair("angularSensibilityX");
            yield return AngularSensibilityY.GetNameValueCodePair("angularSensibilityY");
            yield return PanningSensibility.GetNameValueCodePair("panningSensibility");
            yield return PinchDeltaPercentage.GetNameValueCodePair("pinchDeltaPercentage");
            yield return PinchPrecision.GetNameValueCodePair("pinchPrecision");
            yield return UseAutoRotationBehavior.GetNameValueCodePair("useAutoRotationBehavior");
            yield return UseBouncingBehavior.GetNameValueCodePair("useBouncingBehavior");
            yield return UseFramingBehavior.GetNameValueCodePair("useFramingBehavior");
            yield return UseNaturalPinchZoom.GetNameValueCodePair("useNaturalPinchZoom");
            yield return WheelDeltaPercentage.GetNameValueCodePair("wheelDeltaPercentage");
            yield return WheelPrecision.GetNameValueCodePair("wheelPrecision");
            yield return ZoomToMouseLocation.GetNameValueCodePair("zoomToMouseLocation");
            yield return KeysDown.GetNameValueCodePair("keysDown");
            yield return KeysLeft.GetNameValueCodePair("keysLeft");
            yield return KeysRight.GetNameValueCodePair("keysRight");
            yield return KeysUp.GetNameValueCodePair("keysUp");
        }
    }


    protected override string ConstructorName 
        => "new BABYLON.ArcRotateCamera";

    public ArcRotateCameraProperties? Properties { get; private set; }
        = new ArcRotateCameraProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
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
        Properties = properties;

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