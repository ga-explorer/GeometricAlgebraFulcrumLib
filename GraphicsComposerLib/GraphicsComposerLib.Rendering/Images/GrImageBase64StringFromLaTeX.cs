using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Rendering.Images;

public sealed class GrImageBase64StringFromLaTeX :
    GrImageBase64String
{
    public string? LaTeXCode { get; }


    internal GrImageBase64StringFromLaTeX(string key, string? latexCode, int marginSize, Color backgroundColor)
        : base(key, marginSize, backgroundColor)
    {
        LaTeXCode = latexCode;
    }
}