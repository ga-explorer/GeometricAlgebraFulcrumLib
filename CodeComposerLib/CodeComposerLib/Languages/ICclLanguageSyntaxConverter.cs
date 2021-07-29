using CodeComposerLib.SyntaxTree;

namespace CodeComposerLib.Languages
{
    public interface ICclLanguageSyntaxConverter : 
        ISteDynamicVisitor<ISyntaxTreeElement>
    {
        CclLanguageInfo SourceLanguageInfo { get; }

        CclLanguageInfo TargetLanguageInfo { get; }
    }
}