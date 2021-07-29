using CodeComposerLib.SyntaxTree.Expressions;

namespace CodeComposerLib.Languages
{
    public interface ICclExpressionTreeCodeGenerator : 
        ICclLanguageCodeGenerator
    {
        void Visit(SteExpression code);
    }
}