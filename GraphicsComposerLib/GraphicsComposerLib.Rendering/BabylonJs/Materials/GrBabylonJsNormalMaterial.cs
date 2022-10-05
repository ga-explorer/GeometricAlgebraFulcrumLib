using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.NormalMaterial
/// </summary>
public sealed class GrBabylonJsNormalMaterial :
    GrBabylonJsMaterial
{
    public sealed class NormalMaterialProperties :
        MaterialProperties
    {
        public GrBabylonJsColor3Value? DiffuseColor { get; set; }
        
        public GrBabylonJsTextureValue? DiffuseTexture { get; set; }
        
        public GrBabylonJsInt32Value? MaxSimultaneousLights { get; set; }

        public GrBabylonJsBooleanValue? DisableLighting { get; set; }
        

        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return DiffuseColor.GetNameValueCodePair("diffuseColor");
            yield return DiffuseTexture.GetNameValueCodePair("diffuseTexture");
            yield return DisableLighting.GetNameValueCodePair("disableLighting");
            yield return MaxSimultaneousLights.GetNameValueCodePair("maxSimultaneousLights");
        }
    }


    protected override string ConstructorName
        => "new BABYLON.NormalMaterial";

    public NormalMaterialProperties? Properties { get; private set; }
        = new NormalMaterialProperties();
    
    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;
    

    public GrBabylonJsNormalMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsNormalMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsNormalMaterial SetProperties([NotNull] NormalMaterialProperties? properties)
    {
        Properties = properties;

        return this;
    }
}