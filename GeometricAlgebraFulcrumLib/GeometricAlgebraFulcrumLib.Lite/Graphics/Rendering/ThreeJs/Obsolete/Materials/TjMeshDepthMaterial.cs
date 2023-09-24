using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Constants;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Textures;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Materials
{
    public class TjMeshDepthMaterial :
        TjMaterialBase
    {
        /// <summary>
        /// A material for drawing geometry by depth. Depth is based off
        /// of the camera near and far plane. White is nearest, black is farthest.
        /// https://threejs.org/docs/#api/en/materials/MeshDepthMaterial
        /// </summary>
        public override string JavaScriptClassName 
            => "MeshDepthMaterial";

        public TjTextureConstants.ColorEncodings DepthPacking { get; set; }
            = TjTextureConstants.ColorEncodings.BasicDepthPacking;

        public TjTextureBase DisplacementMap { get; set; } = null;

        public double DisplacementScale { get; set; } = 1d;

        public double DisplacementBias { get; set; } = 0d;


    }
}