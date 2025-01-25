using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.OcclusionMaterial
/// </summary>
public sealed class GrBabylonJsOcclusionMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.OcclusionMaterial";

    public GrBabylonJsOcclusionMaterialProperties? Properties { get; private set; }
        = new GrBabylonJsOcclusionMaterialProperties();
    
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


    public GrBabylonJsOcclusionMaterial SetProperties([NotNull] GrBabylonJsOcclusionMaterialProperties? properties)
    {
        Properties = properties;

        return this;
    }
}