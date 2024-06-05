using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a language symbol having a child scope to contain other symbols. 
/// For example language symbol with a scope can be a structure, a class, a function, a namespace, ... etc
/// </summary>
public class SymbolWithScope : LanguageSymbol, IIronyAstObjectWithScope
{
    /// <summary>
    /// The child symbol scope of this language symbol
    /// </summary>
    public ScopeSymbolChild ChildSymbolScope { get; }

    /// <summary>
    /// The child scope of this language symbol
    /// </summary>
    public LanguageScope ChildScope => ChildSymbolScope;

    /// <summary>
    /// The name of the child scope for this language symbol
    /// </summary>
    public string ChildScopeName => ChildSymbolScope.ObjectName;

    /// <summary>
    /// The symbols inside the child scope of this symbol
    /// </summary>
    public IEnumerable<LanguageSymbol> ChildSymbols => ChildSymbolScope.Symbols();


    protected SymbolWithScope(string symbolName, LanguageScope parentScope, string symbolRoleName)
        : base(symbolName, parentScope, symbolRoleName)
    {
        ChildSymbolScope = ScopeSymbolChild.Create(this, symbolName + "_" + symbolRoleName + "_scope");
    }


    /// <summary>
    /// Return true if a symbol inside the child scope exists with the given name and role name
    /// </summary>
    /// <param name="symbolName"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public bool ChildSymbolExists(string symbolName, string roleName)
    {
        return ChildSymbolScope.SymbolExists(symbolName, roleName);
    }

    /// <summary>
    /// Return true if a symbol inside the child scope exists with the given name
    /// </summary>
    /// <param name="symbolName"></param>
    /// <returns></returns>
    public bool ChildSymbolExists(string symbolName)
    {
        return ChildSymbolScope.SymbolExists(symbolName);
    }

    ///// <summary>
    ///// Return true if a symbol inside the child scope exists with the given name
    ///// </summary>
    ///// <param name="symbol_name"></param>
    ///// <param name="role_name"></param>
    ///// <returns></returns>
    //public bool CanDefineChildSymbol(string symbol_name, string role_name)
    //{
    //    return this.ChildScope.SymbolExists(symbol_name, role_name) == false;
    //}

    /// <summary>
    /// Return true if a symbol inside the child scope with the given name does not exist thus can be defined safely
    /// </summary>
    /// <param name="symbolName"></param>
    /// <returns></returns>
    public bool CanDefineChildSymbol(string symbolName)
    {
        return ChildSymbolScope.SymbolExists(symbolName) == false;
    }

    /// <summary>
    /// Use this with extreme caution beacuse you may remove a child symbol that other 
    /// AST symbols use or reference
    /// </summary>
    /// <param name="symbolName"></param>
    public void RemoveChildSymbol(string symbolName)
    {
        ChildScope.RemoveLanguageSymbol(symbolName);
    }


    public override string ToString()
    {
        //if (this.HasType)
        //    return "<" + this.ObjectName + " : " + this.SymbolType.TypeName + ">";

        return SymbolAccessName;
    }
}