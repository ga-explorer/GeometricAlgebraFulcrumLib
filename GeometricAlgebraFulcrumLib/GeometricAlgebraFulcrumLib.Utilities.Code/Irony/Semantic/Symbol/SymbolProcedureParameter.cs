using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a procedure parameter data store language symbol
/// </summary>
public class SymbolProcedureParameter : SymbolLValue
{
    /// <summary>
    /// The order of the definition of this parameter inside the scope of its procedure
    /// </summary>
    public int DefinitionIndex { get; private set; }

    /// <summary>
    /// When True, the parameter is an input parameter
    /// </summary>
    public bool DirectionIn { get; }

    /// <summary>
    /// When True, the parameter is an output parameter
    /// </summary>
    public bool DirectionOut { get; }

    /// <summary>
    /// Return true if the parameter is input only parameter
    /// </summary>
    public bool DirectionInOnly => DirectionIn && !DirectionOut;

    /// <summary>
    /// Return true if the parameter is output only parameter
    /// </summary>
    public bool DirectionOutOnly => !DirectionIn && DirectionOut;

    /// <summary>
    /// Return true if the parameter is input and output parameter
    /// </summary>
    public bool DirectionInOut => DirectionIn && DirectionOut;


    protected SymbolProcedureParameter(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName, bool dirIn, bool dirOut)
        : base(symbolName, symbolType, parentScope, symbolRoleName)
    {
        if (!(dirIn || dirOut))
            throw new Exception("Illegal procedure parameter direction");

        DefinitionIndex = parentScope.Symbols(symbolRoleName).Count();
        DirectionIn = dirIn;
        DirectionOut = dirOut;
    }

    protected SymbolProcedureParameter(string symbolName, ILanguageType symbolType, LanguageScope parentScope, bool dirIn, bool dirOut)
        : this(symbolName, symbolType, parentScope, parentScope.RootAst.ProcedureParameterRoleName, dirIn, dirOut)
    {
    }


    public virtual string GetParameterTypeSignature()
    {
        return (DirectionIn ? "in" : "") + (DirectionOut ? "out" : "") + "< " + SymbolType.TypeSignature + " >";
    }


    public static SymbolProcedureParameter Create(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName, bool dirIn, bool dirOut)
    {
        return new SymbolProcedureParameter(symbolName, symbolType, parentScope, symbolRoleName, dirIn, dirOut);
    }

    public static SymbolProcedureParameter Create(string symbolName, ILanguageType symbolType, LanguageScope parentScope, bool dirIn, bool dirOut)
    {
        return new SymbolProcedureParameter(symbolName, symbolType, parentScope, dirIn, dirOut);
    }
}