namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

public abstract class SdlStoredValue : SdlValue
{
    public override string Value { get; }


    protected SdlStoredValue(string value)
    {
        Value = value;
    }
}