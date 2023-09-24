using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Materials
{
    public abstract class GrBabylonJsMaterial :
        GrBabylonJsObject,
        IGrVisualElementMaterial3D
    {
        public abstract class MaterialProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsMaterialTransparencyModeValue? TransparencyMode
            {
                get => GetAttributeValueOrNull<GrBabylonJsMaterialTransparencyModeValue>("transparencyMode");
                set => SetAttributeValue("transparencyMode", value);
            }

            public GrBabylonJsFloat32Value? Alpha
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("alpha");
                set => SetAttributeValue("alpha", value);
            }

            public GrBabylonJsAlphaModeValue? AlphaMode
            {
                get => GetAttributeValueOrNull<GrBabylonJsAlphaModeValue>("alphaMode");
                set => SetAttributeValue("alphaMode", value);
            }

            public GrBabylonJsBooleanValue? WireFrame
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("wireFrame");
                set => SetAttributeValue("wireFrame", value);
            }

            public GrBabylonJsMeshOrientationValue? SideOrientation
            {
                get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
                set => SetAttributeValue("sideOrientation", value);
            }

            public GrBabylonJsBooleanValue? BackFaceCulling
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("backFaceCulling");
                set => SetAttributeValue("backFaceCulling", value);
            }

            public GrBabylonJsBooleanValue? CullBackFaces
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("cullBackFaces");
                set => SetAttributeValue("cullBackFaces", value);
            }

            public GrBabylonJsBooleanValue? FogEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("fogEnabled");
                set => SetAttributeValue("fogEnabled", value);
            }

            public GrBabylonJsFloat32Value? PointSize
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pointSize");
                set => SetAttributeValue("pointSize", value);
            }

            public GrBabylonJsBooleanValue? PointsCloud
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("pointsCloud");
                set => SetAttributeValue("pointsCloud", value);
            }



            protected MaterialProperties()
            {
            }

            protected MaterialProperties(MaterialProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        public string MaterialName
            => ConstName;

        public GrBabylonJsSceneValue ParentScene { get; set; }

        public string SceneVariableName
            => ParentScene.Value.ConstName;

        public override GrBabylonJsObjectOptions? ObjectOptions
            => null;


        protected GrBabylonJsMaterial(string constName)
            : base(constName)
        {
        }

        protected GrBabylonJsMaterial(string constName, GrBabylonJsSceneValue scene)
            : base(constName)
        {
            ParentScene = scene;
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();

            if (ParentScene.IsNullOrEmpty()) yield break;
            yield return ParentScene.Value.ConstName;
        }
    }
}