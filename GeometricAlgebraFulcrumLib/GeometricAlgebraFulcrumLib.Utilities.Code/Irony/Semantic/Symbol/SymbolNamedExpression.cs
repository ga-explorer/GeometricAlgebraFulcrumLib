using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a named expression data store language symbol
/// </summary>
public class SymbolNamedExpression : SymbolDataStore
{
    public ILanguageExpression RhsExpression { get; private set; }


    protected SymbolNamedExpression(string symbolName, LanguageScope parentScope, string symbolRoleName, ILanguageExpression rhsExpr)
        : base(symbolName, rhsExpr.ExpressionType, parentScope, symbolRoleName)
    {
        RhsExpression = rhsExpr;
    }
}