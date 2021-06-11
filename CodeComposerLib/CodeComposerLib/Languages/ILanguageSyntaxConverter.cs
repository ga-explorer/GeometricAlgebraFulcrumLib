using CodeComposerLib.SyntaxTree;

namespace CodeComposerLib.Languages
{
    public interface ILanguageSyntaxConverter : ISteDynamicVisitor<ISyntaxTreeElement>
    {
        LanguageInfo SourceLanguageInfo { get; }

        LanguageInfo TargetLanguageInfo { get; }
    }
}