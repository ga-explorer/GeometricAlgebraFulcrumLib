using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a local variable data store language symbol
/// </summary>
public class SymbolLocalVariable : SymbolLValue
{
    protected SymbolLocalVariable(string symbolName, ILanguageType symbolType, LanguageScope parentScope)
        : base(symbolName, symbolType, parentScope, parentScope.RootAst.LocalVariableRoleName)
    {
    }

    protected SymbolLocalVariable(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
        : base(symbolName, symbolType, parentScope, symbolRoleName)
    {
    }

    protected SymbolLocalVariable(ILanguageType symbolType, LanguageScope parentScope)
        : base(symbolType, parentScope, parentScope.RootAst.LocalVariableRoleName)
    {
    }

    protected SymbolLocalVariable(ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
        : base(symbolType, parentScope, symbolRoleName)
    {
    }



    public static SymbolLocalVariable Create(string symbolName, ILanguageType symbolType, LanguageScope parentScope)
    {
        return new SymbolLocalVariable(symbolName, symbolType, parentScope);
    }

    public static SymbolLocalVariable Create(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
    {
        return new SymbolLocalVariable(symbolName, symbolType, parentScope, symbolRoleName);
    }

    public static SymbolLocalVariable Create(ILanguageType symbolType, LanguageScope parentScope)
    {
        return new SymbolLocalVariable(symbolType, parentScope);
    }

    public static SymbolLocalVariable Create(ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
    {
        return new SymbolLocalVariable(symbolType, parentScope, symbolRoleName);
    }
}