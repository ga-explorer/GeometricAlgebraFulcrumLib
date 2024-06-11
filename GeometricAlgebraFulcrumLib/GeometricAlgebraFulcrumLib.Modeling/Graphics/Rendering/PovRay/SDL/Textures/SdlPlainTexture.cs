using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Transforms;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Textures;

public class SdlPlainTexture : SdlTexture
{
    public string TextureIdentifier { get; set; }

    public string PigmentIdentifier { get; set; }

    public string FinishIdentifier { get; set; }

    public ISdlPigment Pigment { get; set; }

    public ISdlFinish Finish { get; set; }

    public List<ISdlTransform> Transforms { get; private set; }


    public SdlPlainTexture()
    {
        Transforms = new List<ISdlTransform>();
    }
}