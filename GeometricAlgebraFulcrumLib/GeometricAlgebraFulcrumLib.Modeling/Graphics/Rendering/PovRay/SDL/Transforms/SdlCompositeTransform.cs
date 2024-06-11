namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Transforms;

public sealed class SdlCompositeTransform : SdlTransform
{
    public List<SdlTransform> Transforms { get; private set; }

    public bool Inverse { get; set; }


    internal SdlCompositeTransform()
    {
        Transforms = new List<SdlTransform>();
    }
}