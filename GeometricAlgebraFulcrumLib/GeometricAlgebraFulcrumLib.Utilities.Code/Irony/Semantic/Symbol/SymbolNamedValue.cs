using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Value;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a named value (a named constant) data store language symbol
/// </summary>
public abstract class SymbolNamedValue : SymbolDataStore
{
    public abstract ILanguageValue AssociatedValue { get; }


    protected SymbolNamedValue(string symbolName, LanguageScope parentScope, string symbolRoleName, ILanguageType valueType)
        : base(symbolName, valueType, parentScope, symbolRoleName)
    {
    }
}