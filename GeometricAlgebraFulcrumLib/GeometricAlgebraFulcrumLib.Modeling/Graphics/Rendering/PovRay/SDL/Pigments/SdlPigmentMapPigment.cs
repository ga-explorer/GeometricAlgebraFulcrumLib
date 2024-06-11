using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Patterns;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Pigments;

public sealed class SdlPigmentMapItem
{
    public double Value { get; set; }

    public ISdlPigment Pigment { get; set; }
}

public sealed class SdlPigmentMapPigment : SdlPigment
{
    public ISdlPattern Pattern { get; set; }

    public List<SdlPigmentMapItem> PigmentItems { get; private set; }


    public SdlPigmentMapPigment()
    {
        PigmentItems = new List<SdlPigmentMapItem>();
    }
}