using CodeComposerLib.SyntaxTree.Expressions;

namespace CodeComposerLib.Languages
{
    public interface IExpressionTreeCodeComposer : 
        ILanguageCodeComposer
    {
        void Visit(SteExpression code);
    }
}