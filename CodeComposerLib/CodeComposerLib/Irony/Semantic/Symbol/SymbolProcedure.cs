using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a procedure\function\macro language symbol. When this procedure is called it is treated as a polyadic language operator
/// </summary>
public class SymbolProcedure : SymbolWithCommandBlock, ILanguageOperator
{
    /// <summary>
    /// The list of parameters for this procedure. If the procedure returns one or more values they are returned via one or more output parameters
    /// </summary>
    public IEnumerable<SymbolProcedureParameter> Parameters { get; }
        

    /// <summary>
    /// When this procedure is called it is treated as a polyadic language operator. This returns its access name
    /// </summary>
    string ILanguageOperator.OperatorName => "call<" + SymbolAccessName + ">";


    protected SymbolProcedure(string symbolName, LanguageScope parentScope)
        : this(symbolName, parentScope, parentScope.RootAst.ProcedureRoleName, parentScope.RootAst.ProcedureParameterRoleName)
    {
    }

    protected SymbolProcedure(string symbolName, LanguageScope parentScope, string symbolRoleName, string parametersRoleName)
        : base(symbolName, parentScope, symbolRoleName)
    {
        Parameters = ChildSymbolScope.Symbols(parametersRoleName).Cast<SymbolProcedureParameter>();
    }


    public ILanguageOperator DuplicateOperator()
    {
        return this;
    }


    /// <summary>
    /// Return the first output parameter for this procedure; if any.
    /// </summary>
    public SymbolProcedureParameter FirstOutputParameter
    {
        get { return Parameters.FirstOrDefault(param => param.DirectionOut); }
    }


    public SymbolProcedureParameter DefineParameter(string symbolName, ILanguageType symbolType, string symbolRoleName, bool dirIn, bool dirOut)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, dirIn, dirOut);
    }

    public SymbolProcedureParameter DefineInputParameter(string symbolName, ILanguageType symbolType, string symbolRoleName)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, true, false);
    }

    public SymbolProcedureParameter DefineOutputParameter(string symbolName, ILanguageType symbolType, string symbolRoleName)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, false, true);
    }

    public SymbolProcedureParameter DefineInputOutputParameter(string symbolName, ILanguageType symbolType, string symbolRoleName)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, true, true);
    }

    public SymbolProcedureParameter DefineParameter(string symbolName, ILanguageType symbolType, bool dirIn, bool dirOut)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, RootAst.ProcedureParameterRoleName, dirIn, dirOut);
    }

    public SymbolProcedureParameter DefineInputParameter(string symbolName, ILanguageType symbolType)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, RootAst.ProcedureParameterRoleName, true, false);
    }

    public SymbolProcedureParameter DefineOutputParameter(string symbolName, ILanguageType symbolType)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, RootAst.ProcedureParameterRoleName, false, true);
    }

    public SymbolProcedureParameter DefineInputOutputParameter(string symbolName, ILanguageType symbolType)
    {
        return SymbolProcedureParameter.Create(symbolName, symbolType, ChildSymbolScope, RootAst.ProcedureParameterRoleName, true, true);
    }

    /// <summary>
    /// Return the full type signature for this procedure
    /// </summary>
    /// <returns></returns>
    public virtual string GetProcedureTypeSignature()
    {
        var s = new StringBuilder();

        s.Append("proc< ");

        foreach (var p in Parameters)
        {
            s.Append(p.GetParameterTypeSignature());

            s.Append(", ");
        }

        s.Length -= 2;

        s.Append(" >");

        return s.ToString();
    }

    public SymbolProcedureParameter GetParameter(string paramName)
    {
        if (ChildSymbolScope.LookupSymbol(paramName, out SymbolProcedureParameter procParam))
            return procParam;

        throw new KeyNotFoundException("Procedure parameter " + paramName + " not found");
    }

    public ILanguageType GetParameterType(string paramName)
    {
        if (ChildSymbolScope.LookupSymbol(paramName, out SymbolProcedureParameter procParam))
            return procParam.SymbolType;

        throw new KeyNotFoundException("Procedure parameter " + paramName + " not found");
    }
        
        
    public static SymbolProcedure Create(string symbolName, LanguageScope parentScope)
    {
        return new SymbolProcedure(symbolName, parentScope);
    }

    public static SymbolProcedure Create(string symbolName, LanguageScope parentScope, string symbolRoleName, string parametersRoleName)
    {
        return new SymbolProcedure(symbolName, parentScope, symbolRoleName, parametersRoleName);
    }
}