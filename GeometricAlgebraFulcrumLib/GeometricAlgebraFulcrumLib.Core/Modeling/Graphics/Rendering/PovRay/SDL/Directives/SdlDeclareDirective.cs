namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Directives;

public sealed class SdlDeclareDirective : SdlDirective
{
    public bool Local { get; set; }

    public string Name { get; set; }

    public ISdlNameable Value { get; set; }
}