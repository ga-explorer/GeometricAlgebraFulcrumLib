using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Cameras
{
    public sealed class GrBabylonJsUniversalCamera :
        GrBabylonJsTargetCamera
    {
        public sealed class UniversalCameraProperties :
            TargetCameraProperties
        {
            public GrBabylonJsBooleanValue? ApplyGravity { get; set; }

            public GrBabylonJsBooleanValue? CheckCollisions { get; set; }
        
            public GrBabylonJsVector3Value? Ellipsoid { get; set; }

            public GrBabylonJsVector3Value? EllipsoidOffset { get; set; }

            public GrBabylonJsFloat32Value? AngularSensibility { get; set; }

            public GrBabylonJsFloat32Value? TouchAngularSensibility { get; set; }

            public GrBabylonJsFloat32Value? TouchMoveSensibility { get; set; }

            public GrBabylonJsFloat32Value? GamePadAngularSensibility { get; set; }

            public GrBabylonJsFloat32Value? GamePadMoveSensibility { get; set; }
        
            public GrBabylonJsFloat32Value? CollisionMask { get; set; }
        
            public GrBabylonJsInt32ArrayValue? KeysDown { get; set; }

            public GrBabylonJsInt32ArrayValue? KeysLeft { get; set; }

            public GrBabylonJsInt32ArrayValue? KeysRight { get; set; }

            public GrBabylonJsInt32ArrayValue? KeysUp { get; set; }
        
            public GrBabylonJsInt32ArrayValue? KeysRotateLeft { get; set; }

            public GrBabylonJsInt32ArrayValue? KeysRotateRight { get; set; }

            public GrBabylonJsInt32ArrayValue? KeysDownward { get; set; }

            public GrBabylonJsInt32ArrayValue? KeysUpward { get; set; }
        

            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return ApplyGravity.GetNameValueCodePair("applyGravity");
                yield return CheckCollisions.GetNameValueCodePair("checkCollisions");
                yield return CameraRotation.GetNameValueCodePair("cameraRotation");
                yield return Ellipsoid.GetNameValueCodePair("ellipsoid");
                yield return EllipsoidOffset.GetNameValueCodePair("ellipsoidOffset");
                yield return AngularSensibility.GetNameValueCodePair("angularSensibility");
                yield return TouchAngularSensibility.GetNameValueCodePair("touchAngularSensibility");
                yield return TouchMoveSensibility.GetNameValueCodePair("touchMoveSensibility");
                yield return GamePadAngularSensibility.GetNameValueCodePair("gamepadAngularSensibility");
                yield return GamePadMoveSensibility.GetNameValueCodePair("gamepadMoveSensibility");
                yield return CollisionMask.GetNameValueCodePair("collisionMask");
                yield return KeysRotateLeft.GetNameValueCodePair("keysRotateLeft");
                yield return KeysRotateRight.GetNameValueCodePair("keysRotateRight");
                yield return KeysDownward.GetNameValueCodePair("keysDownward");
                yield return KeysUpward.GetNameValueCodePair("keysUpward");
                yield return KeysDown.GetNameValueCodePair("keysDown");
                yield return KeysLeft.GetNameValueCodePair("keysLeft");
                yield return KeysRight.GetNameValueCodePair("keysRight");
                yield return KeysUp.GetNameValueCodePair("keysUp");
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.UniversalCamera";

        public UniversalCameraProperties? Properties { get; private set; } 
            = new UniversalCameraProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
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


        public GrBabylonJsUniversalCamera SetProperties([NotNull] UniversalCameraProperties? properties)
        {
            Properties = properties;

            return this;
        }

        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return Position.GetCode();

            if (ParentScene.IsNullOrEmpty()) yield break;
            yield return SceneVariableName;
        }
    

    }
}