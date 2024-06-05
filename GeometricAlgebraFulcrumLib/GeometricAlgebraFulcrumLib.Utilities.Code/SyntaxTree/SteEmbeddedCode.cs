using GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteEmbeddedCode : SteSyntaxElement
{
    public ICclLanguageSyntaxConverter LanguageConverter { get; private set; }

    public ISyntaxTreeElement Code { get; set; }


    public SteEmbeddedCode(ICclLanguageSyntaxConverter langConverter)
    {
        LanguageConverter = langConverter;
    }
}