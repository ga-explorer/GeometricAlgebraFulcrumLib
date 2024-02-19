using System;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using TextComposerLib.Loggers.Progress;

namespace CodeComposerLib.Irony.DSLInterpreter;

/// <summary>
/// This class uses the dynamic visitor pattern
/// </summary>
public abstract class LanguageExpressionEvaluator : LanguageInterpreter<ILanguageValue, ILanguageValue>, IProgressReportSource
{
    public abstract string ProgressSourceId { get; }

    public abstract ProgressComposer Progress { get; }

        
    protected LanguageExpressionEvaluator(LanguageScope rootScope, LanguageValueAccessPrecessor valueAccessProc)
        : base(rootScope, valueAccessProc)
    {
    }



    /// <summary>
    /// Read the full value of the given symbol depending on its type (for example a constant's value 
    /// is stored with the object while a local variable's value must be read from the suitable activation record)
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    protected abstract ILanguageValue ReadSymbolFullValue(LanguageSymbol symbol);

    /// <summary>
    /// Returns true if the given symbol's value can be fully or partially updated.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    protected abstract bool AllowUpdateSymbolValue(LanguageSymbol symbol);

    /// <summary>
    /// Update the full or partial value of a symbol (if allowed) depending on the given value access
    /// </summary>
    /// <param name="valueAccess"></param>
    /// <param name="value"></param>
    protected virtual void UpdateSymbolValue(LanguageValueAccess valueAccess, ILanguageValue value)
    {
        var symbol = valueAccess.RootSymbol;

        if (AllowUpdateSymbolValue(symbol) == false)
            throw new InvalidOperationException("Invalid symbol type");

        if (valueAccess.IsFullAccess)
        {
            SetSymbolData((SymbolDataStore)symbol, value);

            return;
        }

        var sourceValue = ReadSymbolFullValue(symbol);

        ValueAccessProcessor.WritePartialValue(sourceValue, valueAccess, value);
    }


    /// <summary>
    /// Return the given value as it is
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual ILanguageValue Visit(ILanguageValue value)
    {
        return value;
    }

    /// <summary>
    /// Read the value associated with a value access process
    /// </summary>
    /// <param name="valueAccess"></param>
    /// <returns></returns>
    public virtual ILanguageValue Visit(LanguageValueAccess valueAccess)
    {
        var value = ReadSymbolFullValue(valueAccess.RootSymbol);

        return 
            valueAccess.IsFullAccess ? 
                value : 
                ValueAccessProcessor.ReadPartialValue(value, valueAccess);
    }

    /// <summary>
    /// Compute the value of the given language expression
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public abstract ILanguageValue Visit(CompositeExpression expr);

    /// <summary>
    /// Compute the value of the given language expression
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public abstract ILanguageValue Visit(BasicUnary expr);

    /// <summary>
    /// Compute the value of the given language expression
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public abstract ILanguageValue Visit(BasicBinary expr);

    /// <summary>
    /// Compute the value of the given language expression
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public abstract ILanguageValue Visit(BasicPolyadic expr);


    /// <summary>
    /// Execute the given command without returning any value
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public virtual ILanguageValue Visit(CommandComment command)
    {
        return null;
    }

    /// <summary>
    /// Execute the given command without returning any value
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Should return null</returns>
    public abstract ILanguageValue Visit(CommandDeclareVariable command);

    /// <summary>
    /// Execute the given command without returning any value
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Should return null</returns>
    public abstract ILanguageValue Visit(CommandAssign command);

    /// <summary>
    /// Execute the given command without returning any value
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Should return null</returns>
    public abstract ILanguageValue Visit(CommandBlock command);
}