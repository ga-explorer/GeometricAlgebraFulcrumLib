using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.SimpleMaterial
/// </summary>
public sealed class GrBabylonJsSimpleMaterial :
    GrBabylonJsMaterial
{
    public sealed class SimpleMaterialProperties :
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
            yield return MaxSimultaneousLights.GetNameValueCodePair("maxSimultaneousLights");
            yield return DisableLighting.GetNameValueCodePair("disableLighting");
        }
    }


    protected override string ConstructorName
        => "new BABYLON.SimpleMaterial";

    public SimpleMaterialProperties? Properties { get; private set; }
        = new SimpleMaterialProperties();
    
    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsSimpleMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsSimpleMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsSimpleMaterial SetProperties([NotNull] SimpleMaterialProperties? properties)
    {
        Properties = properties;

        return this;
    }
}