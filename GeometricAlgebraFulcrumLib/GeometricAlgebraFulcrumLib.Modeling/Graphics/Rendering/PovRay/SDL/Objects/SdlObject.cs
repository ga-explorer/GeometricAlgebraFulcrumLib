using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Modifiers;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Objects;

public abstract class SdlObject : ISdlObject
{
    public List<ISdlObjectModifier> Modifiers { get; private set; }


    protected SdlObject()
    {
        Modifiers = new List<ISdlObjectModifier>();
    }
}