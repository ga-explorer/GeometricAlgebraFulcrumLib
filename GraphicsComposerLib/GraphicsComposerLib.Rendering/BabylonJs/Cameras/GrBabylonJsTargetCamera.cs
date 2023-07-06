using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Cameras
{
    public abstract class GrBabylonJsTargetCamera :
        GrBabylonJsCamera
    {
        public abstract class TargetCameraProperties :
            CameraProperties
        {
            public GrBabylonJsVector3Value? CameraDirection
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("cameraDirection");
                set => SetAttributeValue("cameraDirection", value);
            }

            public GrBabylonJsVector2Value? CameraRotation
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("cameraRotation");
                set => SetAttributeValue("cameraRotation", value);
            }

            public GrBabylonJsBooleanValue? IgnoreParentScaling
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("ignoreParentScaling");
                set => SetAttributeValue("ignoreParentScaling", value);
            }

            public GrBabylonJsBooleanValue? InvertRotation
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertRotation");
                set => SetAttributeValue("invertRotation", value);
            }

            public GrBabylonJsBooleanValue? NoRotationConstraint
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("noRotationConstraint");
                set => SetAttributeValue("noRotationConstraint", value);
            }

            public GrBabylonJsBooleanValue? UpdateUpVectorFromRotation
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updateUpVectorFromRotation");
                set => SetAttributeValue("updateUpVectorFromRotation", value);
            }

            public GrBabylonJsFloat32Value? InverseRotationSpeed
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("inverseRotationSpeed");
                set => SetAttributeValue("inverseRotationSpeed", value);
            }

            public GrBabylonJsCodeValue? LockedTarget
            {
                get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("lockedTarget");
                set => SetAttributeValue("lockedTarget", value);
            }

            public GrBabylonJsVector3Value? Rotation
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("rotation");
                set => SetAttributeValue("rotation", value);
            }

            public GrBabylonJsQuaternionValue? RotationQuaternion
            {
                get => GetAttributeValueOrNull<GrBabylonJsQuaternionValue>("rotationQuaternion");
                set => SetAttributeValue("rotationQuaternion", value);
            }

            public GrBabylonJsFloat32Value? Speed
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("speed");
                set => SetAttributeValue("speed", value);
            }

            public GrBabylonJsVector3Value? Target
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("target");
                set => SetAttributeValue("target", value);
            }


            protected TargetCameraProperties()
            {
            }

            protected TargetCameraProperties(TargetCameraProperties properties)
            {
                SetAttributeValues(properties);
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
}