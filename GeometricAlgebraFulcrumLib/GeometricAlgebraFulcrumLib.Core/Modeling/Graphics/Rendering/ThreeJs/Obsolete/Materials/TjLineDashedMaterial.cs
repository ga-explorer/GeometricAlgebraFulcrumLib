namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Materials;

/// <summary>
/// A material for drawing wire-frame style geometries with dashed lines.
/// https://threejs.org/docs/#api/en/materials/LineDashedMaterial
/// </summary>
public class TjLineDashedMaterial :
    TjLineBasicMaterial
{
    public override string JavaScriptClassName 
        => "LineDashedMaterial";

    public double DashSize { get; set; } = 3d;

    public double GapSize { get; set; } = 1d;

    public double Scale { get; set; } = 1d;



}