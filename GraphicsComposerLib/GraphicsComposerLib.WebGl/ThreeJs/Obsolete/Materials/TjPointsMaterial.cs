namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Materials
{
    /// <summary>
    /// The default material used by Points.
    /// https://threejs.org/docs/#api/en/materials/PointsMaterial
    /// </summary>
    public class TjPointsMaterial :
        TjMaterialBase
    {
        public override string JavaScriptClassName 
            => "PointsMaterial";

        public double Size { get; set; } = 1d;

        public bool SizeAttenuation { get; set; } = true;
    }
}