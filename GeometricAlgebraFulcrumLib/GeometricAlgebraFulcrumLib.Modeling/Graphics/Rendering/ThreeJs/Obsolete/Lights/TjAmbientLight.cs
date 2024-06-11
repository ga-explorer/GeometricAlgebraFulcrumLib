namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Lights;

/// <summary>
/// This light globally illuminates all objects in the scene equally.
/// This light cannot be used to cast shadows as it does not have a direction.
/// https://threejs.org/docs/#api/en/lights/AmbientLight
/// </summary>
public sealed class TjAmbientLight :
    TjLight
{
    public override string JavaScriptClassName 
        => "AmbientLight";
}