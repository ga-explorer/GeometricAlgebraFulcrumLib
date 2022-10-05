using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Cameras;

public abstract class GrBabylonJsTargetCamera :
    GrBabylonJsCamera
{
    public abstract class TargetCameraProperties :
        CameraProperties
    {
        public GrBabylonJsVector3Value? CameraDirection { get; set; }
        
        public GrBabylonJsVector2Value? CameraRotation { get; set; }
            
        public GrBabylonJsBooleanValue? IgnoreParentScaling { get; set; }

        public GrBabylonJsBooleanValue? InvertRotation { get; set; }

        public GrBabylonJsBooleanValue? NoRotationConstraint { get; set; }

        public GrBabylonJsBooleanValue? UpdateUpVectorFromRotation { get; set; }

        public GrBabylonJsFloat32Value? InverseRotationSpeed { get; set; }
        
        public GrBabylonJsCodeValue? LockedTarget { get; set; }
        
        public GrBabylonJsVector3Value? Rotation { get; set; }
        
        public GrBabylonJsQuaternionValue? RotationQuaternion { get; set; }

        public GrBabylonJsFloat32Value? Speed { get; set; }

        public GrBabylonJsVector3Value? Target { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return CameraDirection.GetNameValueCodePair("cameraDirection");
            yield return CameraRotation.GetNameValueCodePair("cameraRotation");
            yield return IgnoreParentScaling.GetNameValueCodePair("ignoreParentScaling");
            yield return InvertRotation.GetNameValueCodePair("invertRotation");
            yield return NoRotationConstraint.GetNameValueCodePair("noRotationConstraint");
            yield return UpdateUpVectorFromRotation.GetNameValueCodePair("updateUpVectorFromRotation");
            yield return InverseRotationSpeed.GetNameValueCodePair("inverseRotationSpeed");
            yield return LockedTarget.GetNameValueCodePair("lockedTarget");
            yield return Rotation.GetNameValueCodePair("rotation");
            yield return RotationQuaternion.GetNameValueCodePair("rotationQuaternion");
            yield return Speed.GetNameValueCodePair("speed");
            yield return Target.GetNameValueCodePair("target");
        }
    }


    protected GrBabylonJsTargetCamera(string constName) 
        : base(constName)
    {
    }

    protected GrBabylonJsTargetCamera(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }
}