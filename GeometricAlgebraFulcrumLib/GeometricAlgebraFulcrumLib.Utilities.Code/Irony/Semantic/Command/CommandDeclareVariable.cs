using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Command;

/// <summary>
/// This class represents a declaration command
/// </summary>
public class CommandDeclareVariable : LanguageCommand
{
    /// <summary>
    /// The declared data store
    /// </summary>
    public SymbolDataStore DataStore { get; }

    /// <summary>
    /// Return the declared data store as a local variable object if possible
    /// </summary>
    public SymbolLocalVariable LocalVariable => DataStore as SymbolLocalVariable;


    public CommandDeclareVariable(LanguageScope parentScope, SymbolDataStore dataStore)
        : base(parentScope)
    {
        DataStore = dataStore;
    }
}