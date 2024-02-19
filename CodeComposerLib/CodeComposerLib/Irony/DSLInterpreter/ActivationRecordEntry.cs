using CodeComposerLib.Irony.Semantic.Symbol;

namespace CodeComposerLib.Irony.DSLInterpreter;

public sealed class ActivationRecordEntry<T>
{
    /// <summary>
    /// The data store language symbol of this entry
    /// </summary>
    public SymbolDataStore Symbol { get; private set; }

    /// <summary>
    /// The data associated with the symbol of this entry
    /// </summary>
    public T SymbolData { get; private set; }


    public ActivationRecordEntry(SymbolDataStore symbol, T symbolData)
    {
        Symbol = symbol;

        SymbolData = symbolData;
    }
}