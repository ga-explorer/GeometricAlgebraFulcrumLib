using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GradientMaterial
/// </summary>
public sealed class GrBabylonJsGradientMaterial :
    GrBabylonJsMaterial
{
    public sealed class GradientMaterialProperties :
        MaterialProperties
    {
        public GrBabylonJsColor3Value? BottomColor { get; set; }

        public GrBabylonJsColor3Value? TopColor { get; set; }
        
        public GrBabylonJsFloat32Value? BottomColorAlpha { get; set; }

        public GrBabylonJsFloat32Value? TopColorAlpha { get; set; }

        public GrBabylonJsFloat32Value? Offset { get; set; }

        public GrBabylonJsFloat32Value? Scale { get; set; }

        public GrBabylonJsFloat32Value? Smoothness { get; set; }

        public GrBabylonJsInt32Value? MaxSimultaneousLights { get; set; }

        public GrBabylonJsBooleanValue? DisableLighting { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return BottomColor.GetNameValueCodePair("bottomColor");
            yield return BottomColorAlpha.GetNameValueCodePair("bottomColorAlpha");
            yield return TopColor.GetNameValueCodePair("topColor");
            yield return TopColorAlpha.GetNameValueCodePair("topColorAlpha");
            yield return Offset.GetNameValueCodePair("offset");
            yield return Scale.GetNameValueCodePair("scale");
            yield return Smoothness.GetNameValueCodePair("smoothness");
            yield return MaxSimultaneousLights.GetNameValueCodePair("maxSimultaneousLights");
            yield return DisableLighting.GetNameValueCodePair("disableLighting");
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GradientMaterial";

    public GradientMaterialProperties? Properties { get; private set; }
        = new GradientMaterialProperties();
    
    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsGradientMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsGradientMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsGradientMaterial SetProperties([NotNull] GradientMaterialProperties? properties)
    {
        Properties = properties;

        return this;
    }
}