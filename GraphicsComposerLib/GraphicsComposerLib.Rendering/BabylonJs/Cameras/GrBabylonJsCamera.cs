using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.BabylonJs.Cameras
{
    public abstract class GrBabylonJsCamera :
        GrBabylonJsObject
    {
        public abstract class CameraProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsCameraModeValue? Mode { get; set; }

            public GrBabylonJsFloat32Value? Fov { get; set; }

            public GrBabylonJsCameraFovModeValue? FovMode { get; set; }

            public GrBabylonJsFloat32Value? Inertia { get; set; }

            public GrBabylonJsFloat32Value? MaxZ { get; set; }

            public GrBabylonJsFloat32Value? MinZ { get; set; }

            public GrBabylonJsFloat32Value? ProjectionPlaneTilt { get; set; }

            public GrBabylonJsFloat32Value? OrthoBottom { get; set; }

            public GrBabylonJsFloat32Value? OrthoTop { get; set; }

            public GrBabylonJsFloat32Value? OrthoLeft { get; set; }

            public GrBabylonJsFloat32Value? OrthoRight { get; set; }

            public GrBabylonJsVector3Value? Position { get; set; }

            public GrBabylonJsVector3Value? UpVector { get; set; }
    
    
            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Mode.GetNameValueCodePair("mode");
                yield return Fov.GetNameValueCodePair("fov");
                yield return FovMode.GetNameValueCodePair("fovMode");
                yield return Inertia.GetNameValueCodePair("inertia");
                yield return MaxZ.GetNameValueCodePair("maxZ");
                yield return MinZ.GetNameValueCodePair("minZ");
                yield return ProjectionPlaneTilt.GetNameValueCodePair("projectionPlaneTilt");
                yield return OrthoBottom.GetNameValueCodePair("orthoBottom");
                yield return OrthoTop.GetNameValueCodePair("orthoTop");
                yield return OrthoLeft.GetNameValueCodePair("orthoLeft");
                yield return OrthoRight.GetNameValueCodePair("orthoRight");
                yield return Position.GetNameValueCodePair("position");
                yield return UpVector.GetNameValueCodePair("upVector");
            }
        }


        public string SceneVariableName 
            => ParentScene?.Value.ConstName ?? string.Empty;

        public bool AttachControl { get; set; } = true;

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => null;

        public GrBabylonJsSceneValue? ParentScene { get; set; }


        protected GrBabylonJsCamera(string constName)
            : base(constName)
        {
        }

        protected GrBabylonJsCamera(string constName, GrBabylonJsSceneValue scene)
            : base(constName)
        {
            ParentScene = scene;
        }

    
        public override string GetCode()
        {
            var composer = new LinearTextComposer();
        
            if (!string.IsNullOrEmpty(ConstName))
                composer.Append($"const {ConstName} = ");

            var constructorCode = GetConstructorCode();
            var propertiesCode = GetPropertiesCode();

            composer
                .AppendLine(constructorCode)
                .AppendAtNewLine(propertiesCode);

            if (AttachControl)
                composer.AppendLineAtNewLine($"{ConstName}.attachControl(true);");

            return composer.ToString();
        }
    }
}