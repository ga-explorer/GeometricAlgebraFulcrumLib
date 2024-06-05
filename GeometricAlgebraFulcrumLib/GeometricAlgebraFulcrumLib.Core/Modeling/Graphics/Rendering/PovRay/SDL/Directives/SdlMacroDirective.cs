namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Directives;

public sealed class SdlMacroDirective : SdlDirective
{
    public string Name { get; set; }

    public List<string> Parameters { get; private set; }

    public List<ISdlStatement> Statements { get; private set; }


    internal SdlMacroDirective()
    {
        Parameters = new List<string>();
        Statements = new List<ISdlStatement>();
    }
}