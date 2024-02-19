using CodeComposerLib.SyntaxTree.Expressions;
using DataStructuresLib;

namespace CodeComposerLib.Languages;

public interface ICclExpressionToLanguageConverter<in T> :
    IDynamicTreeVisitor<T, SteExpression> where T : class
{
    CclLanguageInfo TargetLanguageInfo { get; }
}