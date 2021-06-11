using CodeComposerLib.SyntaxTree.Expressions;

namespace CodeComposerLib.Languages
{
    public interface IExpressionTreeCodeGenerator : ILanguageCodeGenerator
    {
        void Visit(SteExpression code);
    }
}