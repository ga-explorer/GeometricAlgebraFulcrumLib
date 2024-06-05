using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This class marks a data store symbol for being an l-value (i.e. a named data storage that can change 
/// value using assignments). For example local variables and procedure parameters are language l-values
/// </summary>
public abstract class SymbolLValue : SymbolDataStore
{
    public override bool IsLValue => true;


    protected SymbolLValue(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
        : base(symbolName, symbolType, parentScope, symbolRoleName)
    {
    }

    protected SymbolLValue(ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
        : base(symbolType, parentScope, symbolRoleName)
    {
    }

}