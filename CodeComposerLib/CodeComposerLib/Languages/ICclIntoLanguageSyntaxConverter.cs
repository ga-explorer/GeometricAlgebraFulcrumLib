using CodeComposerLib.SyntaxTree;
using DataStructuresLib;

namespace CodeComposerLib.Languages
{
    public interface ICclIntoLanguageSyntaxConverter<in T> :
        IDynamicTreeVisitor<T, ISyntaxTreeElement> where T : class
    {
        CclLanguageInfo TargetLanguageInfo { get; }
    }
}