using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials
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
            public GrBabylonJsColor3Value? ShadowColor { get; set; }
            
            public GrBabylonJsCodeValue? ActiveLight { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return ShadowColor.GetNameValueCodePair("shadowColor");
                yield return ActiveLight.GetNameValueCodePair("activeLight");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.ShadowOnlyMaterial";

        public ShadowOnlyMaterialProperties? Properties { get; private set; }
            = new ShadowOnlyMaterialProperties();
    
        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsShadowOnlyMaterial(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsShadowOnlyMaterial(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsShadowOnlyMaterial SetProperties([NotNull] ShadowOnlyMaterialProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}