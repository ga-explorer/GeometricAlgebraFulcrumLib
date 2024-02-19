using System;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a language symbol that can be used as a data store for later read\write access.
/// For example a local variable, a procedure parameter, and a named constant.
/// </summary>
public abstract class SymbolDataStore : LanguageSymbol
{
    /// <summary>
    /// Type of symbol (may be null for typless symbols)
    /// </summary>
    private ILanguageType _symbolType;

    /// <summary>
    /// Type of symbol (may be null for typless symbols)
    /// </summary>
    public ILanguageType SymbolType
    {
        get
        {
            return _symbolType;
        }
        set
        {
            if (ReferenceEquals(value, null) == false && ReferenceEquals(_symbolType, null))
                _symbolType = value;
            else
                throw new Exception("Illegal type assignment");
        }
    }

    /// <summary>
    /// True if this data store symbol is an l-value (i.e. can change its value using assignments)
    /// </summary>
    public virtual bool IsLValue => false;

    /// <summary>
    /// True if this symbol has a type
    /// </summary>
    public bool HasType => _symbolType != null;

    /// <summary>
    /// If this symbol has a type this returns the name of the type
    /// </summary>
    public string SymbolTypeSignature => (ReferenceEquals(_symbolType, null) ? "" : _symbolType.TypeSignature);


    protected SymbolDataStore(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
        : base(symbolName, parentScope, symbolRoleName)
    {
        _symbolType = symbolType;
    }

    protected SymbolDataStore(ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName)
        : base(parentScope, symbolRoleName)
    {
        _symbolType = symbolType;
    }


    /// <summary>
    /// True if the type of this symbol is the same as the given type
    /// </summary>
    /// <param name="languageType">The name of the type to be compared</param>
    /// <returns></returns>
    public bool HasSameType(ILanguageType languageType)
    {
        return (HasType && _symbolType.IsSameType(languageType));
    }


    ///// <summary>
    ///// Create a data store language symbol
    ///// </summary>
    ///// <param name="symbol_name">The data store symbol name</param>
    ///// <param name="symbol_type">The data store symbol type</param>
    ///// <param name="parent_scope">The parent scope</param>
    ///// <param name="symbol_role_name">The symbol role name</param>
    ///// <returns></returns>
    //public static SymbolDataStore Create(string symbol_name, ILanguageType symbol_type, LanguageScope parent_scope, string symbol_role_name)
    //{
    //    return new SymbolDataStore(symbol_name, symbol_type, parent_scope, symbol_role_name);
    //}

    ///// <summary>
    ///// Create a data store language symbol with an automatically assigned name
    ///// </summary>
    ///// <param name="symbol_type">The data store symbol type</param>
    ///// <param name="parent_scope">The parent scope</param>
    ///// <param name="symbol_role_name">The symbol role name</param>
    ///// <returns></returns>
    //public static SymbolDataStore Create(ILanguageType symbol_type, LanguageScope parent_scope, string symbol_role_name)
    //{
    //    return new SymbolDataStore(symbol_type, parent_scope, symbol_role_name);
    //}

    ///// <summary>
    ///// Create a data store language symbol
    ///// </summary>
    ///// <param name="symbol_name">The data store symbol name</param>
    ///// <param name="parent_scope">The parent scope</param>
    ///// <param name="symbol_role_name">The symbol role name</param>
    ///// <returns></returns>
    //public static SymbolDataStore Create(string symbol_name, LanguageScope parent_scope, string symbol_role_name)
    //{
    //    return new SymbolDataStore(symbol_name, null, parent_scope, symbol_role_name);
    //}

    ///// <summary>
    ///// Create a data store language symbol with an automatically assigned name
    ///// </summary>
    ///// <param name="parent_scope">The parent scope</param>
    ///// <param name="symbol_role_name">The symbol role name</param>
    ///// <returns></returns>
    //public static SymbolDataStore Create(LanguageScope parent_scope, string symbol_role_name)
    //{
    //    return new SymbolDataStore(null, parent_scope, symbol_role_name);
    //}
}