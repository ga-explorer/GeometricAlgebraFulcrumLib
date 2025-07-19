using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;
using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.DSLInterpreter;

/// <summary>
/// This class represents an activation record
/// </summary>
public sealed class ActivationRecord<TSymbolData>
{
    private static readonly IntegerSequenceGenerator IdCounter 
        = new IntegerSequenceGenerator();

    private static int CreateNewId()
    {
        return IdCounter.GetNewCountId();
    }

    public static string CreateNewObjectName()
    {
        return IdCounter.GetNewStringId("sto_");
    }

    public static string CreateNewObjectName(string prefix)
    {
        return IdCounter.GetNewStringId(prefix);
    }


    /// <summary>
    /// A unique ID for this activtion record
    /// </summary>
    public int Arid { get; private set; }

    /// <summary>
    /// The evaluator controlling this activation record
    /// </summary>
    public ILanguageInterpreter<TSymbolData> ParentInterpreter { get; }

    /// <summary>
    /// The static scope associated with this actiivation record
    /// </summary>
    public LanguageScope AssociatedScope { get; private set; }

    /// <summary>
    /// The static link for this activation record
    /// </summary>
    public ActivationRecord<TSymbolData> UpperStaticAr { get; }

    /// <summary>
    /// The dynamic link for this activation record
    /// </summary>
    public ActivationRecord<TSymbolData> UpperDynamicAr { get; }

    /// <summary>
    /// Local variables and procedure parameters values are stored here
    /// </summary>
    private readonly Dictionary<string, ActivationRecordEntry<TSymbolData>> _variableValueDictionary = new Dictionary<string, ActivationRecordEntry<TSymbolData>>();


    /// <summary>
    /// True if this is the top of the activation record stack
    /// </summary>
    public bool IsRootAr => UpperDynamicAr == null;

    /// <summary>
    /// True if this activation record has no upper static activation record (i.e. no static link)
    /// </summary>
    public bool IsRootStaticAr => UpperStaticAr == null;

    /// <summary>
    /// True if this activation record has an upper static activation record (i.e. a static link)
    /// </summary>
    public bool HasUpperStaticAr => UpperStaticAr != null;


    //public SymbolWithScope AssociatedSymbolWithScope { get { return (AssociatedScope is ScopeSymbolChild) ? ((ScopeSymbolChild)AssociatedScope).ParentLanguageSymbolWithScope : null; } }


    /// <summary>
    /// Create a root activation record
    /// </summary>
    /// <param name="interpreter">The controlling evaluator for this activation record</param>
    /// <param name="associatedScope">The associated scope</param>
    public ActivationRecord(ILanguageInterpreter<TSymbolData> interpreter, LanguageScope associatedScope)
    {
        Arid = CreateNewId();
        ParentInterpreter = interpreter;
        UpperStaticAr = null;
        UpperDynamicAr = null;
        AssociatedScope = associatedScope;
    }

    /// <summary>
    /// Create a sub-activation record
    /// </summary>
    /// <param name="callingAr">The dynamic link (the following activation record on the stack)</param>
    /// <param name="associatedScope">The associated scope</param>
    /// <param name="useUpperStaticAr">If true, use the dynamic link as the static link (make the upper static scope the same as the following activation record on the stack)</param>
    public ActivationRecord(ActivationRecord<TSymbolData> callingAr, LanguageScope associatedScope, bool useUpperStaticAr)
    {
        Arid = CreateNewId();
        ParentInterpreter = callingAr.ParentInterpreter;
        UpperStaticAr = useUpperStaticAr ? callingAr : null;
        UpperDynamicAr = callingAr;
        AssociatedScope = associatedScope;
    }


    public IEnumerable<ActivationRecord<TSymbolData>> StaticChain
    {
        get
        {
            for (var ar = this; !ReferenceEquals(ar, null); ar = ar.UpperStaticAr)
                yield return ar;
        }
    }

    //public void RemoveUpperStaticAR()
    //{
    //    UpperStaticAR = null;
    //}


    public bool ContainsSymbol(SymbolDataStore symbol)
    {
        return _variableValueDictionary.ContainsKey(symbol.ObjectName);
    }

    public void AddSymbolData(SymbolDataStore symbol, TSymbolData symbolData)
    {
        _variableValueDictionary.Add(symbol.ObjectName, new ActivationRecordEntry<TSymbolData>(symbol, symbolData));
    }

    public void SetSymbolData(SymbolDataStore symbol, TSymbolData symbolData)
    {
        _variableValueDictionary[symbol.ObjectName] = new ActivationRecordEntry<TSymbolData>(symbol, symbolData);
    }

    public TSymbolData GetSymbolData(SymbolDataStore symbol)
    {
        return _variableValueDictionary[symbol.ObjectName].SymbolData;
    }

    public bool TryGetSymbolData(SymbolDataStore symbol, out TSymbolData value)
    {
        if (_variableValueDictionary.TryGetValue(symbol.ObjectName, out var entry))
        {
            value = entry.SymbolData;
            return true;
        }

        value = default(TSymbolData);
        return false;
    }
}