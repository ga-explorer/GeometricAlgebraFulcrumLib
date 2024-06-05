using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Constants;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Textures;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Materials;

/// <summary>
/// A material for drawing geometries in a simple shaded (flat or wire-frame) way.
/// This material is not affected by lights.
/// https://threejs.org/docs/#api/en/materials/MeshBasicMaterial
/// </summary>
public class TjMeshBasicMaterial :
    TjMaterialBase
{
    public override string JavaScriptClassName 
        => "MeshBasicMaterial";


    public TjTextureBase AlphaMap { get; set; } = null;

    public TjTextureBase AoMap { get; set; } = null;

    public double AoMapIntensity { get; set; } = 1d;

    public Color Color { get; set; } = Color.White;

    public TjMaterialConstants.TextureCombineOperations TextureCombineOperation { get; set; }
        = TjMaterialConstants.TextureCombineOperations.MultiplyOperation;

    public TjTextureBase EnvironmentMap { get; set; } = null;

    public TjTextureBase LightMap { get; set; } = null;

    public double LightMapIntensity { get; set; } = 1d;

    public TjTextureBase ColorMap { get; set; } = null;

    public double Reflectivity { get; set; } = 1d;

    public double RefractionRatio { get; set; } = 0.98d;

    public TjTextureBase SpecularMap { get; set; } = null;

    public bool RenderAsWireFrame { get; set; } = false;


}