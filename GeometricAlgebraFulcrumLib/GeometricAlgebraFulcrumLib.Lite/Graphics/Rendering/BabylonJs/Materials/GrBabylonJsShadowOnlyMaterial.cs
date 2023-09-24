using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Materials
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.ShadowOnlyMaterial
    /// </summary>
    public sealed class GrBabylonJsShadowOnlyMaterial :
        GrBabylonJsMaterial
    {
        public sealed class ShadowOnlyMaterialProperties :
            MaterialProperties
        {
            public GrBabylonJsColor3Value? ShadowColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("shadowColor");
                set => SetAttributeValue("shadowColor", value);
            }
            
            public GrBabylonJsCodeValue? ActiveLight
            {
                get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("activeLight");
                set => SetAttributeValue("activeLight", value);
            }


            public ShadowOnlyMaterialProperties()
            {
            }

            public ShadowOnlyMaterialProperties(ShadowOnlyMaterialProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName
            => "new BABYLON.ShadowOnlyMaterial";

        public ShadowOnlyMaterialProperties Properties { get; private set; }
            = new ShadowOnlyMaterialProperties();
    
        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;


        public GrBabylonJsShadowOnlyMaterial(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsShadowOnlyMaterial(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsShadowOnlyMaterial SetProperties(ShadowOnlyMaterialProperties properties)
        {
            Properties = new ShadowOnlyMaterialProperties(properties);

            return this;
        }
    }
}