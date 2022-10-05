using System.Diagnostics.CodeAnalysis;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.OcclusionMaterial
/// </summary>
public sealed class GrBabylonJsOcclusionMaterial :
    GrBabylonJsMaterial
{
    public sealed class OcclusionMaterialProperties :
        MaterialProperties
    {
        
        

        
    }


    protected override string ConstructorName
        => "new BABYLON.OcclusionMaterial";

    public OcclusionMaterialProperties? Properties { get; private set; }
        = new OcclusionMaterialProperties();
    
    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsOcclusionMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsOcclusionMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsOcclusionMaterial SetProperties([NotNull] OcclusionMaterialProperties? properties)
    {
        Properties = properties;

        return this;
    }
}