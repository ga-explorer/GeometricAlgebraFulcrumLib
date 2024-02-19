using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;

namespace CodeComposerLib.Irony.Semantic.Type;

/// <summary>
/// This class represents a primitive language type (like int and float in C++)
/// </summary>
public class TypePrimitive : LanguageSymbol, ILanguageType, ILanguageOperator
{
    public TypePrimitive(string symbolName, LanguageScope parentScope, string symbolRoleName)
        : base(symbolName, parentScope, symbolRoleName)
    {
    }


    public ILanguageOperator DuplicateOperator()
    {
        return this;
    }


    public string TypeSignature => SymbolAccessName;

    public virtual bool IsSameType(ILanguageType languageType)
    {
        if (!(languageType is TypePrimitive))
            return false;

        return ((TypePrimitive)languageType).ObjectId == ObjectId;
    }

    public virtual bool IsCompatibleType(ILanguageType languageType)
    {
        if (!(languageType is TypePrimitive))
            return false;

        return ((TypePrimitive)languageType).ObjectId == ObjectId;
    }

    /// <summary>
    /// The operator name when this primitive type is used as a cast operation
    /// </summary>
    public string OperatorName => "cast_to<" + ObjectName + ">";
}