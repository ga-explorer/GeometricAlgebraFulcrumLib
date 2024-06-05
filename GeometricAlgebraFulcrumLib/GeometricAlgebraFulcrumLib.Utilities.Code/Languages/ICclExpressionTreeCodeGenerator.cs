using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public interface ICclExpressionTreeCodeGenerator : 
    ICclLanguageCodeGenerator
{
    void Visit(SteExpression code);
}