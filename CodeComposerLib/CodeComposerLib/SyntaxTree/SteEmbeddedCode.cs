using CodeComposerLib.Languages;

namespace CodeComposerLib.SyntaxTree
{
    public class SteEmbeddedCode : SteSyntaxElement
    {
        public ILanguageSyntaxConverter LanguageConverter { get; private set; }

        public ISyntaxTreeElement Code { get; set; }


        public SteEmbeddedCode(ILanguageSyntaxConverter langConverter)
        {
            LanguageConverter = langConverter;
        }
    }
}
