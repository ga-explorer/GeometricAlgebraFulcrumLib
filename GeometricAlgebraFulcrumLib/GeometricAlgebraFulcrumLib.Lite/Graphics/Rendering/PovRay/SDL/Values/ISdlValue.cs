namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

public interface ISdlValue : ISdlNameable
{
    /// <summary>
    /// The plane text value
    /// </summary>
    string Value { get; }

    /// <summary>
    /// The text value enclosed in double qoute
    /// </summary>
    string QuotedValue { get; }

    /// <summary>
    /// The text value enclosed between tag delimiters
    /// </summary>
    string TaggedValue { get; }

    /// <summary>
    /// The text value converted into a string literal enclosed in double qoute
    /// </summary>
    string LiteralValue { get; }
}