using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.DSLInterpreter;

public abstract class LanguageInterpreter<TSymbolData>
    : ILanguageInterpreter<TSymbolData>, IAstNodeDynamicVisitor
{
    /// <summary>
    /// The parent Irony DSL for this evaluator
    /// </summary>
    public IronyAst ParentDsl { get; private set; }

    /// <summary>
    /// The step number for evaluation process
    /// </summary>
    public int StepNumber { get; private set; }

    /// <summary>
    /// The current active activation record
    /// </summary>
    public ActivationRecord<TSymbolData> ActiveAr { get; protected set; }

    /// <summary>
    /// A helper object used for performing basic access operations on values using LanguageValueAccess objects
    /// </summary>
    public LanguageValueAccessPrecessor ValueAccessProcessor { get; }

    public bool IgnoreNullElements => false;

    public bool UseExceptions => true;


    protected LanguageInterpreter(LanguageScope rootScope, LanguageValueAccessPrecessor valueAccessProc)
    {
        ParentDsl = rootScope.RootAst;

        ValueAccessProcessor = valueAccessProc;

        ActiveAr = new ActivationRecord<TSymbolData>(this, rootScope);
    }


    protected int AdvanceStep()
    {
        StepNumber += 1;

        return StepNumber;
    }


    /// <summary>
    /// Depending on the given scope and the active activation record this function creates a suitable activation
    /// record on top of the activation record stack
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="useUpperStaticAr"></param>
    /// <returns></returns>
    protected ActivationRecord<TSymbolData> PushRecord(LanguageScope scope, bool useUpperStaticAr)
    {
        ActiveAr = new ActivationRecord<TSymbolData>(ActiveAr, scope, useUpperStaticAr);

        return ActiveAr;
    }

    /// <summary>
    /// This function pops and returns the top activation record of the activation record stack
    /// </summary>
    /// <returns></returns>
    protected ActivationRecord<TSymbolData> PopRecord()
    {
        var record = ActiveAr;

        ActiveAr = ActiveAr.UpperDynamicAr;

        return record;
    }

    public virtual TSymbolData GetSymbolData(SymbolDataStore symbol)
    {
        var symbolData = default(TSymbolData);

        if (ActiveAr.StaticChain.Any(record => record.TryGetSymbolData(symbol, out symbolData)))
        {
            return symbolData;
        }

        throw new KeyNotFoundException("Data store variable " + symbol.ObjectName + " not found in static chain");
    }

    public virtual void SetSymbolData(SymbolDataStore symbol, TSymbolData symbolData)
    {
        foreach (var record in ActiveAr.StaticChain.Where(record => record.ContainsSymbol(symbol)))
        {
            record.SetSymbolData(symbol, symbolData);

            return;
        }

        throw new KeyNotFoundException("Data store variable " + symbol.ObjectName + " not found in static chain");
    }

    public void Fallback(IIronyAstObject objItem, RuntimeBinderException excException)
    {
        throw new NotImplementedException();
    }
}

public abstract class LanguageInterpreter<TSymbolData, TReturnValue> 
    : ILanguageInterpreter<TSymbolData>, IAstNodeDynamicVisitor<TReturnValue>
{
    /// <summary>
    /// The parent Irony DSL for this evaluator
    /// </summary>
    public IronyAst ParentDsl { get; private set; }

    /// <summary>
    /// The step number for evaluation process
    /// </summary>
    public int StepNumber { get; private set; }

    /// <summary>
    /// The current active activation record
    /// </summary>
    public ActivationRecord<TSymbolData> ActiveAr { get; protected set; }

    /// <summary>
    /// A helper object used for performing basic access operations on values using LanguageValueAccess objects
    /// </summary>
    public LanguageValueAccessPrecessor ValueAccessProcessor { get; }

    public bool IgnoreNullElements => false;

    public bool UseExceptions => true;


    protected LanguageInterpreter(LanguageScope rootScope, LanguageValueAccessPrecessor valueAccessProc)
    {
        ParentDsl = rootScope.RootAst;

        ValueAccessProcessor = valueAccessProc;

        ActiveAr = new ActivationRecord<TSymbolData>(this, rootScope);
    }


    protected int AdvanceStep()
    {
        StepNumber += 1;

        return StepNumber;
    }


    /// <summary>
    /// Depending on the given scope and the active activation record this function creates a suitable activation
    /// record on top of the activation record stack
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="useUpperStaticAr"></param>
    /// <returns></returns>
    protected ActivationRecord<TSymbolData> PushRecord(LanguageScope scope, bool useUpperStaticAr)
    {
        ActiveAr = new ActivationRecord<TSymbolData>(ActiveAr, scope, useUpperStaticAr);

        return ActiveAr;
    }

    /// <summary>
    /// This function pops and returns the top activation record of the activation record stack
    /// </summary>
    /// <returns></returns>
    protected ActivationRecord<TSymbolData> PopRecord()
    {
        var record = ActiveAr;

        ActiveAr = ActiveAr.UpperDynamicAr;

        return record;
    }


    public virtual TSymbolData GetSymbolData(SymbolDataStore symbol)
    {
        var symbolData = default(TSymbolData);

        if (ActiveAr.StaticChain.Any(record => record.TryGetSymbolData(symbol, out symbolData)))
            return symbolData;

        throw new KeyNotFoundException("Data store variable " + symbol.ObjectName + " not found in static chain");
    }

    public virtual void SetSymbolData(SymbolDataStore symbol, TSymbolData symbolData)
    {
        foreach (var record in ActiveAr.StaticChain.Where(record => record.ContainsSymbol(symbol)))
        {
            record.SetSymbolData(symbol, symbolData);

            return;
        }

        throw new KeyNotFoundException("Data store variable " + symbol.ObjectName + " not found in static chain");
    }

    public TReturnValue Fallback(IIronyAstObject objItem, RuntimeBinderException excException)
    {
        throw new NotImplementedException();
    }
}