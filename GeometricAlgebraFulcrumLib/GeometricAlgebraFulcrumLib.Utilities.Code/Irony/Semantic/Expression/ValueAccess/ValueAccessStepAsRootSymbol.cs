using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.ValueAccess;

/// <summary>
/// This class represents the first step in the value access process using a language symbol.
/// For example in the statement 'a.b = c' the first step is to access symbol 'a'; then to access component 'b'
/// </summary>
public sealed class ValueAccessStepAsRootSymbol : ValueAccessStep
{
    /// <summary>
    /// The language symbol used for access
    /// </summary>
    public LanguageSymbol AccessSymbol { get; private set; }

    public override string AccessName => AccessSymbol.SymbolAccessName;


    private ValueAccessStepAsRootSymbol(LanguageSymbol componentSymbol)
        : base((componentSymbol as SymbolDataStore)?.SymbolType)
    {
        AccessSymbol = componentSymbol;
    }


    public void ReplaceComponentSymbol(LanguageSymbol symbol)
    {
        AccessSymbol = symbol;
    }


    public override ValueAccessStep Duplicate()
    {
        return new ValueAccessStepAsRootSymbol(AccessSymbol);
    }


    public static ValueAccessStepAsRootSymbol Create(LanguageSymbol componentSymbol)
    {
        return new ValueAccessStepAsRootSymbol(componentSymbol);
    }
}